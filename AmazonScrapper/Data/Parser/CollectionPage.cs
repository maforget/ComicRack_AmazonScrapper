using AmazonScrapper.Data.Parser.Collection;
using AmazonScrapper.Web;
using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AmazonScrapper.Data.Parser
{
    public static class CollectionPage
    {
        public static CollectionInfo ParseCollectionInfo(HtmlNode body)
        {
            string collectionTitle = string.Empty;
            string collectionSize = string.Empty;

            var collection = body?.SelectSingleNode(".//title")?.InnerText?.DecodeHTML();
            if (collection != null)
            {
                Regex regex = new Regex(@"(?<title>[^()]+).*?(?<count>\d+)", RegexOptions.IgnoreCase);
                collectionTitle = regex.Match(collection)?.Groups["title"]?.Value?.Trim();
                collectionSize = regex.Match(collection)?.Groups["count"]?.Value?.Trim();
            }

            return new CollectionInfo(collectionTitle, collectionSize);
        }

        public static List<AmazonLinkIssues> ParseIssues(HtmlNode body, CollectionInfo collectionInfo, AmazonLinkSerie linkSerie)
        {
            var results = new List<AmazonLinkIssues>();
            var nodes = GetIssuesNodes(body);
            foreach (HtmlNode node in nodes)
            {
                var parser = new ParserManager<IParserCollection>(node);
                var title = parser.Get<Title>().Result;
                var link = parser.Get<Link>().Result;
                var largeImage = parser.Get<Cover>().Result;
                var amazon = new AmazonLinkIssues(title, link, largeImage);
                var serieInfo = ParseSeriesInfo(node, collectionInfo, linkSerie);

                if (serieInfo != null)
                    amazon.SerieInfo = serieInfo;

                if (amazon != null && !results.Any(x => x.ASIN == amazon.ASIN))
                    results.Add(amazon);
            }

            return results;
        }

        /// <summary>
        /// Parses the Series from the Collection Page
        /// </summary>
        /// <param name="node">The HtmlNode for the collection item</param>
        /// <returns>a SerieInfo</returns>
        public static SerieInfo ParseSeriesInfo(HtmlNode node, CollectionInfo collectionInfo, AmazonLinkSerie linkSerie)
        {
            var collection = new ParserManager<IParserCollection>(node);
            var title = collection.Get<Title>().ToString();
            var number = Number.ParseFromTitle(title);
            var index = Regex.Match(node.Id, @".+_(\d+)$", RegexOptions.IgnoreCase)?.Groups[1]?.Value;//Use the index instead if no number
            var num = string.IsNullOrEmpty(number) ? index : number;//if no number in the title, then take the index

            var serieInfo = SerieInfo.Parse(num, collectionInfo.Title, collectionInfo.Size, linkSerie);
            return serieInfo;
        }

        private static List<HtmlNode> GetIssuesNodes(HtmlNode body)
        {
            List<HtmlNode> nodes = new List<HtmlNode>();

            string selector = @"//div[contains(@class, 'series-childAsin-item')]";
            var nodeCollection = body?.SelectNodes(selector);

            if (nodeCollection != null)
                nodes.AddRange(nodeCollection);

            return nodes;
        }
    }
}
