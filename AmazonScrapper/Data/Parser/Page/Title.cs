using AmazonScrapper.Web;
using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace AmazonScrapper.Data.Parser.Page
{
    public class Title : ParserPage, IResult<string>
    {
        public Title(HtmlNode node) : base(node)
        {
        }

        public string Result => this.ToString();

        /// <summary>
        /// ///Parses the Title from the book Page
        /// </summary>
        /// <param name="node">The HtmlNode for the body</param>
        /// <returns>the title</returns>
        public override object Parse()
        {
            var text = Node.SelectSingleNode(".//span[@id='productTitle']")?.InnerText?.Trim().DecodeHTML();

            if (string.IsNullOrEmpty(text))
                return string.Empty;

            return text;
        }
    }
}
