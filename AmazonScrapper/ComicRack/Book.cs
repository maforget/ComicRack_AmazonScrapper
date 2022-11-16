using AmazonScrapper.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static AmazonScrapper.Plugin;

namespace AmazonScrapper.ComicRack
{
    public class Book
    {
        private Type bookType => book.GetType();
        public readonly object book;

        public Book(object book)
        {
            this.book = book;
        }

        public T Get<T>(string property)
        {
            var prop = bookType.GetProperty(property);
            return (T)bookType.GetPropertyValue(this.book, property);
        }

        public void SetValue(string property, object value)
        {
            bookType.SetPropertyValue(this.book, property, value);
        }

        public string Series => Get<string>("Series");
        public string ShadowSeries => Get<string>("ShadowSeries");

        public string Number => Get<string>("Number");
        public string ShadowNumber => Get<string>("ShadowNumber");

        public int Volume => Get<int>("Volume");
        public int ShadowVolume => Get<int>("ShadowVolume");

        public string FilePath => Get<string>("FilePath");
    }
}
