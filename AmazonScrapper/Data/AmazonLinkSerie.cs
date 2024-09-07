using AmazonScrapper.Web;
using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using AmazonScrapper.Data.Parser;

namespace AmazonScrapper.Data
{
	public class AmazonLinkSerie : AmazonLink
    {
        const int maxSize = 500;
        public override string Title { get; }
        public override string SerieDisplayText { get; }
        public string FullCollectionlURL => $@"https://www.amazon.{TLD}/kindle-dbs/productPage/ajax/seriesAsinList?asin={ASIN}&pageNumber=1&pageSize={maxSize}";

        public AmazonLinkSerie(string asin, TLDs tld = TLDs.com)
            : base(asin, string.Empty, string.Empty, string.Empty, tld)
        {

        }

        public AmazonLinkSerie(string title, string link, string imageLink = "", TLDs tld = TLDs.com)
            : base(title, link, imageLink, tld)
        {
            SerieInfo serieInfo = SerieInfo.Parse(title, amazonLinkSerie: this);

            if (serieInfo == null)
                return;

            Title = serieInfo.Serie;
            SerieDisplayText = serieInfo.Count <= 0 ? serieInfo.Serie : $"{serieInfo.Serie} ({serieInfo.Count} book series)";
        }

        public List<AmazonLinkIssues> GetIssues(CancellationToken ct = default)
        {
            var results = new List<AmazonLinkIssues>();

            if (ct.IsCancellationRequested) 
                ct.ThrowIfCancellationRequested();

            //Get Normal page to get the collection info at the top
            var body = Fetcher.Instance.GetBody(Link, ct);
            var collectionInfo = CollectionPage.ParseCollectionInfo(body);
            results = CollectionPage.ParseIssues(body, collectionInfo, this);

            if (results.Count == 20)
            {
                if (ct.IsCancellationRequested) 
                    ct.ThrowIfCancellationRequested();

                //Use the ajax page to get all the issues
                var ajax = Fetcher.Instance.GetHtmlDocument(FullCollectionlURL, ct);
                results = CollectionPage.ParseIssues(ajax, collectionInfo, this);
            }

            return results;
        }
    }
}
