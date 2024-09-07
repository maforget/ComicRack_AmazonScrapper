using AmazonScrapper.Web;
using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AmazonScrapper.Data.Parser.Page
{
    public class Series: ParserPage, IResult<SerieInfo>
    {
        public Series(HtmlNode node) : base(node)
        {

        }

        public SerieInfo Result => this.ToType<SerieInfo>();

        public override object Parse()
        {
            var parser = new ParserManager<IParserPage>(Node, TLD);
            var title = parser.Get<Title>().Result;
            var number = Number.ParseFromTitle(title);

            //Series Link Header at top
            var serieLink = Node.SelectSingleNode(".//div[@id='seriesBulletWidget_feature_div']//a")?.Attributes["href"]?.Value?.Trim();
            var serieText = Node.SelectSingleNode(".//div[@id='seriesBulletWidget_feature_div']//a")?.InnerText?.Trim().DecodeHTML();
            var cover = parser.Get<Cover>().Result;
            var amazonlinkSerie = new AmazonLinkSerie(serieText, serieLink, cover, TLD);
            var serieInfo = SerieInfo.Parse(serieText, title, amazonlinkSerie);

            //ALT Series Location in case it doesn't have the header ex: https://www.amazon.com/dp/B08TSWQSZP
            var altSerieCountText = Node.SelectSingleNode(".//div[@id='SeriesWidget']//span")?.InnerText?.Trim();
            altSerieCountText = altSerieCountText ?? string.Empty;
            var altSerieCount = Regex.Match(altSerieCountText, @"\((\d+)\s", RegexOptions.IgnoreCase)?.Groups[1]?.Value;
            var altSerieNode = Node.SelectSingleNode(".//div[@id='seriesParentAsinHolder']");
            var altSerieImage = altSerieNode?.SelectSingleNode(".//img[@id='product-image']")?.Attributes["src"]?.Value?.Trim();
            var altSerieText = altSerieNode?.SelectSingleNode(".//a[@class='a-link-normal']/span")?.InnerText?.Trim();
            var altSerieLink = altSerieNode?.SelectSingleNode(".//a[@class='a-link-normal']")?.Attributes["href"]?.Value?.Trim();
            var altAmazonLinkSerie = new AmazonLinkSerie(altSerieText, altSerieLink, altSerieImage, TLD);
            var altSerieInfo = SerieInfo.Parse(number, altSerieText, altSerieCount, altAmazonLinkSerie);

            return string.IsNullOrEmpty(serieText) ? altSerieInfo : serieInfo;
        }
    }
}
