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
		protected override void OnHandleCreated(EventArgs e)
		{
			base.OnHandleCreated(e);
			Plugin.Theme.ApplyTheme(this);
		}
	}
}
