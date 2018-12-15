using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Controls;

namespace DailyDiary
{
	public class misc
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

		public misc()
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
}
