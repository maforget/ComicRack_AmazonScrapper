using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using HtmlAgilityPack;
using System.Windows.Forms;

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
            var displayedPrice = digital_bulk_form ?? buyOneClick;

            if (displayedPrice == null)
                return -1f;

            string price = displayedPrice.Value.Trim();
            if (float.TryParse(price, out float value))
                return value;

            return -1f;
        }
    }
}