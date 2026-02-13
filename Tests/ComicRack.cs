using Microsoft.VisualStudio.TestTools.UnitTesting;
using cYo.Projects.ComicRack.Engine;
using System;
using System.IO;
using AmazonScrapper.ComicRack;
namespace Tests
{
    [TestClass]
    public class ComicRack
    {
        [TestMethod]
        public void TestComicBook()
        {
            string sFilePath = GetComicBook("ComicBook1.cbz");
            var cb = ComicBook.Create(sFilePath, RefreshInfoOptions.ForceRefresh);

            var book = new Book(cb);

            Assert.AreEqual(cb.Series, book.Series);
            Assert.AreEqual(cb.Number, book.Number);
            Assert.AreEqual(cb.Volume, book.Volume);
            Assert.AreEqual(cb.Title, book.GetValue<string>("Title"));
        }

        private static string GetComicBook(string file)
        {
            string sCurrentDirectory = AppDomain.CurrentDomain.BaseDirectory;
            string sFile = Path.Combine(sCurrentDirectory, $@"..\..\Resources\\{file}");
            return Path.GetFullPath(sFile);
        }
    }
}
