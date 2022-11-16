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
            var text = Node.SelectSingleNode(".//div[@id='detailBullets_feature_div']//span[contains(text(), 'Print length')]/following::span")?.InnerText?.Trim();

            if (string.IsNullOrEmpty(text))
                return 0;

            string pageCount = Regex.Match(text, @"\d+", RegexOptions.IgnoreCase)?.Value;
            if (int.TryParse(pageCount, out int value))
                return value;

            return 0;
        }
    }
}
