using System.Drawing;
using System.Text.RegularExpressions;
using AmazonScrapper.Tools;
using AmazonScrapper.Web;

namespace AmazonScrapper.Data
{
	public abstract class AmazonLink: IDomain
    {
        protected string link;

        public AmazonLink(string asin, string title, string link, string imageLink, TLDs tld = TLDs.com)
        {
            Title = title;
            ASIN = asin;
            //this.link = Web.Utility.FixLink(link);
            ImageLink = imageLink;
            TLD = tld;
            Fetcher.Instance.RegisterTLDs(tld);
        }

        public AmazonLink(string title, string link, string imageLink, TLDs tld = TLDs.com)
            : this(GetASINfromLink(link), title, link, imageLink, tld)
        {

        }

        public override bool Equals(object obj)
        {
            var other = obj as AmazonLink;
            return this.ASIN.Equals(other.ASIN);
        }

        public virtual string Title { get; }
        public string Link => $@"https://www.amazon.{TLD.GetDescription()}/dp/{ASIN}";
        public string ASIN { get; set; }
        public string ImageLink { get; }
        public TLDs TLD { get; }
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