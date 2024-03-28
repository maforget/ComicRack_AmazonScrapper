using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using HtmlAgilityPack;

namespace AmazonScrapper.Data.Parser.Page
{
    public class Cover : ParserPage, IResult<string>
    {
        public Cover(HtmlNode node) : base(node)
        {
        }

        public string Result => this.ToString();

        /// <summary>
        /// ///Parses the Title from the book Page
        /// </summary>
        /// <param name="node">The HtmlNode for the body</param>
        /// <returns>the cover link</returns>
        public override object Parse()
        {
            var text = Node.SelectSingleNode(".//img[@id='landingImage']")?.Attributes["src"]?.Value?.Trim();

            if (string.IsNullOrEmpty(text))
                return string.Empty;

            //Strip unwanted strings from the url, to get the high-res iamge
            string output = Regex.Replace(text, @"(https[^_]+)_[^\.]+\.(.+)", "$1$2", RegexOptions.IgnoreCase);

            return string.IsNullOrEmpty(output) ? text : output;
        }
    }
}
