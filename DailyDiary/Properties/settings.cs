using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

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
		/// Fills the setting container with values deserialized from the database entry
		/// </summary>
		/// <param name="settings">string contained in DB</param>
		public void fillSettings(string settings)
		{
				var definition = new
				{
					settings = new
					{
						username 							=	"",
						userid 								=	0,
						c_day_Day 							=	new { A = "", R = "", G = "", B = "" },
						c_night_Day 						=	new { A = "", R = "", G = "", B = "" },
						c_day_MainBackground 				=	new { A = "", R = "", G = "", B = "" },
						c_night_MainBackground 				=	new { A = "", R = "", G = "", B = "" },
						c_day_SelectedDay 					=	new { A = "", R = "", G = "", B = "" },
						c_night_SelectedDay 				=	new { A = "", R = "", G = "", B = "" },
						c_day_Sunday 						=	new { A = "", R = "", G = "", B = "" },
						c_night_Sunday 						=	new { A = "", R = "", G = "", B = "" },
						c_day_TextboxBackground 			=	new { A = "", R = "", G = "", B = "" },
						c_night_TextboxBackground 			=	new { A = "", R = "", G = "", B = "" },
						c_day_Text 							=	new { A = "", R = "", G = "", B = "" },
						c_night_Text 						=	new { A = "", R = "", G = "", B = "" },
						c_day_TextSelection 				=	new { A = "", R = "", G = "", B = "" },
						c_night_TextSelection 				=	new { A = "", R = "", G = "", B = "" },
						c_day_DayText 						=	new { A = "", R = "", G = "", B = "" },
						c_night_DayText 					=	new { A = "", R = "", G = "", B = "" },
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

			string s =
	@"{ ""settings"" : {
				""username""                          : ""wolfyd"",
				""userid""                            : ""1130"",
				""c_day_Day""                         : {""A"" : 255, ""R"": 100, ""G"" : 100, ""B"" : 100},
				""c_night_Day""                       : {""A"" : 255, ""R"": 000, ""G"" : 255, ""B"" : 100},
				""c_day_MainBackground""              : {""A"" : 255, ""R"": 100, ""G"" : 100, ""B"" : 100},
				""c_night_MainBackground""            : {""A"" : 255, ""R"": 100, ""G"" : 100, ""B"" : 100},
				""c_day_SelectedDay""                 : {""A"" : 255, ""R"": 100, ""G"" : 100, ""B"" : 100},
				""c_night_SelectedDay""               : {""A"" : 255, ""R"": 100, ""G"" : 100, ""B"" : 100},
				""c_day_Sunday""                      : {""A"" : 255, ""R"": 100, ""G"" : 100, ""B"" : 100},
				""c_night_Sunday""                    : {""A"" : 255, ""R"": 100, ""G"" : 100, ""B"" : 100},
				""c_day_TextboxBackground""           : {""A"" : 255, ""R"": 100, ""G"" : 100, ""B"" : 100},
				""c_night_TextboxBackground""         : {""A"" : 255, ""R"": 100, ""G"" : 100, ""B"" : 100},
				""c_day_Text""                        : {""A"" : 255, ""R"": 100, ""G"" : 100, ""B"" : 100},
				""c_night_Text""                      : {""A"" : 255, ""R"": 100, ""G"" : 100, ""B"" : 100},
				""c_day_TextSelection""               : {""A"" : 255, ""R"": 100, ""G"" : 100, ""B"" : 100},
				""c_night_TextSelection""             : {""A"" : 255, ""R"": 100, ""G"" : 100, ""B"" : 100},
				""c_day_DayText""                     : {""A"" : 255, ""R"": 100, ""G"" : 100, ""B"" : 100},
				""c_night_DayText""                   : {""A"" : 255, ""R"": 100, ""G"" : 100, ""B"" : 100},
				""b_AskBeforeChange""                 : ""true"",
				""b_NightMode""                       : ""false"",
				""b_PanelClosed""                     : ""TRUE"",
				""b_SingleUserMode""                  : ""FALSE"",
				""b_SpellCheck""                      : ""TRUE"",
				""b_TextBox_WordWrap""                : ""TRUE"",
				""b_TextBox_LightCursor""             : ""true"",
				""b_TextBox_LightCursor_NightOnly""   : ""false"",
				""TextBox_Align""                     : ""-1"",
				""TextBox_Language""                  : ""en-UK"",
				""SingleUser_UserName""               : """"
			}
		}";
			settings = s;

			object o = JsonConvert.DeserializeAnonymousType(settings, definition);

		}

		/// <summary>
		/// Generates settings entry for DB from data contained in the settings container
		/// </summary>
		/// <returns>String containing the data in a db acceptable format</returns>
		public string generateSettingsJson()
		{
			string json = Properties.Resources.settings_JSON_Template;

			json = json.Replace("{username}",							@"""" + _mySettings.username							+ @"""").
						Replace("{userid}",								@"""" + _mySettings.userid								+ @"""").
						Replace("{c_day_Day}",							@"""" + _mySettings.c_day_Day							+ @"""").
						Replace("{c_night_Day}",						@"""" + _mySettings.c_night_Day							+ @"""").
						Replace("{c_day_MainBackground}",				@"""" + _mySettings.c_day_MainBackground				+ @"""").
						Replace("{c_night_MainBackground}",				@"""" + _mySettings.c_night_MainBackground				+ @"""").
						Replace("{c_day_SelectedDay}",					@"""" + _mySettings.c_day_SelectedDay					+ @"""").
						Replace("{c_night_SelectedDay}",				@"""" + _mySettings.c_night_SelectedDay					+ @"""").
						Replace("{c_day_Sunday}",						@"""" + _mySettings.c_day_Sunday						+ @"""").
						Replace("{c_night_Sunday}",						@"""" + _mySettings.c_night_Sunday						+ @"""").
						Replace("{c_day_TextboxBackground}",			@"""" + _mySettings.c_day_TextboxBackground				+ @"""").
						Replace("{c_night_TextboxBackground}",			@"""" + _mySettings.c_night_TextboxBackground			+ @"""").
						Replace("{c_day_Text}",							@"""" + _mySettings.c_day_Text							+ @"""").
						Replace("{c_night_Text}",						@"""" + _mySettings.c_night_Text						+ @"""").
						Replace("{c_day_TextSelection}",				@"""" + _mySettings.c_day_TextSelection					+ @"""").
						Replace("{c_night_TextSelection}",				@"""" + _mySettings.c_night_TextSelection				+ @"""").
						Replace("{c_day_DayText}",						@"""" + _mySettings.c_day_DayText						+ @"""").
						Replace("{c_night_DayText}",					@"""" + _mySettings.c_night_DayText						+ @"""").
						Replace("{b_AskBeforeChange}",					@"""" + _mySettings.b_AskBeforeChange					+ @"""").
						Replace("{b_NightMode}",						@"""" + _mySettings.b_NightMode							+ @"""").
						Replace("{b_PanelClosed}",						@"""" + _mySettings.b_PanelClosed						+ @"""").
						Replace("{b_SingleUserMode}",					@"""" + _mySettings.b_SingleUserMode					+ @"""").
						Replace("{b_SpellCheck}",						@"""" + _mySettings.b_SpellCheck						+ @"""").
						Replace("{b_TextBox_WordWrap}",					@"""" + _mySettings.b_TextBox_WordWrap					+ @"""").
						Replace("{b_TextBox_LightCursor}",				@"""" + _mySettings.b_TextBox_LightCursor				+ @"""").
						Replace("{b_TextBox_LightCursor_NightOnly}",	@"""" + _mySettings.b_TextBox_LightCursor_NightOnly		+ @"""").
						Replace("{TextBox_Align}",						@"""" + _mySettings.TextBox_Align						+ @"""").
						Replace("{TextBox_Language}",					@"""" + _mySettings.TextBox_Language					+ @"""").
						Replace("{SingleUser_UserName}",				@"""" + _mySettings.SingleUser_UserName					+ @"""");

			return json;
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

