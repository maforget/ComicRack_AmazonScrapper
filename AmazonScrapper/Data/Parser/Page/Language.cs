using AmazonScrapper.Web;
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
            var text = Node?.SelectSingleNode($".//div[@id='detailBullets_feature_div']//span[contains(text(), '{GetLanguageText()}')]/following::span")?.InnerText?.Trim();

            if (string.IsNullOrEmpty(text))
                return string.Empty;

			var culture = GetCulture(text);
			if (culture != null)
                return culture.TwoLetterISOLanguageName;

            return string.Empty;
        }

		protected virtual string GetLanguageText() => "Language";

        protected virtual CultureInfo GetCulture(string text)
		{
			return CultureInfo.GetCultures(CultureTypes.AllCultures).FirstOrDefault(r => string.Equals(r.EnglishName, text, StringComparison.OrdinalIgnoreCase));
		}
	}

	public class Language_Fr : Language
	{
		public Language_Fr(HtmlNode node) : base(node)
		{
		}

		public override TLDs TLD => TLDs.fr;
		protected override string GetLanguageText() => "Langue";

		override protected CultureInfo GetCulture(string text)
		{
			CultureInfo cultureInfo = base.GetCulture(text);
			return cultureInfo ?? CultureInfo.GetCultures(CultureTypes.AllCultures).FirstOrDefault(r => string.Equals(r.NativeName, text, StringComparison.OrdinalIgnoreCase));
		}
	}
}
