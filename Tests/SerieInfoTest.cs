using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using AmazonScrapper.Data;
using AmazonScrapper.Data.Parser;

namespace Tests
{
    [TestClass]
    public class SerieInfoTest
    {
        [TestMethod]
        public void TestSerieInfo()
        {
            var serieInfo = SerieInfo.Parse("Book 5 of 19: Farmhand");

            Assert.AreEqual("Book 5 of 19: Farmhand", serieInfo.RawText);
            Assert.AreEqual("Farmhand (5 of 19 book series)", serieInfo.DisplayText);
            Assert.AreEqual("Farmhand", serieInfo.Serie);
            Assert.AreEqual("5", serieInfo.Number);
            Assert.AreEqual(19, serieInfo.Count);
        }

        [TestMethod]
        public void TestSerieInfo2()
        {
            var serieInfo = SerieInfo.Parse("Book 3 of 25: Batman/Superman (2019-)", bookTitle: "Batman/Superman (2019-) #2");

            Assert.AreEqual("Book 3 of 25: Batman/Superman (2019-)", serieInfo.RawText);
            Assert.AreEqual("Batman/Superman (2019-) (2 of 25 book series)", serieInfo.DisplayText);
            Assert.AreEqual("Batman/Superman (2019-)", serieInfo.Serie);
            Assert.AreEqual("2", serieInfo.Number);
            Assert.AreEqual(25, serieInfo.Count);
        }


        [TestMethod]
        public void TestSerieInfoAlt()
        {
            var serieInfo = SerieInfo.Parse("Part of: Usagi Yojimbo Saga (10 Books)", bookTitle: "Usagi Yojimbo Saga Volume 1 (Second Edition)");

            Assert.AreEqual("Part of: Usagi Yojimbo Saga (10 Books)", serieInfo.RawText);
            Assert.AreEqual("Usagi Yojimbo Saga (1 of 10 book series)", serieInfo.DisplayText);
            Assert.AreEqual("Usagi Yojimbo Saga", serieInfo.Serie);
            Assert.AreEqual("1", serieInfo.Number);
            Assert.AreEqual(10, serieInfo.Count);
        }

        [TestMethod]
        public void TestSerieInfoAlt2()
        {
            var serieInfo = SerieInfo.Parse("Chapter 9 of 9: What I Love About You", bookTitle: "What I Love About You Vol. 9");

            Assert.AreEqual("Chapter 9 of 9: What I Love About You", serieInfo.RawText);
            Assert.AreEqual("What I Love About You (9 of 9 book series)", serieInfo.DisplayText);
            Assert.AreEqual("What I Love About You", serieInfo.Serie);
            Assert.AreEqual("9", serieInfo.Number);
            Assert.AreEqual(9, serieInfo.Count);
        }

        [TestMethod]
        public void TestSerieInfoAlt3()
        {
            var serieInfo = SerieInfo.Parse("Part of: The Way of the Househusband", bookTitle: "The Way of the Househusband, Vol. 9");

            Assert.AreEqual("Part of: The Way of the Househusband", serieInfo.RawText);
            Assert.AreEqual("The Way of the Househusband (book series)", serieInfo.DisplayText);
            Assert.AreEqual("The Way of the Househusband", serieInfo.Serie);
            Assert.AreEqual("9", serieInfo.Number);
            Assert.AreEqual(-1, serieInfo.Count);
        }

        [TestMethod]
        public void TestSerieInfoForCollection()
        {
            var serieInfo = SerieInfo.Parse("5", "Farmhand", "19");

            Assert.IsNull(serieInfo.RawText);
            Assert.AreEqual("Farmhand (5 of 19 book series)", serieInfo.DisplayText);
            Assert.AreEqual("Farmhand", serieInfo.Serie);
            Assert.AreEqual("5", serieInfo.Number);
            Assert.AreEqual(19, serieInfo.Count);
        }
    }
}
