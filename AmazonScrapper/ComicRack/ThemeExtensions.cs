using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AmazonScrapper.Tools;

namespace AmazonScrapper.ComicRack
{
	public static class ThemeExtensions
	{
		private static readonly Type _type = Initialize();
		public static Type Initialize() => Type.GetType("cYo.Common.Windows.Forms.ThemeExtensions, cYo.Common.Windows");

		public static bool IsDarkModeEnabled => _type?.GetField<bool>() ?? false;

		public static void Theme(this Control control) => _type.InvokeStaticMethod( new object[] { control });
	}
}
