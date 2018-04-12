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
	/// Interaction logic for PasswordChangeWindow.xaml
	/// </summary>
	public partial class PasswordChangeWindow : Window
	{
		public SQLiteConnection sqlc { get; set; }

		public PasswordChangeWindow()
		{
			InitializeComponent();

			Loaded += PasswordChangeWindow_Loaded;
		}

		private void PasswordChangeWindow_Loaded(object sender, RoutedEventArgs e)
		{
			
		}

		private void btn_Cancel_Click(object sender, RoutedEventArgs e)
		{
			this.Close();
		}

		private void btn_ChangePass_Click(object sender, RoutedEventArgs e)
		{
			tb_Name.Background = Brushes.White;
			pb_OldPass.Background = Brushes.White;

			if (c_DBManager.login(sqlc, tb_Name.Text, pb_OldPass.Password))
			{
				if (c_DBManager.changePass(sqlc, tb_Name.Text, pb_NewPass.Password))
				{
					this.Close();
				}
				else
				{
					tb_Name.Background = Brushes.LightPink;
					pb_OldPass.Background = Brushes.LightPink;
				}
			}
			else
			{
				tb_Name.Background = Brushes.LightPink;

				if (c_DBManager.userExists(sqlc, tb_Name.Text))
				{
					pb_OldPass.Background = Brushes.LightPink;
				}
			}
		}
		
		private void btn_OldPassShow_PreviewMouseDown(object sender, MouseButtonEventArgs e)
		{
			tb_OldPass.Text = pb_OldPass.Password;
			tb_OldPass.Visibility = Visibility.Visible;
		}

		private void btn_OldPassShow_PreviewMouseUp(object sender, MouseButtonEventArgs e)
		{
			tb_OldPass.Visibility = Visibility.Hidden;
		}

		private void btn_NewPassShow_PreviewMouseDown(object sender, MouseButtonEventArgs e)
		{
			tb_NewPass.Text = pb_NewPass.Password;
			tb_NewPass.Visibility = Visibility.Visible;
		}

		private void btn_NewPassShow_PreviewMouseUp(object sender, MouseButtonEventArgs e)
		{
			tb_NewPass.Visibility = Visibility.Hidden;
		}
	}
}
