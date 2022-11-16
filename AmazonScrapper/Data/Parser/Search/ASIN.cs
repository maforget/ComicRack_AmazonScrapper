using HtmlAgilityPack;
using System;
using System.Xml.Linq;

namespace AmazonScrapper.Data.Parser.Search
{
    public class ASIN : ParserSearch, IResult<string>
    {
        public ASIN(HtmlNode node) : base(node)
        {
        }

        public string Result => this.ToString();

        public override object Parse()
        {
            return Node.GetAttributeValue("data-asin", string.Empty)?.Trim();
        }
    }
}