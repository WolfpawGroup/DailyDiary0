using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

using System.Security.Cryptography;
using System.Net.Mail;
using System.Net;

namespace DailyDiary
{
	public partial class PasswordReminderWindow : Window
	{
		public SQLiteConnection sqlc { get; set; }
		public long IDnumber = 0;
		public string username = "";
		public string code = "";
		private string pass = ""; 

		public PasswordReminderWindow()
		{
			InitializeComponent();
		}

		private void btn_SendMail_Click(object sender, RoutedEventArgs e)
		{
			string pwdandemail = c_DBManager.PWDReminder(sqlc, tb_Name.Text, tb_Email.Text);

			if (pwdandemail == "")
			{
				MessageBox.Show("The username or email address you provided did not match.\r\nPlease try again.", "No match!", MessageBoxButton.OK, MessageBoxImage.Error);
			}
			else
			{
				string pwd = pwdandemail.Substring(0, pwdandemail.IndexOf("::"));
				string email = pwdandemail.Substring(pwdandemail.IndexOf("::") + 2);

				username = tb_Name.Text;
				IDnumber = DateTime.Now.ToFileTime() * 10000 + new Random().Next(100, 1000);

				string tmp = IDnumber + "::" + username;

				foreach (byte b in new MD5CryptoServiceProvider().ComputeHash(Encoding.UTF8.GetBytes(tmp)))
				{
					code += b.ToString("X2").ToUpper();
				}

				try
				{
					MailMessage mail = new MailMessage("noreply.wolfpawstudios+DailyDiary@gmail.com", email);
					SmtpClient client = new SmtpClient();
					client.Port = 587;
					client.EnableSsl = true;
					client.DeliveryMethod = SmtpDeliveryMethod.Network;
					client.UseDefaultCredentials = false;
					client.Credentials = new NetworkCredential("noreply.wolfpawstudios", "Alpha666");
					client.Host = "smtp.gmail.com";
					mail.Subject = "Password reminder";
					mail.Body = "TEST → " + code;
					client.Send(mail);
				}
				catch(Exception ex)
				{
					MessageBox.Show(ex.Message, ex.GetBaseException().ToString(), MessageBoxButton.OK, MessageBoxImage.Error);
				}

				pass = pwd;
			}


		}

		private void btn_ShowPass_Click(object sender, RoutedEventArgs e)
		{
			if(tb_ID.Text == code)
			{
				tb_Password.Text = pass;
			}
			else
			{
				MessageBox.Show("The verification ID you have provided is not valid.\r\nPlease try again.", "No match!", MessageBoxButton.OK, MessageBoxImage.Error);
			}
		}

		private void btn_Paste_Click(object sender, RoutedEventArgs e)
		{
			tb_ID.Text = Clipboard.GetText();
		}

		private void btn_Copy_Click(object sender, RoutedEventArgs e)
		{
			Clipboard.SetText(tb_Password.Text);
			tb_Password.Text = "";
		}

		private void btn_Close_Click(object sender, RoutedEventArgs e)
		{
			this.Close();
		}
	}
}
