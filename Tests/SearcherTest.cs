﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using AmazonScrapper.Web;
using AmazonScrapper.Data;
using System;
using System.Text.RegularExpressions;
using AmazonScrapper.Web.Searcher;

namespace Tests
{
	[TestClass]
    public class SearcherTest
    {
        [TestMethod]
        public void TestURL()
        {
            var searcherFarm = Searcher.Create("Farmhand #18", false, true);
            string searchURLfarm = @"https://www.amazon.com/s?k=""Farmhand+%2318""&i=comics-manga";
            Assert.AreEqual(searchURLfarm, searcherFarm.SearchURL);
            Assert.AreEqual("Farmhand #18", searcherFarm.SearchTerm);
            Assert.AreEqual("\"Farmhand+%2318\"", searcherFarm.SearchTermEncoded);

            var searcherFarmLoose = Searcher.Create("Farmhand #18", false, false);
            string searchURLfarmLoose = @"https://www.amazon.com/s?k=Farmhand+%2318&i=comics-manga";
            Assert.AreEqual(searchURLfarmLoose, searcherFarmLoose.SearchURL);
            Assert.AreEqual("Farmhand #18", searcherFarmLoose.SearchTerm);
            Assert.AreEqual("Farmhand+%2318", searcherFarmLoose.SearchTermEncoded);

            var searcherOne = Searcher.Create("one piece", true);
            string searchURLone = @"https://www.amazon.com/s?k=one+piece&i=comics-manga&s=date-desc-rank";
            Assert.AreEqual(searchURLone, searcherOne.SearchURL);
            Assert.AreEqual("one piece", searcherOne.SearchTerm);

            var searcherFr = Searcher.Create("Achille Talon", false, false, TLDs.fr);
			string searchURLfr = @"https://www.amazon.fr/s?k=Achille+Talon&i=stripbooks";
			Assert.AreEqual(searchURLfr, searcherFr.SearchURL);
			Assert.AreEqual("Achille Talon", searcherFr.SearchTerm);
		}

        [TestMethod]
        public void TestGetResults()
        {
            var searcherFarm = Searcher.Create("Farmhand #16", false, true);
            var res = searcherFarm.GetResults();
			Assert.IsTrue(res.Count > 0);
			var linkFarm = res[0] as AmazonLinkIssues;

            Assert.IsNotNull(res);
            Assert.IsInstanceOfType(linkFarm, typeof(AmazonLinkIssues));
            Assert.IsTrue(res.Count > 0);
            Assert.AreEqual("B09TS3PV64", linkFarm.ASIN);
            Assert.AreEqual("Farmhand #16", linkFarm.Title);
            Assert.AreEqual(@"https://www.amazon.com/dp/B09TS3PV64", linkFarm.Link);
            Assert.AreEqual(@"https://m.media-amazon.com/images/I/91n3NKbV9aL._AC_UY654_QL65_.jpg", linkFarm.ImageLink);
            Assert.AreEqual("Farmhand", linkFarm.SerieInfo.Serie);
            Assert.AreEqual("Farmhand", linkFarm.SerieInfo.SerieLink.Title);
            //Assert.IsTrue(Regex.IsMatch(linkFarm.SerieInfo.SerieLink.SerieDisplayText, @"Farmhand \(\d+? book series\)"));
            Assert.AreEqual("16", linkFarm.SerieInfo.Number);
            //Assert.AreEqual(19, linkFarm.SerieInfo.Count);
            Assert.IsTrue(Regex.IsMatch(linkFarm.SerieDisplayText, @"Farmhand \(book series\)"));

        }

		[TestMethod]
		public void TestGetResultsFR()
		{
			var searcherFarm = Searcher.Create("Les Petits Marsus et la grande ville", false, true, TLDs.fr);
			var res = searcherFarm.GetResults();
            Assert.IsTrue(res.Count > 0);
			var link = res[0] as AmazonLinkIssues;

			Assert.IsNotNull(res);
			Assert.IsInstanceOfType(link, typeof(AmazonLinkIssues));
			Assert.IsTrue(res.Count > 0);
			Assert.AreEqual("2374081044", link.ASIN);
			Assert.AreEqual("Les Petits Marsus et la grande ville", link.Title);
			Assert.AreEqual(@"https://www.amazon.fr/dp/2374081044", link.Link);
			Assert.AreEqual(@"https://m.media-amazon.com/images/I/91zqKa82x+L._AC_UY654_QL65_.jpg", link.ImageLink);
		}

		[TestMethod]
        public void TestGetResultsSerie()
        {
            var searcherFarm = Searcher.Create("Farmhand #16", false, true);
            var res = Searcher.GroupResultsBySerie(searcherFarm.GetResults());
            var linkFarm = res[0] as AmazonLinkSerie;

            Assert.IsNotNull(res);
            Assert.IsInstanceOfType(linkFarm, typeof(AmazonLinkSerie));
            Assert.IsTrue(res.Count > 0);
            Assert.AreEqual("B07JJ7S3BR", linkFarm.ASIN);
            Assert.AreEqual("Farmhand", linkFarm.Title);
            //Assert.IsTrue(Regex.IsMatch(linkFarm.SerieDisplayText, @"Farmhand \(\d+ book series\)"));
            Assert.AreEqual(@"https://www.amazon.com/dp/B07JJ7S3BR", linkFarm.Link);
        }
    }
}
