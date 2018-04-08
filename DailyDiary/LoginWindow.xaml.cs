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

namespace DailyDiary
{
	/// <summary>
	/// Interaction logic for LoginWindow.xaml
	/// </summary>
	public partial class LoginWindow : Window
	{
		SQLiteConnection sqlc = new SQLiteConnection();

		public LoginWindow()
		{
			InitializeComponent();

			Loaded += LoginWindow_Loaded;
		}

		private void LoginWindow_Loaded(object sender, RoutedEventArgs e)
		{
			c_DBManager.createFile();
			sqlc = c_DBManager.connect();
			c_DBManager.createTables(sqlc);

			tb_Username.Focus();
		}

		private void cb_ShowPass_Checked(object sender, RoutedEventArgs e)
		{
			tb_Pass_Clear.Text = tb_Pass.Password;
			tb_Pass_Clear.Visibility = Visibility.Visible;
		}

		private void cb_ShowPass_Unchecked(object sender, RoutedEventArgs e)
		{
			tb_Pass_Clear.Visibility = Visibility.Hidden;
		}

		private void tb_Pass_Clear_TextChanged(object sender, TextChangedEventArgs e)
		{
			tb_Pass.Password = tb_Pass_Clear.Text;
		}

		private void btn_More_Click(object sender, RoutedEventArgs e)
		{
			if(sp_Stack.Visibility == Visibility.Hidden)
			{
				sp_Stack.Visibility = Visibility.Visible;
			}
			else
			{
				sp_Stack.Visibility = Visibility.Hidden;
			}
		}

		private void Grid_MouseDown(object sender, MouseButtonEventArgs e)
		{
			if (sp_Stack.Visibility == Visibility.Visible)
			{
				sp_Stack.Visibility = Visibility.Hidden;
			}
		}

		private void tb_Pass_GotFocus(object sender, RoutedEventArgs e)
		{
			if (sp_Stack.Visibility == Visibility.Visible)
			{
				sp_Stack.Visibility = Visibility.Hidden;
			}
		}

		private void btn_Login_Click(object sender, RoutedEventArgs e)
		{
			bool login = c_DBManager.login(sqlc, tb_Username.Text, tb_Pass.Password);
			if (login)
			{
				MainWindow mw = new MainWindow()
				{
					sqlc = sqlc,
					username = tb_Username.Text,
					userid = c_DBManager.getUserId(sqlc, tb_Username.Text)
				};

				tb_Username.Text = "";
				tb_Pass.Password = "";
				tb_Pass_Clear.Text = "";
				cb_ShowPass.IsChecked = false;

				tb_Pass.Background = Brushes.White;
				tb_Pass_Clear.Background = Brushes.White;
				tb_Username.Background = Brushes.White;

				Hide();
				mw.ShowDialog();
				Show();

				tb_Username.Focus();
			}
			else
			{
				bool userExists = c_DBManager.userExists(sqlc, tb_Username.Text);
				if (userExists)
				{
					tb_Pass.Background = Brushes.LightPink;
					tb_Pass_Clear.Background = Brushes.LightPink;
				}
				else
				{
					tb_Username.Background = Brushes.LightPink;
				}
			}

		}

		private void btn_Quit_Click(object sender, RoutedEventArgs e)
		{
			Environment.Exit(0);
		}

		private void btn_ForgotMyPass_Click(object sender, RoutedEventArgs e)
		{
			PasswordReminderWindow pwrw = new PasswordReminderWindow() { sqlc = sqlc };
			pwrw.ShowDialog();
		}

		private void btn_ChangePass_Click(object sender, RoutedEventArgs e)
		{
			PasswordChangeWindow pwcw = new PasswordChangeWindow() { sqlc = sqlc };
			pwcw.ShowDialog();
		}

		private void btn_DeleteUser_Click(object sender, RoutedEventArgs e)
		{
			DeleteUserWindow duw = new DeleteUserWindow() { sqlc = sqlc };
			duw.ShowDialog();
		}

		private void btn_NewUser_Click(object sender, RoutedEventArgs e)
		{
			NewUserWindow nuw = new NewUserWindow() { sqlc = sqlc};
			nuw.ShowDialog();
			if(nuw.username != "")
			{
				tb_Username.Text = nuw.username;
			}
			
		}

		public void handleKeys(KeyEventArgs e)
		{
			if (e.Key == Key.Enter)
			{
				btn_Login_Click(null, null);
			}
			else if (e.Key == Key.Escape)
			{
				btn_Quit_Click(null, null);
			}
		}

		private void tb_Username_KeyDown(object sender, KeyEventArgs e)
		{
			handleKeys(e);
		}

		private void tb_Pass_KeyDown(object sender, KeyEventArgs e)
		{
			handleKeys(e);
		}
	}
}
