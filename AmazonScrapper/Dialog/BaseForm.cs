using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AmazonScrapper.ComicRack;

namespace AmazonScrapper.Dialog
{
	public class BaseForm : Form
	{
		public void ApplyTheme(Control control = null)
		{
			if (!ThemeExtensions.IsDarkModeEnabled) return;
			if (control == null)
			{
				ThemeExtensions.Theme(this);
			}
			else
			{
				ThemeExtensions.Theme(control);
			}
		}

		protected override void OnHandleCreated(EventArgs e)
		{
			base.OnHandleCreated(e);
			ApplyTheme();
		}
	}
}
