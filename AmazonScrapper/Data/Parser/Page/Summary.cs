using AmazonScrapper.Web;
using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AmazonScrapper.Data.Parser.Page
{
    public class Summary : ParserPage, IResult<string>
    {
        public Summary(HtmlNode node) : base(node)
        {
        }

        public string Result => this.ToString();

        public override object Parse()
        {
            var text = Node.SelectSingleNode(".//div[@data-feature-name='bookDescription']")?.InnerText.DecodeHTML();

            if (string.IsNullOrEmpty(text))
                return string.Empty;

            return Regex.Replace(text, @"\s+Read more|\s+Read less", "", RegexOptions.IgnoreCase);//Remove Read more when it exists
        }
    }

	public class Summary_Fr : Summary
	{
		public Summary_Fr(HtmlNode node) : base(node)
		{
		}

		public override TLDs TLD => TLDs.fr;

		public override object Parse()
		{
            string text = base.Parse() as string;

            if (string.IsNullOrEmpty(text))
                return string.Empty;
			    
            return Regex.Replace(text, @"\s+En lire plus", "", RegexOptions.IgnoreCase);//Remove Read more when it exists

		}
	}
}
