using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmazonScrapper.Data.Parser.Search
{
    public class Link : ParserSearch, IResult<string>
    {
        public Link(HtmlNode node) : base(node)
        {
        }
        public string Result => this.ToString();

        /// <summary>
        /// ///Parses the Title from the Search Page
        /// </summary>
        /// <returns></returns>
        public override object Parse()
        {
            return Node.SelectSingleNode(@".//div[@class='aok-relative']/span/a")?.Attributes["href"]?.Value;
        }
    }
}
