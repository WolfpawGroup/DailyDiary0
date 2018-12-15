using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace DailyDiary
{
	/// <summary>
	/// Interaction logic for SettingsWindow.xaml
	/// </summary>
	public partial class SettingsWindow : Window
	{
		public SettingsWindow()
		{
			InitializeComponent();
		}

		private void w_Settings_Loaded(object sender, RoutedEventArgs e)
		{
			loadSettings();
		}

		public void saveSettings()
		{
			Properties.Settings.Default.s_textboxLightCursor = cb_Cursor.IsChecked == true;
			Properties.Settings.Default.s_textboxLightCursorNightOnly = rb_Cursor_NightMode.IsChecked == true;

			Brush b = null;
			Color c;

			//-- Day

			b = rect_Color_Day_Day.Fill;
			c = ((SolidColorBrush)b).Color;
			Properties.Settings.Default.s_day_DayColor = System.Drawing.Color.FromArgb(255, c.R, c.G, c.B);

			b = rect_Color_Night_Day.Fill;
			c = ((SolidColorBrush)b).Color;
			Properties.Settings.Default.s_night_DayColor = System.Drawing.Color.FromArgb(255, c.R, c.G, c.B);

			//-- BG

			b = rect_Color_Day_BG.Fill;
			c = ((SolidColorBrush)b).Color;
			Properties.Settings.Default.s_day_MainBackgroundColor = System.Drawing.Color.FromArgb(255, c.R, c.G, c.B);

			b = rect_Color_Night_BG.Fill;
			c = ((SolidColorBrush)b).Color;
			Properties.Settings.Default.s_night_MainBackgroundColor = System.Drawing.Color.FromArgb(255, c.R, c.G, c.B);

			//-- Day Text

			b = rect_Color_Day_DayText.Fill;
			c = ((SolidColorBrush)b).Color;
			Properties.Settings.Default.s_day_DayTextColor = System.Drawing.Color.FromArgb(255, c.R, c.G, c.B);

			b = rect_Color_Night_DayText.Fill;
			c = ((SolidColorBrush)b).Color;
			Properties.Settings.Default.s_night_DayTextColor = System.Drawing.Color.FromArgb(255, c.R, c.G, c.B);

			//-- Sunday

			b = rect_Color_Day_Sunday.Fill;
			c = ((SolidColorBrush)b).Color;
			Properties.Settings.Default.s_day_SundayColor = System.Drawing.Color.FromArgb(255, c.R, c.G, c.B);

			b = rect_Color_Night_Sunday.Fill;
			c = ((SolidColorBrush)b).Color;
			Properties.Settings.Default.s_night_SundayColor = System.Drawing.Color.FromArgb(255, c.R, c.G, c.B);

			//-- Text

			b = rect_Color_Day_Text.Fill;
			c = ((SolidColorBrush)b).Color;
			Properties.Settings.Default.s_day_DayTextColor = System.Drawing.Color.FromArgb(255, c.R, c.G, c.B);

			b = rect_Color_Night_Text.Fill;
			c = ((SolidColorBrush)b).Color;
			Properties.Settings.Default.s_night_TextColor = System.Drawing.Color.FromArgb(255, c.R, c.G, c.B);

			//-- Text BG

			b = rect_Color_Day_TextBG.Fill;
			c = ((SolidColorBrush)b).Color;
			Properties.Settings.Default.s_day_TextboxBackgroundColor = System.Drawing.Color.FromArgb(255, c.R, c.G, c.B);

			b = rect_Color_Night_TextBG.Fill;
			c = ((SolidColorBrush)b).Color;
			Properties.Settings.Default.s_night_TextboxBackgroundColor = System.Drawing.Color.FromArgb(255, c.R, c.G, c.B);


			Properties.Settings.Default.Save();

		}

		public void loadSettings()
		{
			if (File.Exists("ibeam.cur"))
			{
				cb_Cursor.IsEnabled = true;

				cb_Cursor.IsChecked = Properties.Settings.Default.s_textboxLightCursor;

				if (Properties.Settings.Default.s_textboxLightCursorNightOnly)
				{
					rb_Cursor_NightMode.IsChecked = true;
				}
				else
				{
					rb_Cursor_Allways.IsChecked = true;
				}
			}
			else
			{
				cb_Cursor.IsChecked = false;
				cb_Cursor.IsEnabled = false;
			}

			cb_AskBeforeChange.IsChecked = Properties.Settings.Default.s_AskBeforeChange;
			cb_SingleUser.IsChecked = Properties.Settings.Default.s_SingleUserMode;
			
			//tb_Data.Cursor = useLightCursor && myCursor != null ? myCursor : Cursors.IBeam;
			//tb_Title.Cursor = useLightCursor && myCursor != null ? myCursor : Cursors.IBeam;

			System.Drawing.Color c = Properties.Settings.Default.s_night_DayColor;
			rect_Color_Night_Day.Fill = new SolidColorBrush(new Color() { A = c.A, R = c.R, G = c.G, B = c.B });

			c = Properties.Settings.Default.s_day_DayColor;
			rect_Color_Day_Day.Fill = new SolidColorBrush(new Color() { A = c.A, R = c.R, G = c.G, B = c.B });

			//----

			c = Properties.Settings.Default.s_night_SundayColor;
			rect_Color_Night_Sunday.Fill = new SolidColorBrush(new Color() { A = c.A, R = c.R, G = c.G, B = c.B });

			c = Properties.Settings.Default.s_day_SundayColor;
			rect_Color_Day_Sunday.Fill = new SolidColorBrush(new Color() { A = c.A, R = c.R, G = c.G, B = c.B });

			//----

			c = Properties.Settings.Default.s_night_MainBackgroundColor;
			rect_Color_Night_BG.Fill = new SolidColorBrush(new Color() { A = c.A, R = c.R, G = c.G, B = c.B });

			c = Properties.Settings.Default.s_day_MainBackgroundColor;
			rect_Color_Day_BG.Fill = new SolidColorBrush(new Color() { A = c.A, R = c.R, G = c.G, B = c.B });

			//----

			c = Properties.Settings.Default.s_night_TextboxBackgroundColor;
			rect_Color_Night_TextBG.Fill = new SolidColorBrush(new Color() { A = c.A, R = c.R, G = c.G, B = c.B });

			c = Properties.Settings.Default.s_day_TextboxBackgroundColor;
			rect_Color_Day_TextBG.Fill = new SolidColorBrush(new Color() { A = c.A, R = c.R, G = c.G, B = c.B });

			//----

			c = Properties.Settings.Default.s_night_TextColor;
			rect_Color_Night_Text.Fill = new SolidColorBrush(new Color() { A = c.A, R = c.R, G = c.G, B = c.B });

			c = Properties.Settings.Default.s_day_TextColor;
			rect_Color_Day_Text.Fill = new SolidColorBrush(new Color() { A = c.A, R = c.R, G = c.G, B = c.B });

			//----

			c = Properties.Settings.Default.s_night_DayTextColor;
			rect_Color_Night_DayText.Fill = new SolidColorBrush(new Color() { A = c.A, R = c.R, G = c.G, B = c.B });

			c = Properties.Settings.Default.s_day_DayTextColor;
			rect_Color_Day_DayText.Fill = new SolidColorBrush(new Color() { A = c.A, R = c.R, G = c.G, B = c.B });

			setupToolTips();
		}

		private void cb_Cursor_Checked(object sender, RoutedEventArgs e)
		{
			rb_Cursor_Allways.IsEnabled = rb_Cursor_NightMode.IsEnabled = true;
		}

		private void cb_Cursor_Unloaded(object sender, RoutedEventArgs e)
		{
			rb_Cursor_Allways.IsEnabled = rb_Cursor_NightMode.IsEnabled = false;
		}

		private void rect_Color_Day_Text_MouseDown(object sender, MouseButtonEventArgs e)
		{
			Rectangle r = sender as Rectangle;

			if(r != null)
			{
				ColorDialog cd = new ColorDialog();
				if(cd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
				{
					r.Fill = new SolidColorBrush(new Color() { A = cd.Color.A, R = cd.Color.R, G = cd.Color.G, B = cd.Color.B });
					setupToolTips();
				}
			}
		}

		public void setupToolTips()
		{
			foreach(UIElement c in g_MainGrid.Children)
			{
				if(c is Rectangle && (c as Rectangle).Fill != null)
				{
					Color cc = ((SolidColorBrush)(c as Rectangle).Fill).Color;
					(c as Rectangle).ToolTip = "RGB: (" + cc.R + ", " + cc.G + ", " + cc.B + ")";

					System.Drawing.Color sc = System.Drawing.Color.FromArgb(255, cc.R, cc.G, cc.B);

					try
					{
						(c as Rectangle).ToolTip += "\r\nHEX: (#" + sc.Name.ToUpper().Substring(2) + ")";
					}
					catch
					{

					}
				}
			}
		}

		private void Save_Click(object sender, RoutedEventArgs e)
		{
			saveSettings();
			this.Close();
		}

		private void Cancel_Click(object sender, RoutedEventArgs e)
		{
			this.Close();
		}
	}
}
