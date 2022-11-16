using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;
using AmazonScrapper.Tools;

namespace AmazonScrapper.Data.Parser
{
    public class ParserManager<T>: Collection<T> where T: IParser
    {
        public ParserManager(HtmlNode node)
            : base(new object[] { node })
        {
        }
    }
}
