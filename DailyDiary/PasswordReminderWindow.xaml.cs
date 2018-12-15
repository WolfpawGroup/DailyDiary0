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

using Google.Apis.Gmail.v1.Data;
using Google.Apis.Gmail.v1;
using Google.Apis.Gmail;
using Google.Apis;

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
		private string _requestId = "";
		private string _requestTime = "";
		public MD5CryptoServiceProvider md50 = new MD5CryptoServiceProvider();
		public misc msc = new misc();
		
		public PasswordReminderWindow()
		{
			InitializeComponent();
			Loaded += PasswordReminderWindow_Loaded;
		}

		private void PasswordReminderWindow_Loaded(object sender, RoutedEventArgs e)
		{
			generateRequestId();
			tb_Name.Focus();
		}

		public void generateRequestId()
		{
			string tim = DateTime.Now.ToFileTime() + "";
			byte[] bbb = new byte[20];
			new Random().NextBytes(bbb);
			List<byte> bb = bbb.ToList();
			bb.AddRange(Encoding.UTF8.GetBytes(tim));
			bbb = bb.ToArray();
			byte[] b1 = md50.ComputeHash( Encoding.UTF8.GetBytes(Environment.UserName )	);
			byte[] b3 = md50.ComputeHash( bbb );

			byte[] b4 = new byte[b1.Length * 2];

			b1.CopyTo(b4, 0);
			b3.CopyTo(b4, b1.Length);

			string s = "";
			foreach(byte b in b4)
			{
				s += b.ToString("X2").ToUpper();
			}

			int[] ii = new int[8];
			int i = 0;
			int iii = 0;
			for(int v = 0; v < s.Length; v++)
			{
				if(v % 8 == 0 && v != 0)
				{
					ii[iii] = new Random(i).Next(0,256);
					iii++;
					i = 0;
				}
				i += (int)(s[v] / Math.PI) + iii;
			}
			ii[iii] = new Random(i).Next(0, 256);

			s = "";

			foreach(int c in ii)
			{
				s += c.ToString("X2").ToUpper();
			}

			_requestId = s;
			lbl_RequestVal.Content = _requestId;
			DateTime dtn = DateTime.Now;
			_requestTime = $"{dtn.ToShortDateString()} {dtn.ToShortTimeString()} ({dtn.ToFileTimeUtc()} UTC)";
		}

		private void btn_SendMail_Click(object sender, RoutedEventArgs e)
		{
			sendEmail();
		}
		
		public void sendEmail()
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

					//TODO: Mail
					MailMessage mail = new MailMessage("noreply.wolfpawstudios+DailyDiary+PasswordReset@gmail.com", email);
					SmtpClient client = new SmtpClient();

					client.Port = 587;
					client.EnableSsl = true;
					client.DeliveryMethod = SmtpDeliveryMethod.Network;
					client.UseDefaultCredentials = false;
					client.Credentials = new NetworkCredential("noreply.wolfpawstudios", "jrhgjukmcmbbancj");
					client.Host = "smtp.gmail.com";
					mail.Subject = "Password reminder";
					mail.IsBodyHtml = true;
					mail.Body = buildEmailBody(username, code);
					client.Send(mail);
					msc.flashText(lbl_EmailSent);
				}
				catch (Exception ex)
				{
					MessageBox.Show(ex.Message, ex.GetBaseException().ToString(), MessageBoxButton.OK, MessageBoxImage.Error);
				}

				pass = pwd;
			}
		}

		public string buildEmailBody(string username, string passcode)
		{
			StringBuilder sb = new StringBuilder();
			sb._("<body>");
			sb._("<h1>Dear {0},</h1>");
			sb._("<p>");
			sb._("You have requested a password reminder for your DailyDiary account.<br />");
			sb._("To see your password, please copy the following code and paste it into the password reminder window<br />");
			sb._("in the [ReminderID] box.<br />");
			sb._("This code is only valid in the current session of the password reminder window. <br />");
			sb._("If you have closed the window, you will have to start this process again.");
			sb._("</p>");
			sb._("<table>");
			sb._("<tr>");
			sb._("<td>RequestID: </td><td>{2}</td>");
			sb._("</tr>");
			sb._("<tr>");
			sb._("<td>ReminderID: </td><td>{1}</td>");
			sb._("</tr>");
			sb._("<tr>");
			sb._("<td>Request Sent: </td><td>{3}</td>");
			sb._("</tr>");
			sb._("</table>");
			sb._("<br />");
			sb._("If you were not the one to request this email, you may wish to change your password, just in case.");
			sb._("<br />");
			sb._("<h5><i>Best Regards, <br />WolfPaw Studio<br />DailyDiary</i></h5>");
			sb._("</body>");
			return string.Format(sb.ToString(), username, passcode, _requestId, _requestTime);
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
			tb_ID.Text = Clipboard.GetText().Trim();
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

		private void F_PasswordReminder_PreviewKeyDown(object sender, KeyEventArgs e)
		{
			if(e.Key == Key.Escape) { this.Close(); }
		}

		private void Tb_Name_PreviewKeyDown(object sender, KeyEventArgs e)
		{
			enterKey(e);
		}

		private void Tb_Email_PreviewKeyDown(object sender, KeyEventArgs e)
		{
			enterKey(e);
		}

		private void enterKey(KeyEventArgs e)
		{
			if(e.Key == Key.Enter)
			{
				sendEmail();
			}
		}
	}

	public static class extensions
	{
		public static void _(this StringBuilder sb, string addition)
		{
			sb.Append(addition);
		}
		
		
	}
}
