using System;
using System.Collections.Generic;
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

using System.Data.SQLite;
using System.Text.RegularExpressions;

namespace DailyDiary
{
	public partial class NewUserWindow : Window
	{
		public SQLiteConnection sqlc { get; set; }
		public string username { get; set; }

		public NewUserWindow()
		{
			InitializeComponent();
			Loaded += NewUserWindow_Loaded;
		}

		private void NewUserWindow_Loaded(object sender, RoutedEventArgs e)
		{

		}

		public bool checkEmail(string email)
		{
			bool ret = false;

			Regex r = new Regex("[\\w-]{3,}\\@\\w{3,}\\.[a-z]{2,}");
			if (r.IsMatch(tb_Email.Text)) { ret = true; }

			return ret;
		}

		private void btn_CreateUser_Click(object sender, RoutedEventArgs e)
		{
			if (tb_UserName.Text != "" && tb_Password.Password != "" && tb_Email.Text != "" && checkEmail(tb_Email.Text))
			{
				bool ex = c_DBManager.userExists(sqlc, tb_UserName.Text);
				if (!ex)
				{
					c_DBManager.addUser(sqlc, tb_UserName.Text, c_DBManager.encryptData(tb_Password.Password, tb_UserName.Text), DateTime.Now.ToShortDateString(), tb_Email.Text);
					username = tb_UserName.Text;
					this.Close();
				}
				else
				{
					tb_UserName.Background = Brushes.LightPink;
					lbl_Info.Content = "A User with that name already exists";
				}
			}
			else
			{
				string txt = "";

				if (tb_UserName.Text == "")
				{
					tb_UserName.Background = Brushes.LightPink;
					txt = "Username can not be empty";
				}
				else if (tb_Password.Password == "")
				{
					tb_UserName.Background = Brushes.White;
					tb_Password.Background = Brushes.LightPink;
					txt = "Password can not be empty";
				}
				else if (tb_Email.Text == "")
				{
					tb_UserName.Background = Brushes.White;
					tb_Password.Background = Brushes.White;
					tb_Email.Background = Brushes.LightPink;
					txt = "Email can not be empty";
				}
				else if (!checkEmail(tb_Email.Text))
				{
					tb_UserName.Background = Brushes.White;
					tb_Password.Background = Brushes.White;
					tb_Email.Background = Brushes.LightPink;
					txt = "Not a valid email address";
				}

				lbl_Info.Content = txt;
			}
		}
	}
}
