using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using Newtonsoft.Json;

namespace DailyDiary.Properties
{
	/// <summary>
	/// Settings container class
	/// </summary>
	public class mySettings_Settings
	{
		//Main
		public string username										{ get; set; }
		public int userid											{ get; set; }

		//Colors
		public System.Drawing.Color c_day_Day 						{ get; set; }
		public System.Drawing.Color c_night_Day 					{ get; set; }
		public System.Drawing.Color c_day_MainBackground 			{ get; set; }
		public System.Drawing.Color c_night_MainBackground 			{ get; set; }
		public System.Drawing.Color c_day_SelectedDay 				{ get; set; }
		public System.Drawing.Color c_night_SelectedDay 			{ get; set; }
		public System.Drawing.Color c_day_Sunday 					{ get; set; }
		public System.Drawing.Color c_night_Sunday 					{ get; set; }
		public System.Drawing.Color c_day_TextboxBackground 		{ get; set; }
		public System.Drawing.Color c_night_TextboxBackground 		{ get; set; }
		public System.Drawing.Color c_day_Text 						{ get; set; }
		public System.Drawing.Color c_night_Text 					{ get; set; }
		public System.Drawing.Color c_day_TextSelection 			{ get; set; }
		public System.Drawing.Color c_night_TextSelection 			{ get; set; }
		public System.Drawing.Color c_day_DayText 					{ get; set; }
		public System.Drawing.Color c_night_DayText 				{ get; set; }

		//Booleans
		public bool b_AskBeforeChange 								{ get; set; }
		public bool b_NightMode 									{ get; set; }
		public bool b_PanelClosed 									{ get; set; }
		public bool b_SingleUserMode 								{ get; set; }
		public bool b_SpellCheck 									{ get; set; }
		public bool b_TextBox_WordWrap 								{ get; set; }
		public bool b_TextBox_LightCursor 							{ get; set; }
		public bool b_TextBox_LightCursor_NightOnly 				{ get; set; }

		//Misc
		public int TextBox_Align 									{ get; set; } = 1;
		public string TextBox_Language 								{ get; set; }
		public string SingleUser_UserName 							{ get; set; }
	}

	public class mySettings {
		public System.Data.SQLite.SQLiteConnection sqlc { get; set; }
		public string username { get; set; }
		public int userid { get; set; }

		/// <summary>
		/// Mysettings_settings which contains the actual values
		/// </summary>
		private mySettings_Settings _mySettings = null;

		/// <summary>
		/// Initialize a new mySettings object
		/// </summary>
		/// <param name="MySettings">Mysettings settings container class</param>
		public mySettings(mySettings_Settings MySettings)
		{
			_mySettings = MySettings;
		}

		/// <summary>
		/// Gets color from settings as a brush
		/// </summary>
		/// <param name="type">Color type to set</param>
		/// <param name="variant">Set day or night variant. Default is day</param>
		/// <returns>SolidColorBrush, color of color type per variant</returns>
		public SolidColorBrush getBrush(colors type, string variant = "day")
		{
			SolidColorBrush bru = null;

			switch (type)
			{
				case colors.Day:				bru = colorToBrush(variant == "day" ? _mySettings.c_day_Day					: _mySettings.c_night_Day);
					break;
				case colors.DayText:			bru = colorToBrush(variant == "day" ? _mySettings.c_day_DayText				: _mySettings.c_night_DayText);
					break;
				case colors.MainBackground:		bru = colorToBrush(variant == "day" ? _mySettings.c_day_MainBackground		: _mySettings.c_night_MainBackground);
					break;
				case colors.SelectedDay:		bru = colorToBrush(variant == "day" ? _mySettings.c_day_SelectedDay			: _mySettings.c_night_SelectedDay);
					break;
				case colors.Sunday:				bru = colorToBrush(variant == "day" ? _mySettings.c_day_Sunday				: _mySettings.c_night_Sunday);
					break;
				case colors.Text:				bru = colorToBrush(variant == "day" ? _mySettings.c_day_Text				: _mySettings.c_night_Text);
					break;
				case colors.TextboxBackground:	bru = colorToBrush(variant == "day" ? _mySettings.c_day_TextboxBackground	: _mySettings.c_night_TextboxBackground);
					break;
				case colors.TextSelection:		bru = colorToBrush(variant == "day" ? _mySettings.c_day_TextSelection		: _mySettings.c_night_TextSelection);
					break;
			}

			return bru;
		}

		/// <summary>
		/// Set the colors in the settings for each type both day and night coloration
		/// </summary>
		/// <param name="c">Color to set</param>
		/// <param name="type">Color type to set</param>
		/// <param name="variant">Set day or night variant. Default is day</param>
		public void setColor(System.Drawing.Color c, colors type, string variant = "day")
		{
				if(variant == "day") { 
				switch (type)
				{
					case colors.Day:				_mySettings.c_day_Day					= c;	break;
					case colors.DayText:			_mySettings.c_day_DayText				= c;	break;
					case colors.MainBackground:		_mySettings.c_day_MainBackground		= c;	break;
					case colors.SelectedDay:		_mySettings.c_day_SelectedDay			= c;	break;
					case colors.Sunday:				_mySettings.c_day_Sunday				= c;	break;
					case colors.Text:				_mySettings.c_day_Text					= c;	break;
					case colors.TextboxBackground:	_mySettings.c_day_TextboxBackground		= c;	break;
					case colors.TextSelection:		_mySettings.c_day_TextSelection			= c;	break;
				}
			}
			else
			{
				switch (type)
				{
					case colors.Day:				_mySettings.c_night_Day					= c;	break;
					case colors.DayText:			_mySettings.c_night_DayText				= c;	break;
					case colors.MainBackground:		_mySettings.c_night_MainBackground		= c;	break;
					case colors.SelectedDay:		_mySettings.c_night_SelectedDay			= c;	break;
					case colors.Sunday:				_mySettings.c_night_Sunday				= c;	break;
					case colors.Text:				_mySettings.c_night_Text				= c;	break;
					case colors.TextboxBackground:	_mySettings.c_night_TextboxBackground	= c;	break;
					case colors.TextSelection:		_mySettings.c_night_TextSelection		= c;	break;
				}
			}
		}

		/// <summary>
		/// Converts Color to SolidColorBrush
		/// </summary>
		/// <param name="c">Color to convert</param>
		/// <returns>SolidColorBrush, from input colors ARGB values</returns>
		private SolidColorBrush colorToBrush(System.Drawing.Color c)
		{
			return new SolidColorBrush(new Color() { A = c.A, R = c.R, G = c.G, B = c.B });
		}

		/// <summary>
		/// Cleans strings and makes sure the value is not null
		/// </summary>
		private string clean(string input)
		{
			return input.Trim() ?? "<NULL>";
		}

		/// <summary>
		/// Returns a filled settings for a specific userid if it has one associated
		/// </summary>
		/// <param name="userid">The users id to get the settings for</param>
		/// <returns>full settings</returns>
		public mySettings_Settings getSettings()
		{
			fillSettings(c_DBManager.getSettings(sqlc, userid));
			return _mySettings;
		}

		/// <summary>
		/// Fills the setting container with values deserialized from the database entry
		/// </summary>
		/// <param name="settings">string contained in DB</param>
		/// <returns>true if success, false otherwise</returns>
		public bool fillSettings(string settings)
		{
			if(settings == "") { return false; }
				var definition = new
				{
					settings = new
					{
						username 							=	"",
						userid 								=	0,
						c_day_Day 							=	new JsonColor(0,0,0,0),
						c_night_Day 						=	new JsonColor(0,0,0,0),
						c_day_MainBackground 				=	new JsonColor(0,0,0,0),
						c_night_MainBackground 				=	new JsonColor(0,0,0,0),
						c_day_SelectedDay 					=	new JsonColor(0,0,0,0),
						c_night_SelectedDay 				=	new JsonColor(0,0,0,0),
						c_day_Sunday 						=	new JsonColor(0,0,0,0),
						c_night_Sunday 						=	new JsonColor(0,0,0,0),
						c_day_TextboxBackground 			=	new JsonColor(0,0,0,0),
						c_night_TextboxBackground 			=	new JsonColor(0,0,0,0),
						c_day_Text 							=	new JsonColor(0,0,0,0),
						c_night_Text 						=	new JsonColor(0,0,0,0),
						c_day_TextSelection 				=	new JsonColor(0,0,0,0),
						c_night_TextSelection 				=	new JsonColor(0,0,0,0),
						c_day_DayText 						=	new JsonColor(0,0,0,0),
						c_night_DayText =						new JsonColor(0,0,0,0),
						b_AskBeforeChange 					=	true,
						b_NightMode 						=	true,
						b_PanelClosed 						=	true,
						b_SingleUserMode 					=	true,
						b_SpellCheck 						=	true,
						b_TextBox_WordWrap 					=	true,
						b_TextBox_LightCursor 				=	true,
						b_TextBox_LightCursor_NightOnly 	=	true,
						TextBox_Align 						=	0,
						TextBox_Language 					=	"",
						SingleUser_UserName 				=	""
					}
				};

			var o = JsonConvert.DeserializeAnonymousType(settings, definition);

			_mySettings.username							=	o.settings.username;
			_mySettings.userid								=	o.settings.userid;
			_mySettings.c_day_Day							=	o.settings.c_day_Day.Color;
			_mySettings.c_night_Day							=	o.settings.c_night_Day.Color;
			_mySettings.c_day_MainBackground				=	o.settings.c_day_MainBackground.Color;
			_mySettings.c_night_MainBackground				=	o.settings.c_night_MainBackground.Color;
			_mySettings.c_day_SelectedDay					=	o.settings.c_day_SelectedDay.Color;
			_mySettings.c_night_SelectedDay					=	o.settings.c_night_SelectedDay.Color;
			_mySettings.c_day_Sunday						=	o.settings.c_day_Sunday.Color;
			_mySettings.c_night_Sunday						=	o.settings.c_night_Sunday.Color;
			_mySettings.c_day_TextboxBackground				=	o.settings.c_day_TextboxBackground.Color;
			_mySettings.c_night_TextboxBackground			=	o.settings.c_night_TextboxBackground.Color;
			_mySettings.c_day_Text							=	o.settings.c_day_Text.Color;
			_mySettings.c_night_Text						=	o.settings.c_night_Text.Color;
			_mySettings.c_day_TextSelection					=	o.settings.c_day_TextSelection.Color;
			_mySettings.c_night_TextSelection				=	o.settings.c_night_TextSelection.Color;
			_mySettings.c_day_DayText						=	o.settings.c_day_DayText.Color;
			_mySettings.c_night_DayText						=	o.settings.c_night_DayText.Color;
			_mySettings.b_AskBeforeChange					=	o.settings.b_AskBeforeChange;
			_mySettings.b_NightMode							=	o.settings.b_NightMode;
			_mySettings.b_PanelClosed						=	o.settings.b_PanelClosed;
			_mySettings.b_SingleUserMode					=	o.settings.b_SingleUserMode;
			_mySettings.b_SpellCheck						=	o.settings.b_SpellCheck;
			_mySettings.b_TextBox_WordWrap					=	o.settings.b_TextBox_WordWrap;
			_mySettings.b_TextBox_LightCursor				=	o.settings.b_TextBox_LightCursor;
			_mySettings.b_TextBox_LightCursor_NightOnly		=	o.settings.b_TextBox_LightCursor_NightOnly;
			_mySettings.TextBox_Align						=	o.settings.TextBox_Align;
			_mySettings.TextBox_Language					=	o.settings.TextBox_Language;
			_mySettings.SingleUser_UserName					=	o.settings.SingleUser_UserName;

			return true;
		}
		
		/// <summary>
		/// Initializes the settings datarow for the user with the default values
		/// </summary>
		public bool initializeSettings()
		{
			try
			{
				string json = Properties.Resources.settings_JSON_Default;
				json = json.Replace("{username}",	username	).
							Replace("{userid}",		userid + ""	);

				fillSettings(json);
				setSettings();
				return true;
			}
			catch(Exception ex)
			{
				Console.Error.WriteLine(ex.ToString());
				return false;
			}
		}

		/// <summary>
		/// Checks if the user has a line in the settings table
		/// </summary>
		public bool hasSettings()
		{
			return c_DBManager.getSettings(sqlc, userid) != "<NULL>";
		}

		/// <summary>
		/// Sets the settings for the user in the settings table
		/// </summary>
		public bool setSettings()
		{
			return c_DBManager.setSettings(sqlc, userid, generateSettingsJson());
		}

		/// <summary>
		/// Generates settings entry for DB from data contained in the settings container
		/// </summary>
		/// <returns>String containing the data in a db acceptable format</returns>
		public string generateSettingsJson()
		{
			string json = Properties.Resources.settings_JSON_Template;

			json = json.Replace("{username}",							_mySettings.username									+ @""	).
						Replace("{userid}",								_mySettings.userid										+ @""	).
						Replace("{c_day_Day}",							_mySettings.c_day_Day.toJC().ARGB						+ @""	).
						Replace("{c_night_Day}",						_mySettings.c_night_Day.toJC().ARGB						+ @""	).
						Replace("{c_day_MainBackground}",				_mySettings.c_day_MainBackground.toJC().ARGB			+ @""	).
						Replace("{c_night_MainBackground}",				_mySettings.c_night_MainBackground.toJC().ARGB			+ @""	).
						Replace("{c_day_SelectedDay}",					_mySettings.c_day_SelectedDay.toJC().ARGB				+ @""	).
						Replace("{c_night_SelectedDay}",				_mySettings.c_night_SelectedDay.toJC().ARGB				+ @""	).
						Replace("{c_day_Sunday}",						_mySettings.c_day_Sunday.toJC().ARGB					+ @""	).
						Replace("{c_night_Sunday}",						_mySettings.c_night_Sunday.toJC().ARGB					+ @""	).
						Replace("{c_day_TextboxBackground}",			_mySettings.c_day_TextboxBackground.toJC().ARGB			+ @""	).
						Replace("{c_night_TextboxBackground}",			_mySettings.c_night_TextboxBackground.toJC().ARGB		+ @""	).
						Replace("{c_day_Text}",							_mySettings.c_day_Text.toJC().ARGB						+ @""	).
						Replace("{c_night_Text}",						_mySettings.c_night_Text.toJC().ARGB					+ @""	).
						Replace("{c_day_TextSelection}",				_mySettings.c_day_TextSelection.toJC().ARGB				+ @""	).
						Replace("{c_night_TextSelection}",				_mySettings.c_night_TextSelection.toJC().ARGB			+ @""	).
						Replace("{c_day_DayText}",						_mySettings.c_day_DayText.toJC().ARGB					+ @""	).
						Replace("{c_night_DayText}",					_mySettings.c_night_DayText.toJC().ARGB					+ @""	).
						Replace("{b_AskBeforeChange}",					_mySettings.b_AskBeforeChange							+ @""	).
						Replace("{b_NightMode}",						_mySettings.b_NightMode									+ @""	).
						Replace("{b_PanelClosed}",						_mySettings.b_PanelClosed								+ @""	).
						Replace("{b_SingleUserMode}",					_mySettings.b_SingleUserMode							+ @""	).
						Replace("{b_SpellCheck}",						_mySettings.b_SpellCheck								+ @""	).
						Replace("{b_TextBox_WordWrap}",					_mySettings.b_TextBox_WordWrap							+ @""	).
						Replace("{b_TextBox_LightCursor}",				_mySettings.b_TextBox_LightCursor						+ @""	).
						Replace("{b_TextBox_LightCursor_NightOnly}",	_mySettings.b_TextBox_LightCursor_NightOnly				+ @""	).
						Replace("{TextBox_Align}",						_mySettings.TextBox_Align								+ @""	).
						Replace("{TextBox_Language}",					_mySettings.TextBox_Language							+ @""	).
						Replace("{SingleUser_UserName}",				_mySettings.SingleUser_UserName							+ @""	);

			return json;
		}

	}

	/// <summary>
	/// Class to turn ARGB values into Color
	/// </summary>
	public class JsonColor
	{
		private byte A { get; set; }
		private byte R { get; set; }
		private byte G { get; set; }
		private byte B { get; set; }

		private System.Drawing.Color _color = System.Drawing.Color.White;
		public System.Drawing.Color Color { get { return _color; } }
		private string _argb = "{ A:255, R:255, G:255, B:255 }";
		public string ARGB { get { return _argb; } }

		public JsonColor(byte a, byte r, byte g, byte b)
		{
			A = a;
			R = r;
			G = g;
			B = b;
			
			setColor();
			setARGB();
		}

		private void setColor()
		{
			_color = System.Drawing.Color.FromArgb(A, R, G, B);
		}
		private void setARGB()
		{
			_argb = $"{{ A:{A}, R:{R}, G:{G}, B:{B} }}";
		}


	}

	/// <summary>
	/// All color types handled in the customization
	/// </summary>
	public enum colors
	{
		Day,
		DayText,
		MainBackground,
		SelectedDay,
		Sunday,
		Text,
		TextboxBackground,
		TextSelection
	}
}

