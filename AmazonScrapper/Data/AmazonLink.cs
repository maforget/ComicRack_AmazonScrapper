using System.Drawing;
using System.Text.RegularExpressions;

namespace AmazonScrapper.Data
{
    public abstract class AmazonLink
    {
        protected string link;

        public AmazonLink(string asin, string title, string link, string imageLink)
        {
            Title = title;
            ASIN = asin;
            //this.link = Web.Utility.FixLink(link);
            ImageLink = imageLink;
        }

        public AmazonLink(string title, string link, string imageLink)
            : this(GetASINfromLink(link), title, link, imageLink)
        {

        }

        public override bool Equals(object obj)
        {
            var other = obj as AmazonLink;
            return this.ASIN.Equals(other.ASIN);
        }

        public virtual string Title { get; }
        public string Link => $@"https://www.amazon.com/dp/{ASIN}";
        public string ASIN { get; set; }
        public string ImageLink { get; }
        public virtual string SerieDisplayText { get; }
        //public Image Image => Web.Fetcher.GetImage(ImageLink);

        private static string GetASINfromLink(string link)
        {
            if (!string.IsNullOrEmpty(link))
                return Regex.Match(link, "dp/(?<asin>[^/?&]+)|gp/product/(?<asin>[^/?&]+)", RegexOptions.IgnoreCase)?.Groups["asin"].Value;

            return string.Empty;
        }
    }
}