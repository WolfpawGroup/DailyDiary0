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
using System.Xml;
using System.IO;
using System.Windows.Controls.Primitives;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;

namespace DailyDiary
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		[DllImport("User32.dll", CharSet = CharSet.Ansi, BestFitMapping = false, ThrowOnUnmappableChar = true)]
		private static extern IntPtr LoadCursorFromFile(String str);

		public SQLiteConnection sqlc { get; set; }
		public int userid { get; set; }
		public string username { get; set; }

		string loadedTitle = "";
		string loadedData = "";
		string loadedDate = "";

		int selectedYear = 0;
		int selectedMonth = 0;
		int selectedDay = 0;

		c_CalendarBackground CalendarBackGround = null;
		Calendar MyCalendar = null;
		Cursor myCursor = null;
		Dictionary<int, Grid> days = new Dictionary<int, Grid>();
		Dictionary<string, string> langs = new Dictionary<string, string>();

		bool nightMode = false;

		/*===== SETTINGS =====*/

		bool useLightCursor = false;
		bool lightCursorWithNightModeOnly = false;
		bool askBeforeChange = true;
		Brush dayColor = Brushes.LightGray;
		Brush sundayColor = Brushes.Red;
		Brush mainBackgroundColor = Brushes.White;
		Brush textboxBackgroundColor = Brushes.White;
		Brush textColor = Brushes.Black;
		Brush dayTextColor = Brushes.Black;
		Brush selectedDayColor = Brushes.Black;
		Brush textSelectionColor = Brushes.Black;


		public MainWindow()
		{
			InitializeComponent();
			Loaded += MainWindow_Loaded;
		}

		public void loadLangs()
		{
			langs.Add("aa", "Afar");
			langs.Add("ab", "Abkhazian");
			langs.Add("af", "Afrikaans");
			langs.Add("ak", "Akan");
			langs.Add("sq", "Albanian");
			langs.Add("am", "Amharic");
			langs.Add("ar", "Arabic");
			langs.Add("an", "Aragonese");
			langs.Add("hy", "Armenian");
			langs.Add("as", "Assamese");
			langs.Add("av", "Avaric");
			langs.Add("ae", "Avestan");
			langs.Add("ay", "Aymara");
			langs.Add("az", "Azerbaijani");
			langs.Add("ba", "Bashkir");
			langs.Add("bm", "Bambara");
			langs.Add("eu", "Basque");
			langs.Add("be", "Belarusian");
			langs.Add("bn", "Bengali");
			langs.Add("bh", "Bihari languages");
			langs.Add("bi", "Bislama");
			langs.Add("bs", "Bosnian");
			langs.Add("br", "Breton");
			langs.Add("bg", "Bulgarian");
			langs.Add("my", "Burmese");
			langs.Add("ca", "Catalan");
			langs.Add("ch", "Chamorro");
			langs.Add("ce", "Chechen");
			langs.Add("zh", "Chinese");
			langs.Add("cu", "Church Slavic");
			langs.Add("cv", "Chuvash");
			langs.Add("kw", "Cornish");
			langs.Add("co", "Corsican");
			langs.Add("cr", "Cree");
			langs.Add("cs", "Czech");
			langs.Add("da", "Danish");
			langs.Add("dv", "Divehi");
			langs.Add("nl", "Dutch");
			langs.Add("dz", "Dzongkha");
			langs.Add("en-UK", "English UK");
			langs.Add("en-US", "English US");
			langs.Add("eo", "Esperanto");
			langs.Add("et", "Estonian");
			langs.Add("ee", "Ewe");
			langs.Add("fo", "Faroese");
			langs.Add("fj", "Fijian");
			langs.Add("fi", "Finnish");
			langs.Add("fr", "French");
			langs.Add("fy", "Western Frisian");
			langs.Add("ff", "Fulah");
			langs.Add("ka", "Georgian");
			langs.Add("de", "German");
			langs.Add("gd", "Gaelic");
			langs.Add("ga", "Irish");
			langs.Add("gl", "Galician");
			langs.Add("gv", "Manx");
			langs.Add("el", "Greek");
			langs.Add("gn", "Guarani");
			langs.Add("gu", "Gujarati");
			langs.Add("ht", "Haitian");
			langs.Add("ha", "Hausa");
			langs.Add("he", "Hebrew");
			langs.Add("hz", "Herero");
			langs.Add("hi", "Hindi");
			langs.Add("ho", "Hiri Motu");
			langs.Add("hr", "Croatian");
			langs.Add("hu", "Hungarian");
			langs.Add("ig", "Igbo");
			langs.Add("is", "Icelandic");
			langs.Add("io", "Ido");
			langs.Add("ii", "Sichuan Yi");
			langs.Add("iu", "Inuktitut");
			langs.Add("ie", "Occidental");
			langs.Add("ia", "Interlingua");
			langs.Add("id", "Indonesian");
			langs.Add("ik", "Inupiaq");
			langs.Add("it", "Italian");
			langs.Add("jv", "Javanese");
			langs.Add("ja", "Japanese");
			langs.Add("kl", "Greenlandic");
			langs.Add("kn", "Kannada");
			langs.Add("ks", "Kashmiri");
			langs.Add("kr", "Kanuri");
			langs.Add("kk", "Kazakh");
			langs.Add("km", "Central Khmer");
			langs.Add("ki", "Kikuyu");
			langs.Add("rw", "Kinyarwanda");
			langs.Add("ky", "Kirghiz");
			langs.Add("kv", "Komi");
			langs.Add("kg", "Kongo");
			langs.Add("ko", "Korean");
			langs.Add("kj", "Kuanyama");
			langs.Add("ku", "Kurdish");
			langs.Add("lo", "Lao");
			langs.Add("la", "Latin");
			langs.Add("lv", "Latvian");
			langs.Add("li", "Limburgan");
			langs.Add("ln", "Lingala");
			langs.Add("lt", "Lithuanian");
			langs.Add("lb", "Luxembourgish");
			langs.Add("lu", "Luba-Katanga");
			langs.Add("lg", "Ganda");
			langs.Add("mk", "Macedonian");
			langs.Add("mh", "Marshallese");
			langs.Add("ml", "Malayalam");
			langs.Add("mi", "Maori");
			langs.Add("mr", "Marathi");
			langs.Add("ms", "Malay");
			langs.Add("mg", "Malagasy");
			langs.Add("mt", "Maltese");
			langs.Add("mn", "Mongolian");
			langs.Add("na", "Nauru");
			langs.Add("nv", "Navajo");
			langs.Add("nr", "South Ndebele");
			langs.Add("nd", "North Ndebele");
			langs.Add("ng", "Ndonga");
			langs.Add("ne", "Nepali");
			langs.Add("nn", "Nynorsk");
			langs.Add("nb", "Norwegian (NB)");
			langs.Add("no", "Norwegian (NO)");
			langs.Add("ny", "Nyanja");
			langs.Add("oc", "Occitan");
			langs.Add("oj", "Ojibwa");
			langs.Add("or", "Oriya");
			langs.Add("om", "Oromo");
			langs.Add("os", "Ossetic");
			langs.Add("pa", "Panjabi");
			langs.Add("fa", "Persian");
			langs.Add("pi", "Pali");
			langs.Add("pl", "Polish");
			langs.Add("pt", "Portuguese");
			langs.Add("ps", "Pushto");
			langs.Add("qu", "Quechua");
			langs.Add("rm", "Romansh");
			langs.Add("ro", "Romanian");
			langs.Add("rn", "Rundi");
			langs.Add("ru", "Russian");
			langs.Add("sg", "Sango");
			langs.Add("sa", "Sanskrit");
			langs.Add("si", "Sinhala");
			langs.Add("sk", "Slovak");
			langs.Add("sl", "Slovenian");
			langs.Add("se", "Northern Sami");
			langs.Add("sm", "Samoan");
			langs.Add("sn", "Shona");
			langs.Add("sd", "Sindhi");
			langs.Add("so", "Somali");
			langs.Add("st", "Sotho, Southern");
			langs.Add("es", "Spanish");
			langs.Add("sc", "Sardinian");
			langs.Add("sr", "Serbian");
			langs.Add("ss", "Swati");
			langs.Add("su", "Sundanese");
			langs.Add("sw", "Swahili");
			langs.Add("sv", "Swedish");
			langs.Add("ty", "Tahitian");
			langs.Add("ta", "Tamil");
			langs.Add("tt", "Tatar");
			langs.Add("te", "Telugu");
			langs.Add("tg", "Tajik");
			langs.Add("tl", "Tagalog");
			langs.Add("th", "Thai");
			langs.Add("bo", "Tibetan");
			langs.Add("ti", "Tigrinya");
			langs.Add("to", "Tonga");
			langs.Add("tn", "Tswana");
			langs.Add("ts", "Tsonga");
			langs.Add("tk", "Turkmen");
			langs.Add("tr", "Turkish");
			langs.Add("tw", "Twi");
			langs.Add("ug", "Uighur; Uyghur");
			langs.Add("uk", "Ukrainian");
			langs.Add("ur", "Urdu");
			langs.Add("uz", "Uzbek");
			langs.Add("ve", "Venda");
			langs.Add("vi", "Vietnamese");
			langs.Add("vo", "VolapÃ¼k");
			langs.Add("cy", "Welsh");
			langs.Add("wa", "Walloon");
			langs.Add("wo", "Wolof");
			langs.Add("xh", "Xhosa");
			langs.Add("yi", "Yiddish");
			langs.Add("yo", "Yoruba");
			langs.Add("za", "Zhuang; Chuang");
			langs.Add("zu", "Zulu");

			foreach(var v in langs)
			{
				cb_SpellcheckLanguage.Items.Add(v.Value);
			}

			cb_SpellcheckLanguage.SelectedValue = langs[Properties.Settings.Default.s_tbLanguage];
		}

		private void MainWindow_Loaded(object sender, RoutedEventArgs e)
		{
			nightMode = Properties.Settings.Default.s_NightMode;
			loadLangs();
			loadSettings();
			lbl_LoggedInAs.Content += username;
			int y = DateTime.Now.Year;
			for(int i = 0; i < 10; i++)
			{
				cb_Years.Items.Add("" + (y - i));
			}

			try
			{	//1
				cb_Years.SelectedIndex = 0;
				selectedYear = Convert.ToInt32(cb_Years.Text);

				cb_Months.SelectedIndex = DateTime.Now.Month - 1;
				selectedMonth = cb_Months.SelectedIndex + 1;
			}
			catch { Console.WriteLine("Load | 1"); }

			try
			{	//2
				loadDays(selectedYear, selectedMonth);
				selectedDay = 1;
			}
			catch { Console.WriteLine("Load | 2"); }

			try
			{	//3
				G_MouseDown(c_DatePicker.Items[0] as Grid, null);
			}
			catch { Console.WriteLine("Load | 3"); }

			Popup MyPopup = FindChild<Popup>(dp_Date, "PART_Popup");

			// Get Calendar in child of Popup
			MyCalendar = (Calendar)MyPopup.Child;
			MyCalendar.DisplayDateChanged += MyCalendar_DisplayDateChanged;
			MyCalendar.SelectionMode = CalendarSelectionMode.SingleDate;

			CalendarBackGround = new c_CalendarBackground(MyCalendar);
			CalendarBackGround.AddOverlay("0", "circle.png");
			CalendarBackGround.AddOverlay("1", "square.png");
			CalendarBackGround.grayoutweekends = "";

			loadImages();

			lv_DateList.MouseDoubleClick += Lv_DateList_MouseDoubleClick;
			lv_DateList.MouseDown += Lv_DateList_MouseDown;
		}

		private void Lv_DateList_MouseDown(object sender, MouseButtonEventArgs e)
		{
			lv_DateList.SelectedItem = null;
		}

		private void Lv_DateList_MouseDoubleClick(object sender, MouseButtonEventArgs e)
		{
			if(lv_DateList.SelectedItem != null) { jumpToDate(); }
		}

		public SolidColorBrush colortobrush(System.Drawing.Color c)
		{
			return new SolidColorBrush(new Color() { A = c.A, R = c.R, G = c.G, B = c.B });
		}

		public void loadSettings()
		{
			if (File.Exists("ibeam.cur"))
			{
				myCursor = new Cursor(File.OpenRead(System.AppDomain.CurrentDomain.BaseDirectory + "ibeam.cur"));
			}

			tb_Data.CaretBrush = nightMode ? Brushes.WhiteSmoke : Brushes.Red;
			tb_Title.CaretBrush = tb_Data.CaretBrush;

			useLightCursor = Properties.Settings.Default.s_textboxLightCursor;
			lightCursorWithNightModeOnly = Properties.Settings.Default.s_textboxLightCursorNightOnly;

			if (useLightCursor && (!lightCursorWithNightModeOnly || nightMode))
			{
				tb_Data.Cursor = myCursor;
				tb_Title.Cursor = myCursor;
			}
			else
			{
				tb_Data.Cursor = Cursors.IBeam;
				tb_Title.Cursor = Cursors.IBeam;
			}
			
			System.Drawing.Color c = nightMode ? Properties.Settings.Default.s_night_DayColor : Properties.Settings.Default.s_day_DayColor;
			dayColor = colortobrush(c);

			c = nightMode ? Properties.Settings.Default.s_night_SundayColor : Properties.Settings.Default.s_day_SundayColor;
			sundayColor = colortobrush(c);

			c = nightMode ? Properties.Settings.Default.s_night_MainBackgroundColor : Properties.Settings.Default.s_day_MainBackgroundColor;
			mainBackgroundColor = colortobrush(c);

			c = nightMode ? Properties.Settings.Default.s_night_TextboxBackgroundColor : Properties.Settings.Default.s_day_TextboxBackgroundColor;
			textboxBackgroundColor = colortobrush(c);

			c = nightMode ? Properties.Settings.Default.s_night_TextColor : Properties.Settings.Default.s_day_TextColor;
			textColor = colortobrush(c);

			c = nightMode ? Properties.Settings.Default.s_night_DayTextColor : Properties.Settings.Default.s_day_DayTextColor;
			dayTextColor = colortobrush(c);

			c = nightMode ? Properties.Settings.Default.s_night_SelectedDayColor : Properties.Settings.Default.s_day_SelectedDayColor;
			selectedDayColor = colortobrush(c);

			c = nightMode ? Properties.Settings.Default.s_night_TextSelectionBrush : Properties.Settings.Default.s_day_TextSelectionBrush;
			textSelectionColor = colortobrush(c);

			foreach(var v in langs)
			{
				Console.WriteLine(System.Windows.Markup.XmlLanguage.GetLanguage(v.Key));
			}

			Background = mainBackgroundColor;
			tb_Data.Background = textboxBackgroundColor;
			tb_Title.Background = textboxBackgroundColor;
			tb_Data.Foreground = textColor;
			tb_Title.Foreground = textColor;
			tb_Data.SelectionBrush = textSelectionColor;
			tb_Data.TextWrapping = Properties.Settings.Default.s_tbWordWrap ? TextWrapping.Wrap : TextWrapping.NoWrap;
			tb_Data.Language = System.Windows.Markup.XmlLanguage.GetLanguage(Properties.Settings.Default.s_tbLanguage);
			tb_Data.Padding = new Thickness(5, 5, 5, 5);
			tb_Data.AutoWordSelection = true;
			tb_Data.IsInactiveSelectionHighlightEnabled = true;
			tb_Data.IsUndoEnabled = true;
			tb_Data.UndoLimit = 1000;

			if (Properties.Settings.Default.s_PanelClosed) { hidePanel(); } else { showPanel(); }
			
		}

		public void loadSaveImage()
		{
			//Save floppy
			byte[] binaryData = Convert.FromBase64String(Properties.Resources.floppy_image);
			BitmapImage bi = new BitmapImage();
			bi.BeginInit();
			bi.StreamSource = new MemoryStream(binaryData);
			bi.EndInit();
			i_Save.Source = bi;
		}

		public void loadImages()
		{
			//Day/Night mode
			byte[] binaryData = Convert.FromBase64String(Properties.Resources.day_night_image);
			BitmapImage bi2 = new BitmapImage();
			bi2.BeginInit();
			bi2.StreamSource = new MemoryStream(binaryData);
			bi2.EndInit();
			btn_NightMode.Source = bi2;
		}

		public void hideImage()
		{
			i_Save.Source = null;
		}

		public string getDay(int year, int month, int day)
		{
			string ret = "";

			DateTime d = new DateTime(year, month, day);
			ret = d.DayOfWeek.ToString();

			return ret;
		}

		public void loadDays(int year, int month)
		{
			days.Clear();
			c_DatePicker.Items.Clear();
			int daysInMonth = DateTime.DaysInMonth(year, month);

			for(int i = 0; i < daysInMonth; i++)
			{
				

				Grid g = new Grid();
				g.Name = "day_" + (i + 1);
				g.Background = dayColor;
				
				g.Width = 227;
				g.Height = 30;
				g.Margin = new Thickness(-7, -3, 0, 0);

				Label l = new Label()
				{
					FontSize = 22,
					FontFamily = new FontFamily("Seriff"),
					Content = (i + 1).ToString().PadLeft(2, '0'),
					Foreground = dayTextColor
				};

				string __day = getDay(year, month, i + 1);

				Label ll = new Label() {
					FontSize = 17,
					Margin = new Thickness(25, 0, 0, 0),
					Content = " - " + __day,
					Foreground = dayTextColor
				};

				g.Children.Add(l);
				g.Children.Add(ll);

				g.Tag = new int[] { year, month, (i + 1) };

				g.MouseDown += G_MouseDown;
				days.Add(i + 1, g);

				c_DatePicker.Items.Add(g);
			}

			updateDays();
		}

		public void updateDays()
		{
			foreach(var v in c_DatePicker.Items)
			{
				Grid g = null;
				if(v is Grid) { g = v as Grid; }

				
				if(g != null)
				{
					int[] date = g.Tag as int[];

					Dictionary<int, string> days = c_DBManager.getDaysForUser(sqlc, selectedYear, selectedMonth, userid);
					Label star = new Label() { Foreground = dayTextColor, Name = "STAR", HorizontalAlignment = HorizontalAlignment.Right, VerticalAlignment = VerticalAlignment.Top, FontSize = 22, Content = "*", Margin = new Thickness(0, 0, 3, 0) };
					bool hasstar = false;

					List<Rectangle> rectcon = new List<Rectangle>();

					foreach (var vvv in g.Children)
					{
						if (vvv as Rectangle != null)
						{
							rectcon.Add(vvv as Rectangle);
						}
					}

					for(int i = 0; i < rectcon.Count; i++) { g.Children.Remove(rectcon[i]); rectcon[i] = null; }

					if (days.ContainsKey(date[2]))
					{
						foreach (var vvv in g.Children)
						{
							
							if (vvv as Label != null)
							{
								if ((vvv as Label).Name == "STAR")
								{
									hasstar = true;
								}
							}
						}
						if (!hasstar) g.Children.Add(star);
					}

					if (getDay(date[0],date[1],date[2]).ToLower() == "sunday")
					{
						g.Background = sundayColor;
						foreach(var vv in g.Children) { if (vv as Label != null) { (vv as Label).Foreground = Brushes.White; } }	
					}
					else
					{
						g.Background = dayColor;
					}

					
				}
			}

			GC.Collect();
		}

		private void G_MouseDown(object sender, MouseButtonEventArgs e)
		{
			loadSelectedDate(sender as Grid);
		}

		public void loadSelectedDate(Grid grid)
		{
			if(grid == null) { return; }
			if (askBeforeChange && (tb_Data.Text != loadedData || tb_Title.Text != loadedTitle))
			{
				MessageBoxResult res = MessageBox.Show("There are unsaved changes for the selected day.\r\nDo you want to save your changes before continuing?", "Unsaved Changes!", MessageBoxButton.YesNoCancel, MessageBoxImage.Warning);
				if (res == MessageBoxResult.Yes) { btn_Save_Click(null, null); }
				else if (res == MessageBoxResult.Cancel) { return; }
			}

			Grid s = grid;

			if (s != null && s.Tag != null)
			{
				int[] date = s.Tag as int[];
				if (date != null && date.Length == 3)
				{
					try
					{
						selectedYear = date[0];
						selectedMonth = date[1];
						selectedDay = date[2];

						updateDays();

						s.Children.Add(new Rectangle() { Width = 229,	Height = 2,		Fill = selectedDayColor, VerticalAlignment = VerticalAlignment.Bottom, HorizontalAlignment = HorizontalAlignment.Left, Margin = new Thickness(0, 0, 0, 0) });
						s.Children.Add(new Rectangle() { Width = 4,		Height = 30,	Fill = selectedDayColor, VerticalAlignment = VerticalAlignment.Bottom, HorizontalAlignment = HorizontalAlignment.Left, Margin = new Thickness(223, 0, 0, 0) });

						getData(date[0], date[1], date[2], userid);
					}
					catch (Exception ex)
					{
						Console.WriteLine(ex.ToString());
					}
				}
			}
			g_MainGrid.Children[g_MainGrid.Children.IndexOf(tb_Data)].Focus();
		}

		public void getData(int year, int month, int day, int _userid)
		{
			lbl_CurrentDate.Content = year + ". " + month.ToString().PadLeft(2, '0') + ". " + day.ToString().PadLeft(2, '0') + ". ";

			try
			{
				string data = c_DBManager.getData(sqlc, year, month, day, _userid);

				loadedData = "";
				loadedTitle = "";
				loadedDate = "";
				tb_Title.Text = "";
				tb_Data.Text = "";
				lbl_LastUpdated.Content = "";

				if (data != "")
				{
					XmlDocument xd = new XmlDocument();
					xd.LoadXml(data);
					loadedTitle = xd.GetElementsByTagName("title")[0].InnerText;
					loadedData = xd.GetElementsByTagName("data")[0].InnerText;
					loadedDate = xd.GetElementsByTagName("lupd")[0].InnerText;

					loadedTitle = loadedTitle == "" ? "" : c_DBManager.decryptData(loadedTitle, username);
					loadedData = loadedData == "" ? "" : c_DBManager.decryptData(loadedData, username);
				}
				tb_Title.Text = loadedTitle;
				tb_Data.Text = loadedData;
				if (loadedDate != "") { lbl_LastUpdated.Content = "Last updated: " + loadedDate; }
				hideImage();
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.ToString());
			}
		}

		public void hidePanel()
		{
			c_DatePicker.Visibility = Visibility.Hidden;
			cb_Months.Visibility = Visibility.Hidden;
			cb_Years.Visibility = Visibility.Hidden;
			btn_year_minus.Visibility = Visibility.Hidden;
			btn_year_plus.Visibility = Visibility.Hidden;
			dp_Date.Visibility = Visibility.Visible;
			btn_ShowHidePanel.Content = "📓";

			lbl_title_label.Margin = new Thickness(btn_ShowHidePanel.Width + 10, lbl_title_label.Margin.Top, 0, 0);
			tb_Title.Margin = new Thickness(btn_ShowHidePanel.Width + lbl_title_label.Width + 20, tb_Title.Margin.Top, tb_Title.Margin.Right, tb_Title.Margin.Bottom);
			tb_Data.Margin = new Thickness(tb_Title.Margin.Left - (lbl_title_label.Width + 10), tb_Data.Margin.Top, tb_Data.Margin.Right, tb_Data.Margin.Bottom);
			btn_Clear.Margin = new Thickness(tb_Title.Margin.Left, btn_Clear.Margin.Top, btn_Clear.Margin.Right, btn_Clear.Margin.Bottom);
		}

		public void showPanel()
		{
			c_DatePicker.Visibility = Visibility.Visible;
			cb_Months.Visibility = Visibility.Visible;
			cb_Years.Visibility = Visibility.Visible;
			btn_year_minus.Visibility = Visibility.Visible;
			btn_year_plus.Visibility = Visibility.Visible;
			dp_Date.Visibility = Visibility.Hidden;
			btn_ShowHidePanel.Content = "📅";

			lbl_title_label.Margin = new Thickness(c_DatePicker.Width + 10, lbl_title_label.Margin.Top, 0, 0);
			tb_Title.Margin = new Thickness(c_DatePicker.Width + lbl_title_label.Width + 20, tb_Title.Margin.Top, tb_Title.Margin.Right, tb_Title.Margin.Bottom);
			tb_Data.Margin = new Thickness(tb_Title.Margin.Left - (lbl_title_label.Width + 10), tb_Data.Margin.Top, tb_Data.Margin.Right, tb_Data.Margin.Bottom);
			btn_Clear.Margin = new Thickness(tb_Title.Margin.Left, btn_Clear.Margin.Top, btn_Clear.Margin.Right, btn_Clear.Margin.Bottom);
		}

		private void btn_ShowHidePanel_Click(object sender, RoutedEventArgs e)
		{
			if(c_DatePicker.Visibility == Visibility.Visible)
			{
				hidePanel();

				Properties.Settings.Default.s_PanelClosed = true;
			}
			else
			{
				showPanel();

				Properties.Settings.Default.s_PanelClosed = false;
			}

			Properties.Settings.Default.Save();
		}

		public void manageDays(int _year_)
		{
			try
			{
				if (cb_Months.SelectedIndex >= 0)
				{
					selectedYear = _year_;
					selectedMonth = cb_Months.SelectedIndex + 1;
					loadDays(selectedYear, selectedMonth);
				}
			}
			catch
			{

			}
		}

		private void cb_Months_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			manageDays(selectedYear);
		}

		private void btn_Save_Click(object sender, RoutedEventArgs e)
		{
			c_DBManager.addData(sqlc, selectedYear, selectedMonth, selectedDay, (tb_Title.Text == "" ? "" : c_DBManager.encryptData(tb_Title.Text, username)), (tb_Data.Text == "" ? "" : c_DBManager.encryptData(tb_Data.Text, username)), userid);
			loadDays(selectedYear, selectedMonth);
			getData(selectedYear, selectedMonth, selectedDay, userid);
		}

		private void tb_Data_TextChanged(object sender, TextChangedEventArgs e)
		{
			if(tb_Data.Text != loadedData || tb_Title.Text != loadedTitle) { loadSaveImage(); }
			else { hideImage(); }
		}

		private void tb_Title_TextChanged(object sender, TextChangedEventArgs e)
		{
			if (tb_Data.Text != loadedData || tb_Title.Text != loadedTitle) { loadSaveImage(); }
			else { hideImage(); }
		}

		private void dp_Date_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
		{
			selectedYear = dp_Date.SelectedDate.Value.Year;
			selectedMonth = dp_Date.SelectedDate.Value.Month;
			selectedDay = dp_Date.SelectedDate.Value.Day;

			getData(selectedYear, selectedMonth, selectedDay, userid);
		}

		private void dp_Date_CalendarOpened(object sender, RoutedEventArgs e)
		{
			getSelectedDates();
		}

		public void getSelectedDates()
		{
			CalendarBackGround.ClearDates();

			DateTime selectedDate = MyCalendar.DisplayDate;

			Dictionary<int, string> _days = c_DBManager.getDaysForUser(sqlc, selectedDate.Year, selectedDate.Month, userid);

			CalendarBackGround.AddDate(new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day), "1");

			foreach (KeyValuePair<int, string> kvp in _days)
			{
				try
				{
					CalendarBackGround.AddDate(new DateTime(selectedDate.Year, selectedDate.Month, kvp.Key), "0");
				}
				catch
				{

				}
			}

			MyCalendar.Background = CalendarBackGround.GetBackground();
			MyCalendar.IsTodayHighlighted = false;
		}

		private void MyCalendar_DisplayDateChanged(object sender, CalendarDateChangedEventArgs e)
		{
			selectedYear = MyCalendar.DisplayDate.Year;
			cb_Years.Text = selectedYear + "";
			selectedMonth = MyCalendar.DisplayDate.Month;
			cb_Months.SelectedIndex = selectedMonth - 1;
			getSelectedDates();
		}

		public static T FindChild<T>(DependencyObject parent, string childName) where T : DependencyObject
		{
			if (parent == null)
			{
				return null;
			}

			T foundChild = null;

			int childrenCount = VisualTreeHelper.GetChildrenCount(parent);

			for (int i = 0; i < childrenCount; i++)
			{
				var child = VisualTreeHelper.GetChild(parent, i);
				T childType = child as T;

				if (childType == null)
				{
					foundChild = FindChild<T>(child, childName);

					if (foundChild != null) break;
				}
				else
					if (!string.IsNullOrEmpty(childName))
				{
					var frameworkElement = child as FrameworkElement;

					if (frameworkElement != null && frameworkElement.Name == childName)
					{
						foundChild = (T)child;
						break;
					}
					else
					{
						foundChild = FindChild<T>(child, childName);

						if (foundChild != null)
						{
							break;
						}
					}
				}
				else
				{
					foundChild = (T)child;
					break;
				}
			}

			return foundChild;
		}

		private void cb_Years_KeyUp(object sender, KeyEventArgs e)
		{
			tryYear();
		}

		public void tryYear()
		{
			if (cb_Years.Text.Length == 4)
			{
				int i = 0;
				if (int.TryParse(cb_Years.Text, out i) && i > 1900)
				{
					selectedYear = i;
					loadDays(selectedYear, selectedMonth);
					updateDays();
				}
			}

			if (cb_Years.Text != "" && !int.TryParse(cb_Years.Text, out int ii))
			{
				cb_Years.Text = selectedYear + "";
				cb_Years_KeyUp(null, null);
			}
		}

		private void cb_Years_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			try
			{
				manageDays(Convert.ToInt32(e.AddedItems[0]));
			}
			catch
			{

			}
		}

		private void btn_NightMode_MouseUp(object sender, MouseButtonEventArgs e)
		{
			nightMode = !nightMode;
			loadSettings();
			loadDays(selectedYear, selectedMonth);
			updateDays();
			Properties.Settings.Default.s_NightMode = nightMode;
			Properties.Settings.Default.Save();
			loadSelectedDate(days[selectedDay]);
		}

		private void btn_Settings_Click(object sender, RoutedEventArgs e)
		{
			SettingsWindow sw = new SettingsWindow();
			sw.ShowDialog();
			loadSettings();
			loadDays(selectedYear, selectedMonth);
			updateDays();
		}

		private void Btn_year_minus_Click(object sender, RoutedEventArgs e)
		{
			int val = 0;
			if(int.TryParse(cb_Years.Text,out val))
			{
				val--;
				cb_Years.Text = val + "";
				tryYear();
			}
		}

		private void Btn_year_plus_Click(object sender, RoutedEventArgs e)
		{
			int val = 0;
			if (int.TryParse(cb_Years.Text, out val))
			{
				val++;
				cb_Years.Text = val + "";
				tryYear();
			}
		}

		private void Btn_ListDaysWithData_Click(object sender, RoutedEventArgs e)
		{
			g_Dates.Visibility = Visibility.Visible;
			datesTitle.Content += username;
			gFillDates();
		}

		public void gFillDates()
		{
			int i = 0;
			Dictionary<DateTime,string> dates = c_DBManager.getDatesForUser(sqlc, userid, username);
			foreach(KeyValuePair<DateTime,string> kvp in dates)
			{
				i++;
				lv_DateList.Items.Add(new { id = i, date = kvp.Key.ToShortDateString(), title = kvp.Value });
			}
		}

		private void Btn_Search_Click(object sender, RoutedEventArgs e)
		{
			showHideSearchPanel();
		}

		public void showHideSearchPanel()
		{
			misc msc = new misc();
			if(cb_SearchPanel.Width == 0)
			{
				msc.showControl(cb_SearchPanel, 250, 5);
				//TODO: fill cb with possible results!
				cb_SearchPanel.Focus();
			}
			else
			{
				msc.hideControl(cb_SearchPanel, 5);
			}
		}

		private void Cb_SearchPanel_PreviewTextInput(object sender, TextCompositionEventArgs e)
		{
			//TODO:!!
		}

		private void Cb_SearchPanel_KeyUp(object sender, KeyEventArgs e)
		{
			if (e.Key == Key.Escape)
			{
				cb_SearchPanel.Text = "";
				showHideSearchPanel();
			}
			else if (e.Key == Key.Enter)
			{
				//TODO:!!
			}
			else
			{

			}
		}

		private void Btn_CancelDate_Click(object sender, RoutedEventArgs e)
		{
			hideDates();	
		}

		public void hideDates()
		{
			g_Dates.Visibility = Visibility.Hidden;
			lv_DateList.Items.Clear();
		}

		private void Btn_JumpToDate_Click(object sender, RoutedEventArgs e)
		{
			jumpToDate();
		}

		public void jumpToDate()
		{
			string d = lv_DateList.SelectedValue.ToString();
			Regex r = new Regex(@"\d{4}\.\d{2}\.\d{2}");
			if (r.IsMatch(d)) { d = r.Matches(d)[0].Value; }
			DateTime dd = DateTime.Now;
			try
			{
				dd = Convert.ToDateTime(d);
			}
			catch { }
			hideDates();
			jumpToDate(dd);
		}

		public void jumpToDate(DateTime date)
		{
			int y = date.Year;
			int m = date.Month;
			int d = date.Day;

			cb_Years.Text = y.ToString();
			tryYear();
			cb_Months.SelectedIndex = m - 1;
			cb_Months_SelectionChanged(null, null);
			selectedDay = d;
			loadSelectedDate(days[d]);
		}

		private void Cb_SpellcheckLanguage_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			List<string> lang = langs.Where(y => y.Value == cb_SpellcheckLanguage.SelectedValue).Select(x => x.Key).ToList();
			if(lang.Count > 0) { Properties.Settings.Default.s_tbLanguage = lang[0]; Properties.Settings.Default.Save(); }
		}
	}

	public partial class myObject : Object
	{
		public DateTime Tag { get; set; }
	}
}
