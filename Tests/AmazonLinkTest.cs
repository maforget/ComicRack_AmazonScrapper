using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using AmazonScrapper.Data;
using AmazonScrapper.Data.Parser;

namespace Tests
{
    [TestClass]
    public class AmazonLinkTest
    {
        [TestMethod]
        public void TestLinks()
        {
            var title = "Farmhand #16";
            var asin = "B09TS3PV64";
         
            var amazonLink = new AmazonLinkIssues(asin, title);
            Assert.AreEqual(asin, amazonLink.ASIN);
            Assert.AreEqual(title, amazonLink.Title);
            Assert.AreEqual($@"https://www.amazon.com/dp/{asin}", amazonLink.Link);

            var serieASIN = "B07JJ7S3BR";
            var serieText = "Book 16 of 20: Farmhand";
            var serieLink = $"dp/{serieASIN}?binding=kindle_edition&ref_=dbs_s_ks_series_rwt_tkin&sr=1-18";
            var linkSerie = new AmazonLinkSerie(serieText, serieLink);
            amazonLink.SerieInfo = SerieInfo.Parse(serieText, title, linkSerie);

            Assert.AreEqual(serieASIN, amazonLink.SerieInfo.SerieLink.ASIN);
            Assert.AreEqual($@"https://www.amazon.com/dp/{serieASIN}", amazonLink.SerieInfo.SerieLink.Link);
            Assert.AreEqual("Farmhand", amazonLink.SerieInfo.Serie);

            Assert.AreEqual("Farmhand (16 of 20 book series)", amazonLink.SerieDisplayText);
            Assert.AreEqual(serieText, amazonLink.SerieInfo.RawText);
            Assert.AreEqual("16", amazonLink.SerieInfo.Number);
            Assert.AreEqual(20, amazonLink.SerieInfo.Count);
        }

        [TestMethod]
        public void TestLinks2()
        {
            var title = "Farmhand #1";
            var asin = "B09TS3PV64";
            var link = $@"https://www.amazon.com/dp/{asin}";
            var imageLink = @"https://m.media-amazon.com/images/I/512QwRkbePL._SY300_.jpg";

            var amazonLink = new AmazonLinkIssues(title, link, imageLink);
            Assert.AreEqual(asin, amazonLink.ASIN);
            Assert.AreEqual(title, amazonLink.Title);
            Assert.AreEqual($@"https://www.amazon.com/dp/{asin}", amazonLink.Link);
            Assert.AreEqual(imageLink, amazonLink.ImageLink);

            var serieASIN = "B07JJ7S3BR";
            var serieText = "Book 1 of 20: Farmhand";
            var serieLink = $"dp/{serieASIN}?binding=kindle_edition&ref_=dbs_s_ks_series_rwt_tkin&sr=1-18";
            var linkSerie = new AmazonLinkSerie(serieText, serieLink);
            amazonLink.SerieInfo = SerieInfo.Parse(serieText, title, linkSerie);

            Assert.AreEqual(serieASIN, amazonLink.SerieInfo.SerieLink.ASIN);
            Assert.AreEqual($@"https://www.amazon.com/dp/{serieASIN}", amazonLink.SerieInfo.SerieLink.Link);
            Assert.AreEqual("Farmhand", amazonLink.SerieInfo.Serie);

            Assert.AreEqual("Farmhand (1 of 20 book series)", amazonLink.SerieDisplayText);
            Assert.AreEqual(serieText, amazonLink.SerieInfo.RawText);
            Assert.AreEqual("1", amazonLink.SerieInfo.Number);
            Assert.AreEqual(20, amazonLink.SerieInfo.Count);
        }

        [TestMethod]
        public void TestLinks3()
        {
            var title = "Usagi Yojimbo Saga Volume 5 (Second Edition)";
            var asin = "B09MVLS1GN";

            var amazonLink = new AmazonLinkIssues(asin, title);
            Assert.AreEqual(asin, amazonLink.ASIN);
            Assert.AreEqual(title, amazonLink.Title);
            Assert.AreEqual($@"https://www.amazon.com/dp/{asin}", amazonLink.Link);

            var serieASIN = "B07JJ7S3BR";
            var serieText = "Part of: Usagi Yojimbo Saga (10 books)";
            var serieLink = $"dp/{serieASIN}?binding=kindle_edition&sr=1-1&ref=dbs_dp_rwt_sb_pc_tukn";
            var linkSerie = new AmazonLinkSerie(serieText, serieLink);
            amazonLink.SerieInfo = SerieInfo.Parse(serieText, title, linkSerie);

            Assert.AreEqual(serieASIN, amazonLink.SerieInfo.SerieLink.ASIN);
            Assert.AreEqual($@"https://www.amazon.com/dp/{serieASIN}", amazonLink.SerieInfo.SerieLink.Link);
            Assert.AreEqual("Usagi Yojimbo Saga", amazonLink.SerieInfo.Serie);

            Assert.AreEqual("Usagi Yojimbo Saga (5 of 10 book series)", amazonLink.SerieDisplayText);
            Assert.AreEqual(serieText, amazonLink.SerieInfo.RawText);
            Assert.AreEqual("5", amazonLink.SerieInfo.Number);
            Assert.AreEqual(10, amazonLink.SerieInfo.Count);
        }

        [TestMethod]
        public void TestLinks4()
        {
            var title = "Once & Future Book One Deluxe Edition Vol. 1";
            var asin = "B09K3Z2LX2";

            var amazonLink = new AmazonLinkIssues(asin, title);
            Assert.AreEqual(asin, amazonLink.ASIN);
            Assert.AreEqual(title, amazonLink.Title);
            Assert.AreEqual($@"https://www.amazon.com/dp/{asin}", amazonLink.Link);

            var serieASIN = "B083Q3Q2GL";
            var serieText = "Book 2 of 5: Once & Future";
            var serieLink = $"dp/{serieASIN}?binding=kindle_edition&ref=dbs_dp_rwt_sb_pc_tukn";
            var linkSerie = new AmazonLinkSerie(serieText, serieLink);
            amazonLink.SerieInfo = SerieInfo.Parse(serieText, title, linkSerie);

            Assert.AreEqual(serieASIN, amazonLink.SerieInfo.SerieLink.ASIN);
            Assert.AreEqual($@"https://www.amazon.com/dp/{serieASIN}", amazonLink.SerieInfo.SerieLink.Link);
            Assert.AreEqual("Once & Future", amazonLink.SerieInfo.Serie);

            Assert.AreEqual("Once & Future (1 of 5 book series)", amazonLink.SerieDisplayText);
            Assert.AreEqual(serieText, amazonLink.SerieInfo.RawText);
            Assert.AreEqual("1", amazonLink.SerieInfo.Number);
            Assert.AreEqual(5, amazonLink.SerieInfo.Count);
        }
    }
}
