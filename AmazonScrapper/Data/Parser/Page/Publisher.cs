using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using AmazonScrapper.Web;
using HtmlAgilityPack;

namespace AmazonScrapper.Data.Parser.Page
{
    public class Publisher: ParserPage, IResult<string>
    {
        public Publisher(HtmlNode node) : base(node)
        {
        }

        public string Result => this.ToString();

        public override object Parse()
        {
            var input = Node.SelectSingleNode($"//*[@id=\"rpi-attribute-book_details-publisher\"]//*[contains(@class, \"rpi-attribute-value\")]/span")?.InnerText?.Trim().DecodeHTML();

            if (string.IsNullOrEmpty(input))
                return string.Empty;

            string publisher = Regex.Match(input, "[^(]+", RegexOptions.IgnoreCase).Value?.Trim();
            return publisher;
        }

        protected virtual string GetPublisherText() => "Publisher";
	}

    public class Publisher_Fr : Publisher
    {
        public Publisher_Fr(HtmlNode node) : base(node)
        {
        }

        public override TLDs TLD => TLDs.fr;

        protected override string GetPublisherText() => "Éditeur";
    }
}
