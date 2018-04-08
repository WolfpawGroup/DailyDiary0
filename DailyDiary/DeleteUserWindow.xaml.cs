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

namespace DailyDiary
{
	/// <summary>
	/// Interaction logic for DeleteUserWindow.xaml
	/// </summary>
	public partial class DeleteUserWindow : Window
	{
		public SQLiteConnection sqlc { get; set; }

		public DeleteUserWindow()
		{
			InitializeComponent();
		}

		private void btn_Cancel_Click(object sender, RoutedEventArgs e)
		{
			this.Close();
		}

		private void btn_DeleteUser_Click(object sender, RoutedEventArgs e)
		{
			//DELETE USER

			if (tb_Username.Text != "" && tb_Password.Password != "" && cb_Sure.IsChecked == true)
			{

				if (c_DBManager.login(sqlc, tb_Username.Text, tb_Password.Password))
				{
					c_DBManager.deleteUser(sqlc, tb_Username.Text);
					this.Close();
				}
				else
				{
					if (!c_DBManager.userExists(sqlc, tb_Username.Text))
					{
						lbl_message.Content = "Username does not exist.";
					}
					else
					{
						lbl_message.Content = "This usernamd/password combination does not exist.";
					}
				}
			}
			else
			{
				if(tb_Username.Text == "")
				{
					tb_Username.Background = Brushes.LightPink;
					lbl_message.Content = "Username can not be empty!";
				}
				else{ tb_Username.Background = Brushes.White; }
				if (tb_Password.Password == "")
				{
					tb_Password.Background = Brushes.LightPink;
					lbl_message.Content = "Password can not be empty!";
				}
				else { tb_Password.Background = Brushes.White; }
				if (cb_Sure.IsChecked == false)
				{
					lbl_message.Content = "Are you sure?";
				}
			}
		}
	}
}
