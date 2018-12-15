using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace DailyDiary.Properties
{
	class settings
	{
		//Main
		public string					username							{ get; set; }
		public int						userid								{ get; set; }

		//Colors
		public System.Drawing.Color		c_day_Day							{ get; set; }
		public System.Drawing.Color		c_night_Day							{ get; set; }
		public System.Drawing.Color		c_day_MainBackground				{ get; set; }
		public System.Drawing.Color		c_night_MainBackground				{ get; set; }
		public System.Drawing.Color		c_day_SelectedDay					{ get; set; }
		public System.Drawing.Color		c_night_SelectedDay					{ get; set; }
		public System.Drawing.Color		c_day_Sunday						{ get; set; }
		public System.Drawing.Color		c_night_Sunday						{ get; set; }
		public System.Drawing.Color		c_day_TextboxBackground				{ get; set; }
		public System.Drawing.Color		c_night_TextboxBackground			{ get; set; }
		public System.Drawing.Color		c_day_Text							{ get; set; }
		public System.Drawing.Color		c_night_Text						{ get; set; }
		public System.Drawing.Color		c_day_TextSelection					{ get; set; }
		public System.Drawing.Color		c_night_TextSelection				{ get; set; }
		public System.Drawing.Color		c_day_DayText						{ get; set; }
		public System.Drawing.Color		c_night_DayText						{ get; set; }

		//Booleans
		public bool						b_AskBeforeChange					{ get; set; }
		public bool						b_NightMode							{ get; set; }
		public bool						b_PanelClosed						{ get; set; }
		public bool						b_SingleUserMode					{ get; set; }
		public bool						b_SpellCheck						{ get; set; }
		public bool						b_TextBox_WordWrap					{ get; set; }
		public bool						b_TextBox_LightCursor				{ get; set; }
		public bool						b_TextBox_LightCursor_NightOnly		{ get; set; }

		//Misc
		public int						TextBox_Align						{ get; set; } = 1;
		public string					TextBox_Language					{ get; set; }
		public string					SingleUser_UserName					{ get; set; }

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
				case colors.Day:				bru = colorToBrush(variant == "day" ? c_day_Day					: c_night_Day);
					break;
				case colors.DayText:			bru = colorToBrush(variant == "day" ? c_day_DayText				: c_night_DayText);
					break;
				case colors.MainBackground:		bru = colorToBrush(variant == "day" ? c_day_MainBackground		: c_night_MainBackground);
					break;
				case colors.SelectedDay:		bru = colorToBrush(variant == "day" ? c_day_SelectedDay			: c_night_SelectedDay);
					break;
				case colors.Sunday:				bru = colorToBrush(variant == "day" ? c_day_Sunday				: c_night_Sunday);
					break;
				case colors.Text:				bru = colorToBrush(variant == "day" ? c_day_Text				: c_night_Text);
					break;
				case colors.TextboxBackground:	bru = colorToBrush(variant == "day" ? c_day_TextboxBackground	: c_night_TextboxBackground);
					break;
				case colors.TextSelection:		bru = colorToBrush(variant == "day" ? c_day_TextSelection		: c_night_TextSelection);
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
					case colors.Day:				c_day_Day					= c;	break;
					case colors.DayText:			c_day_DayText				= c;	break;
					case colors.MainBackground:		c_day_MainBackground		= c;	break;
					case colors.SelectedDay:		c_day_SelectedDay			= c;	break;
					case colors.Sunday:				c_day_Sunday				= c;	break;
					case colors.Text:				c_day_Text					= c;	break;
					case colors.TextboxBackground:	c_day_TextboxBackground		= c;	break;
					case colors.TextSelection:		c_day_TextSelection			= c;	break;
				}
			}
			else
			{
				switch (type)
				{
					case colors.Day:				c_night_Day					= c;	break;
					case colors.DayText:			c_night_DayText				= c;	break;
					case colors.MainBackground:		c_night_MainBackground		= c;	break;
					case colors.SelectedDay:		c_night_SelectedDay			= c;	break;
					case colors.Sunday:				c_night_Sunday				= c;	break;
					case colors.Text:				c_night_Text				= c;	break;
					case colors.TextboxBackground:	c_night_TextboxBackground	= c;	break;
					case colors.TextSelection:		c_night_TextSelection		= c;	break;
				}
			}
		}

		/// <summary>
		/// Converts Color to SolidColorBrush
		/// </summary>
		/// <param name="c">Color to convert</param>
		/// <returns>SolidColorBrush, from input colors ARGB values</returns>
		public SolidColorBrush colorToBrush(System.Drawing.Color c)
		{
			return new SolidColorBrush(new Color() { A = c.A, R = c.R, G = c.G, B = c.B });
		}
	}

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
