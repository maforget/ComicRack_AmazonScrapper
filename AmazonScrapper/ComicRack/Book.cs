using AmazonScrapper.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static AmazonScrapper.Plugin;

namespace AmazonScrapper.ComicRack
{
    public class Book : ObjectBase
    {
        public Book(object book)
            :base(book)
        {
        }

        public string Series => GetValue<string>();
        public string ShadowSeries => GetValue<string>();

        public string Number => GetValue<string>();
        public string ShadowNumber => GetValue<string>();

        public int Volume => GetValue<int>();
        public int ShadowVolume => GetValue<int>();

        public string FilePath => GetValue<string>();
    }
}
