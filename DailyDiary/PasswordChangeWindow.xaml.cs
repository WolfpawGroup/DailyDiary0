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
	}
}
