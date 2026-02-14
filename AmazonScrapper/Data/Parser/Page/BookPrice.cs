using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using HtmlAgilityPack;
using System.Windows.Forms;
using AmazonScrapper.Web;

namespace AmazonScrapper.Data.Parser.Page
{
    public class BookPrice : ParserPage, IResult<float>
    {
        public BookPrice(HtmlNode node) : base(node)
        {
        }

        public float Result => this.ToFloat();

        public override object Parse()
        {
            var buyOneClick = Node.SelectNodes(".//form[@id='buyOneClick']//input[@name='displayedPrice']/@value")?.FirstOrDefault()?.Attributes["value"];
            var digital_bulk_form = Node.SelectNodes(".//form[@class='digital-bulk-form']//input[@name='displayedPrice']/@value")?.FirstOrDefault()?.Attributes["value"];
            var slot_price = Node.SelectNodes("//span[@class='slot-price']//text()").Where(x => !string.IsNullOrEmpty(x.InnerText.Trim())).FirstOrDefault().InnerText.Trim();
            var displayedPrice = digital_bulk_form ?? buyOneClick;

            if (displayedPrice == null && string.IsNullOrEmpty(slot_price))
                return -1f;

            string price = displayedPrice?.Value.Trim();
            if (float.TryParse(price, out float value))
                return value;

            Regex priceRegex = new Regex(@"[0-9]+[\.,][0-9]{2}", RegexOptions.IgnoreCase);
            if (priceRegex.Match(slot_price).Success && float.TryParse(priceRegex.Match(slot_price)?.Value, out value))
                return value;

            return -1f;
        }
    }
}