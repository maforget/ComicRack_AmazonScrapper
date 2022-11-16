using AmazonScrapper.Dialog;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using AmazonScrapper.Tools;
using System.Security.AccessControl;
using AmazonScrapper.ComicRack;
using AmazonScrapper.Settings;

namespace AmazonScrapper
{
    public class Plugin
    {
        private static App _ComicRackApp;
        private static BookCollection _Books;
        private static frmProgress _frmProgress;
        private static CurrentBook _CurrentBook;

        public static void Run(object ComicRackApp, object[] books)
        {
            if (ComicRackApp == null || books?.Length <= 0)
                //return;
                throw new Exception();

            _ComicRackApp = new App(ComicRackApp);
            _Books = new BookCollection(books);
            _CurrentBook = new CurrentBook();
            _frmProgress = new frmProgress(_Books.Length);
            try
            {
                //frm.ShowDialogOnNewThread();
                _frmProgress.ShowDialog();
            }
            catch (OperationCanceledException)
            {
                throw;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static void ProcessBooks()
        {
            if (_ComicRackApp == null && _Books?.Length <= 0)
                return;

            _CurrentBook.ProcessBook();
        }

        public class CurrentBook
        {
            private Book _CurrentBook;
            private int _CurrentBookIndex = 0;

            public void ProcessBook()
            {
                try
                {
                    if (_ComicRackApp == null || _Books?.Length <= 0 || _CurrentBookIndex >= _Books?.Length)
                    {
                        _frmProgress?.Close();
                        return;
                    }

                    var token = _frmProgress.Token;

                    _CurrentBook = _Books[_CurrentBookIndex];

                    var bookSeries = _CurrentBook.Series;
                    var bookShadowSeries = _CurrentBook.ShadowSeries;
                    var serie = string.IsNullOrEmpty(bookSeries) ? bookShadowSeries : bookSeries;

                    var bookVolume = _CurrentBook.Volume;
                    var bookShadowVolume = _CurrentBook.ShadowVolume;
                    var volume = bookVolume <= -1 ? bookShadowVolume : bookVolume;

                    var bookNumber = _CurrentBook.Number;
                    var bookShadowNumber = _CurrentBook.ShadowNumber;
                    var number = string.IsNullOrEmpty(bookNumber) ? bookShadowNumber : bookNumber;
                    number = string.IsNullOrEmpty(number) ? volume.ToString() : number;

                    //Get the current book thumbnail
                    var currentImage = _ComicRackApp.GetComicThumbnail(_CurrentBook, 0);
                    _frmProgress.UpdateBook(currentImage, serie, number);

                    var frmM = new frmMain(serie, number, token);
                    frmM.BookChosen += BookChosen;
                    frmM.BookSkipped += BookSkipped;
                    frmM.Show(_frmProgress);
                }
                catch (OperationCanceledException)
                {
                    
                }
                catch (Exception)
                {
                    throw;
                }
            }

            private void BookSkipped(object sender, EventArgs e)
            {
                var frmM = (frmMain)sender;
                _frmProgress.IncreaseProgressBarByOne();

                //Unsubscribe from the events so it doesn't double fire on next run
                frmM.BookChosen -= BookChosen;
                frmM.BookSkipped -= BookSkipped;

                //Increase Current Book Index & Process Next Book
                _CurrentBookIndex++;
                ProcessBook();
            }

            private void BookChosen(object sender, EventArgs e)
            {
                var frmM = (frmMain)sender;
                _frmProgress.IncreaseProgressBarByOne();
                //var bookType = _CurrentBook.GetType();
                //var ComicRackType = _ComicRackApp.GetType();

                var token = frmM.Token;
                var result = frmM.DialogResult;
                var bookInfo = frmM.BookInfo;

                if (!token.IsCancellationRequested && result == DialogResult.OK && bookInfo != null)
                {
                    var userConfig = Config.GetUserDictionary();
                    foreach (var c in userConfig)
                    {
                        object def = bookInfo.GetDefault(c.Key);
                        object value = def;
                        if (c.Value.Enabled)
                            value = bookInfo.GetValue(c.Key);

                        //Set the Image for fileless
                        if (c.Key == "Cover" && string.IsNullOrEmpty(_CurrentBook.FilePath))
                            _ComicRackApp.SetCustomBookThumbnail(_CurrentBook, bookInfo.Cover);
                        else if (c.Value.Enabled && value != def && c.Key != "Cover")
                            _CurrentBook.SetValue(c.Key, value);
                    }
                }

                //Unsubscribe from the events so it doesn't double fire on next run
                frmM.BookChosen -= BookChosen;
                frmM.BookSkipped -= BookSkipped;

                //Increase Current Book Index & Process Next Book
                _CurrentBookIndex++;
                ProcessBook();
            }
        }
    }
}
