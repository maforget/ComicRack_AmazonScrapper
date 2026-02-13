using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AmazonScrapper.Web;
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
			var text2 = Node.SelectSingleNode(".//div[@id='rpi-attribute-book_details-publication_date']//div[contains(@class, 'rpi-attribute-value')]/span")?.InnerText?.Trim();
			var dateString = text ?? text2;

			if (string.IsNullOrEmpty(dateString))
				return DateTime.MinValue;

			if (TryParseDate(dateString, out DateTime date))
				return date;

			return DateTime.MinValue;
		}

		protected virtual bool TryParseDate(string dateString, out DateTime date)
		{
			return DateTime.TryParse(dateString, out date);
		}
	}

	public class Date_Br : Date
	{
		public Date_Br(HtmlNode node) : base(node)
		{
		}

		public override TLDs TLD => TLDs.br;

		protected override bool TryParseDate(string dateString, out DateTime date)
		{
			CultureInfo culture = new CultureInfo("pt-BR");
			return DateTime.TryParse(dateString, culture, DateTimeStyles.None, out date);
		}
	}
}
