using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AmazonScrapper.Web;
using HtmlAgilityPack;

namespace AmazonScrapper.Tools
{
    public static class Extensions
    {
        public static string GetDescription(this TLDs tld) => Reflections.GetEnumDescription(typeof(TLDs), tld.ToString());

        public static string GetTextWithLineBreaks(this HtmlNode node)
        {
            if (node is null)
                return string.Empty;

            var clone = node.CloneNode(true);
            var brNodes = clone.DescendantsAndSelf()
                .Where(n => n.Name.Equals("br", StringComparison.OrdinalIgnoreCase))
                .ToList();

            foreach (var br in brNodes)
                br.ParentNode.ReplaceChild(HtmlNode.CreateNode("\n"), br);

            return clone.InnerText.DecodeHTML();
        }

    }
}
