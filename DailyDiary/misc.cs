using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Controls;

namespace DailyDiary
{
	public class DisplayHandler
	{
		private Timer t = new Timer();
		private Timer t2 = new Timer();
		private Label _lbl = null;
		private Control _ctrl = null;
		private decimal _opacity = 0;
		private int direction = 1;
		private int _ctrlSize = 0;
		private int _increment = 0;
		private int _currentSize = 0;

		public DisplayHandler()
		{
			t.Elapsed += T_Elapsed;
			t2.Elapsed += T2_Elapsed;
			t2.Interval = 0.5;
		}

		private void T2_Elapsed(object sender, ElapsedEventArgs e)
		{
			if (direction == 1 && _currentSize < _ctrlSize) { _currentSize += _increment; }
			else if (direction == -1 && _currentSize > 0) { _currentSize -= _increment; }
			else if( (direction == 1 && _currentSize >= _ctrlSize) || (direction == -1 && _currentSize <= 0)) { t2.Stop(); }
			_ctrl.Dispatcher.Invoke(new flasher(resizeControl));
		}

		private void T_Elapsed(object sender, ElapsedEventArgs e)
		{
			if(_opacity >= 1) { direction = -1; }
			if(_opacity <= 0 && direction == -1) { t.Stop(); }
			if(direction == 1)
			{
				_opacity += (decimal)0.02;
			}
			else
			{
				_opacity -= (decimal)0.01;
			}
			_lbl.Dispatcher.Invoke(new flasher(flash));
		}

		public void flashText(Label lbl)
		{
			_lbl = lbl;
			t.Interval = 0.1;
			t.Start();
		}

		public void showControl(Control ctrl, int width, int increment)
		{
			_ctrl = ctrl;
			direction = 1;
			_increment = increment;
			_ctrlSize = width;
			t2.Start();
		}
		public void hideControl(Control ctrl, int increment)
		{
			_ctrl = ctrl;
			direction = -1;
			_increment = increment;
			_ctrlSize = (int)ctrl.Width;
			_currentSize = _ctrlSize;
			t2.Start();
		}

		public delegate void flasher();
		public void flash()
		{
			_lbl.Opacity = (double)_opacity;
		}
		public void resizeControl()
		{
			_ctrl.Width = _currentSize;
		}
		
	}

	public static class misc
	{
		public static Dictionary<string, string> fillLangs()
		{
			Dictionary<string, string> langs = new Dictionary<string, string>();

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

			return langs;
		}
	}
}
