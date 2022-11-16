using AmazonScrapper.Web;
using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace AmazonScrapper.Data.Parser.Collection
{
    public class Title : ParserCollection, IResult<string>
    {
        public Title(HtmlNode node) : base(node)
        {
        }

        public string Result => this.ToString();

        /// <summary>
        /// Parses the Title from the Collection Page
        /// </summary>
        /// <param name="node">The HtmlNode for the collection item</param>
        /// <returns>the title</returns>
        public override object Parse()
        {
            return Node.SelectSingleNode(@".//a[contains(@class, 'itemBookTitle')]")?.InnerText?.Trim().DecodeHTML();
        }
    }
}
