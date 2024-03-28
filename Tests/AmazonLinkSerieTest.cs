using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using AmazonScrapper.Data;

namespace Tests
{
    [TestClass]
    public class AmazonLinkSerieTest
    {
        [TestMethod]
        public void TestSeriesLinks()
        {
            var linkFarm = new AmazonLinkSerie("Book 5 of 19: Farmhand", "dp/B07JJ7S3BR?binding=kindle_edition&ref_=dbs_s_ks_series_rwt_tkin&sr=1-18");

            Assert.AreEqual("B07JJ7S3BR", linkFarm.ASIN);
            Assert.AreEqual(@"https://www.amazon.com/dp/B07JJ7S3BR", linkFarm.Link);
            Assert.AreEqual("Farmhand (19 book series)", linkFarm.SerieDisplayText);
            Assert.AreEqual("Farmhand", linkFarm.Title);
        }

        [TestMethod]
        public void TestSeriesGetIssues()
        {
            var amazonLink = new AmazonLinkSerie("B07JJ7S3BR");
            var issues = amazonLink.GetIssues();//From the collections View
            var thirdIssue = issues[2];

            Assert.AreEqual("B07GL7HPN7", thirdIssue.ASIN);
            Assert.AreEqual("Farmhand #3", thirdIssue.Title);
            Assert.AreEqual(@"https://www.amazon.com/dp/B07GL7HPN7", thirdIssue.Link);

            Assert.AreEqual("B07JJ7S3BR", thirdIssue.SerieInfo.SerieLink.ASIN);
            Assert.AreEqual(@"https://www.amazon.com/dp/B07JJ7S3BR", thirdIssue.SerieInfo.SerieLink.Link);

            Assert.IsNull(thirdIssue.SerieInfo.RawText);
            Assert.AreEqual("Farmhand", thirdIssue.SerieInfo.Serie);
            Assert.AreEqual("3", thirdIssue.SerieInfo.Number);
            //Assert.AreEqual(19, thirdIssue.SerieInfo.Count);

        }

        [TestMethod]
        public void TestSeriesGetIssues2()
        {
            var amazonLink = new AmazonLinkSerie("B0932GH6NJ");
            var issues = amazonLink.GetIssues();//From the collections View
            var thirdIssue = issues[1];

            Assert.AreEqual("B08TSN4GCN", thirdIssue.ASIN);
            Assert.AreEqual("Usagi Yojimbo Saga Volume 2 (Second Edition)", thirdIssue.Title);
            Assert.AreEqual(@"https://www.amazon.com/dp/B08TSN4GCN", thirdIssue.Link);

            Assert.AreEqual("B0932GH6NJ", thirdIssue.SerieInfo.SerieLink.ASIN);
            Assert.AreEqual(@"https://www.amazon.com/dp/B0932GH6NJ", thirdIssue.SerieInfo.SerieLink.Link);

            Assert.IsNull(thirdIssue.SerieInfo.RawText);
            Assert.AreEqual("Usagi Yojimbo Saga", thirdIssue.SerieInfo.Serie);
            Assert.AreEqual("2", thirdIssue.SerieInfo.Number);
            Assert.AreEqual(8, thirdIssue.SerieInfo.Count);
        }

        [TestMethod]
        public void TestLongSeries()
        {
            var amazonLink = new AmazonLinkSerie("B07JK35FPY");
            var issues = amazonLink.GetIssues();//From the collections View
            var fortyIssue = issues[39];

            Assert.AreEqual("B01LXHZGD6", fortyIssue.ASIN);
            Assert.AreEqual("Saga #40", fortyIssue.Title);
            Assert.AreEqual(@"https://www.amazon.com/dp/B01LXHZGD6", fortyIssue.Link);

            Assert.AreEqual("B07JK35FPY", fortyIssue.SerieInfo.SerieLink.ASIN);
            Assert.AreEqual(@"https://www.amazon.com/dp/B07JK35FPY", fortyIssue.SerieInfo.SerieLink.Link);

            Assert.IsNull(fortyIssue.SerieInfo.RawText);
            Assert.AreEqual("40", fortyIssue.SerieInfo.Number);
            Assert.AreEqual("Saga", fortyIssue.SerieInfo.Serie);
            Assert.AreEqual(66, fortyIssue.SerieInfo.Count);
        }

        [TestMethod]
        public void TestSeriesGetIssues4()
        {
            var amazonLink = new AmazonLinkSerie("B07JJK91HZ");
            var issues = amazonLink.GetIssues();//From the collections View
            var thirdIssue = issues[2];

            Assert.AreEqual("B01DUTBP8S", thirdIssue.ASIN);
            Assert.AreEqual("Superman: Reign of the Supermen (Superman: The Death of Superman)", thirdIssue.Title);
            Assert.AreEqual(@"https://www.amazon.com/dp/B01DUTBP8S", thirdIssue.Link);

            Assert.AreEqual("B07JJK91HZ", thirdIssue.SerieInfo.SerieLink.ASIN);
            Assert.AreEqual(@"https://www.amazon.com/dp/B07JJK91HZ", thirdIssue.SerieInfo.SerieLink.Link);

            Assert.IsNull(thirdIssue.SerieInfo.RawText);
            Assert.AreEqual("Superman: The Death of Superman", thirdIssue.SerieInfo.Serie);
            Assert.AreEqual("3", thirdIssue.SerieInfo.Number);
            Assert.AreEqual(5, thirdIssue.SerieInfo.Count);
        }
    }
}
