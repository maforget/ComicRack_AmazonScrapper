using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;
using AmazonScrapper.Tools;
using AmazonScrapper.Web;

namespace AmazonScrapper.Data.Parser
{
    public class ParserManager<T>: BaseManager<T> where T: IParser
    {
		public ParserManager(HtmlNode node, TLDs tld = TLDs.com)
            : base(tld, new object[] { node })
        {
		}

		protected override bool HasSubclass(T x) => x.GetType().BaseType?.BaseType != typeof(ParserBase);

	}
}
