﻿using AmazonScrapper.Web;
using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AmazonScrapper.Data.Parser.Search
{
    public class Series: ParserSearch, IResult<AmazonLinkIssues>
    {
        public Series(HtmlNode node) : base(node)
        {
        }

        public AmazonLinkIssues Result => this.ToType<AmazonLinkIssues>();

        /// <summary>
        /// From the search page, assigns the correct data to the AmazonLinkIssues link
        /// </summary>
        /// <param name="node">The HtmlNode for the search item</param>
        /// <param name="link">the AmazonLinkIssues</param>
        public override object Parse()
        {
            var parser = new ParserManager<IParserSearch>(Node);
            var asin = parser.Get<ASIN>().Result;//not needed because it's taken from the link
            var title = parser.Get<Title>().Result;
            var link = parser.Get<Link>().Result;
            var largeImage = parser.Get<Cover>().Result;
            var amazon = new AmazonLinkIssues(title, link, largeImage);
            amazon.ASIN = string.IsNullOrEmpty(amazon.ASIN) ? asin : amazon.ASIN;

            //get series link
            var serieLink = Node.SelectSingleNode(@".//div[@class='a-row']/a")?.Attributes["href"]?.Value?.Trim();
            var serieText = Node.SelectSingleNode(@".//div[@class='a-row']/a/span")?.InnerText?.Trim().DecodeHTML();
            var amazonSerieLink = new AmazonLinkSerie(serieText, serieLink, largeImage);
            SerieInfo serieInfo = SerieInfo.Parse(serieText, title, amazonSerieLink);

            if (serieInfo != null)
                amazon.SerieInfo = serieInfo;

            return amazon;
        }
    }
}
