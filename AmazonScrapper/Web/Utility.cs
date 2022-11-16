using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;

namespace AmazonScrapper.Web
{
    public static class Utility
    {
        public static string DecodeHTML(this string text)
        {
            return HttpUtility.HtmlDecode(text).Trim();
        }

        public static string FixLink(string link)
        {
            string url = AddBaseURL(link);
            string fix = RemoveRef(url);
            return fix;
        }

        private static string RemoveRef(string url)
        {
            if (!string.IsNullOrEmpty(url))
                return Regex.Match(url, ".+dp/[^/?&]+", RegexOptions.IgnoreCase).Value;

            return string.Empty;
        }

        private static string AddBaseURL(string link)
        {
            string baseURL = @"https://www.amazon.com/";
            if (!string.IsNullOrEmpty(link))
                return $"{baseURL}{link}";

            return string.Empty;
        }

        public static Image byteArrayToImage(this byte[] bytesArr)
        {
            MemoryStream memstr = new MemoryStream(bytesArr);
            return Image.FromStream(memstr);
        }

        public static string ConvertToBase64String(this Image image)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                image.Save(ms, image.RawFormat);
                return Convert.ToBase64String(ms.ToArray());
            }
        }

        public static Image ConvertBase64ToImage(this string image64Bit)
        {
            byte[] imageBytes = Convert.FromBase64String(image64Bit);
            return imageBytes.byteArrayToImage();
        }

        public static HtmlNode GetParentBody(this HtmlNode node)
        {
            return node?.OwnerDocument?.DocumentNode?.SelectSingleNode("//body");
        }
    }
}
