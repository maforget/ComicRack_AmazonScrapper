using AmazonScrapper.Web;
using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace AmazonScrapper.Data.Parser.Search
{
    public class Title : ParserSearch, IResult<string>
    {
        public Title(HtmlNode node) : base(node)
        {
        }

        public string Result => this.ToString();

        /// <summary>
        /// ///Parses the Title from the Search Page
        /// </summary>
        /// <param name="node">The HtmlNode for the search item</param>
        /// <returns>the title</returns>
        public override object Parse()
        {
            return Node.SelectSingleNode(@".//h2/a/span")?.InnerText?.Trim().DecodeHTML();
        }
    }
}
