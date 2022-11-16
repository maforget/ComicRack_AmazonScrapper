using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;

namespace AmazonScrapper.Data.Parser.Page
{
    public class Date : ParserPage, IResult<DateTime>
    {
        public Date(HtmlNode node) : base(node)
        {
        }

        public DateTime Result => this.ToDateTime();

        public override object Parse()
        {
            var text = Node.SelectSingleNode(".//div[@id='detailBullets_feature_div']//span[contains(text(), 'Publication')]/following::span")?.InnerText?.Trim();

            if (string.IsNullOrEmpty(text))
                return DateTime.MinValue;

            if (DateTime.TryParse(text, out DateTime date))
                return date;

            return DateTime.MinValue;
        }
    }
}
