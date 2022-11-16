using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.Drawing;
using System.Threading;
using AmazonScrapper.Data.Parser;

namespace AmazonScrapper.Data
{
    public class AmazonLinkIssues : AmazonLink
    {
        public AmazonLinkIssues(string asin, string title = "")
            : base(asin, title, string.Empty, string.Empty)
        {

        }

        public AmazonLinkIssues(string title, string link, string imageLink)
           : base(title, link, imageLink)
        {

        }

        public SerieInfo SerieInfo { get; set; }

        public override string SerieDisplayText => SerieInfo == null ? string.Empty : SerieInfo.DisplayText;

        public AmazonBookInfo ScrapeData(CancellationToken ct = default) => AmazonBookInfo.GetAmazonBookInfo(this, ct);
    }
}