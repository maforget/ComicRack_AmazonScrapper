using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmazonScrapper.Data.Parser.Collection
{
    public class Link : ParserCollection, IResult<string>
    {
        public Link(HtmlNode node) : base(node)
        {
        }
        public string Result => this.ToString();

        /// <summary>
        /// ///Parses the Title from the Collection Page
        /// </summary>
        /// <param name="node">The HtmlNode for the collection item</param>
        /// <returns>the link</returns>
        public override object Parse()
        {
            return Node.SelectSingleNode(@".//a[contains(@class, 'itemBookTitle')]")?.Attributes["href"]?.Value?.Trim();
        }
    }
}
