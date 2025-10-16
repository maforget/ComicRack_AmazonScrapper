using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static AmazonScrapper.Plugin;

namespace AmazonScrapper.ComicRack
{
    public class App : ObjectBase
    {
        public App(object comicRackApp)
            : base(comicRackApp)
        {
        }

		public Version ProductVersion
		{
			get
			{
				if (Version.TryParse(GetValue<string>(), out Version version))
					return version;

				return new Version(0, 9, 0); // super old version for the compare
			}
		}

		public Bitmap GetComicThumbnail(Book currentBook, int page) => InvokeMethod("GetComicThumbnail", currentBook.Object, page) as Bitmap;

		public void SetCustomBookThumbnail(Book currentBook, Image page) => InvokeMethod("SetCustomBookThumbnail", currentBook.Object, page);
	}
}
