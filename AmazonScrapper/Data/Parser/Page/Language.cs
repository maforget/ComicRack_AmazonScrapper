using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmazonScrapper.Data.Parser.Page
{
    public class Language: ParserPage, IResult<string>
    {
        public Language(HtmlNode node) : base(node)
        {
        }

        public string Result => this.ToString();

        public override object Parse()
        {
            var text = Node?.SelectSingleNode(".//div[@id='detailBullets_feature_div']//span[contains(text(), 'Language')]/following::span")?.InnerText?.Trim();

            if (string.IsNullOrEmpty(text))
                return string.Empty;

            var culture = CultureInfo.GetCultures(CultureTypes.AllCultures).FirstOrDefault(r => r.EnglishName == text);
            if (culture != null)
                return culture.TwoLetterISOLanguageName;

            return string.Empty;
        }
    }
}
