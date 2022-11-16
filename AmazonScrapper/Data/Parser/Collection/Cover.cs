using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using HtmlAgilityPack;

namespace AmazonScrapper.Data.Parser.Collection
{
    public class Cover: ParserCollection, IResult<string>
    {
        public Cover(HtmlNode node) : base(node)
        {
        }

        public string Result => this.ToString();

        /// <summary>
        /// ///Parses the Title from the Collection Page
        /// </summary>
        /// <param name="node">The HtmlNode for the collection item</param>
        /// <returns>the cover link</returns>
        public override object Parse()
        {
            return Node.SelectSingleNode(@".//img[contains(@class, 'asinImage')]")?.Attributes["src"]?.Value?.Trim();
        }
    }
}
