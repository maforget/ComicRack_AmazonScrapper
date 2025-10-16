using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AmazonScrapper.ComicRack
{
	public class Theme : ObjectBase
	{
		private readonly Version minSupportedVersion = new Version(0, 9, 182);
		public Theme(object Object) : base(Object)
		{
		}

		public void ApplyTheme(Control control)
		{
			if (base.Object != null && Plugin.App.ProductVersion.CompareTo(minSupportedVersion) >= 0)
				InvokeMethod("ApplyTheme", new object[] { control });
		}
	}
}
