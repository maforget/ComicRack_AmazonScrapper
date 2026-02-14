using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using HtmlAgilityPack;
using System.Globalization;

namespace AmazonScrapper.Data.Parser.Page
{
    public class Rating : ParserPage, IResult<float>
    {
        public Rating(HtmlNode node) : base(node)
        {
        }

        public float Result => this.ToFloat();

        public override object Parse()
        {
            var input = Node.SelectSingleNode("//*[@id=\"detailBullets_averageCustomerReviews\"]//span[contains(@class, 'a-size-small')]")?.InnerText?.Trim();

            if (string.IsNullOrEmpty(input))
                return 0f;

            string rating = Regex.Match(input, @"[.\d]+", RegexOptions.IgnoreCase)?.Value?.Trim();
            if (float.TryParse(rating, NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture, out float value))
                return value;

            return 0f;
        }
    }
}
