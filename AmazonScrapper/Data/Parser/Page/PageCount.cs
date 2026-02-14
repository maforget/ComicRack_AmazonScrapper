using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using HtmlAgilityPack;

namespace AmazonScrapper.Data.Parser.Page
{
    public class PageCount : ParserPage, IResult<int>
    {
        public PageCount(HtmlNode node) : base(node)
        {
        }

        public int Result => this.ToInt();

        public override object Parse()
        {
            var count1 = Node.SelectSingleNode("//*[@id=\"rpi-attribute-book_details-ebook_pages\"]//*[contains(@class, \"rpi-attribute-value\")]/span")?.InnerText?.Trim();
            var count2 = Node.SelectSingleNode("//*[@id=\"rpi-attribute-book_details-fiona_pages\"]//*[contains(@class, \"rpi-attribute-value\")]/span")?.InnerText?.Trim();
            var text = string.IsNullOrEmpty(count1) ? count2 : count1;

            if (string.IsNullOrEmpty(text))
                return 0;

            string pageCount = Regex.Match(text, @"\d+", RegexOptions.IgnoreCase)?.Value;
            if (int.TryParse(pageCount, out int value))
                return value;

            return 0;
        }
    }
}
