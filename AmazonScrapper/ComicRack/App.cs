using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static AmazonScrapper.Plugin;

namespace AmazonScrapper.ComicRack
{
    public class App
    {
        private object comicRackApp;
        private Type comicRackType => comicRackApp.GetType();

        public App(object comicRackApp)
        {
            this.comicRackApp = comicRackApp;
        }

        private object InvokeMethod(string Method, object[] param)
        {
            return comicRackType.GetMethod(Method).Invoke(comicRackApp, param);
        }

        public Image GetComicThumbnail(Book currentBook, int page)
        {
            //Get the current book thumbnail
            return InvokeMethod("GetComicThumbnail", new object[] { currentBook.book, page }) as Image;
        }

        public void SetCustomBookThumbnail(Book currentBook, Image page)
        {
            //Set the book cover thumbnail
            InvokeMethod("SetCustomBookThumbnail", new object[] { currentBook.book, page });
        }
    }
}
