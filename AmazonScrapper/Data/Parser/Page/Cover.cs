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
            var text = Node.SelectSingleNode(".//img[@id='ebooksImgBlkFront']")?.Attributes["data-a-dynamic-image"]?.Value?.Trim();

            if (string.IsNullOrEmpty(text))
                return string.Empty;

            return Regex.Match(text, @"(https[^]_""[}{]+\.jpg)", RegexOptions.IgnoreCase)?.Groups[1]?.Value;
        }
    }
}
