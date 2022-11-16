using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using AmazonScrapper.Data.Parser;

namespace Tests
{
    [TestClass]
    public class Parser
    {
        [TestMethod]
        public void TestNumber()
        {
            var number1 = Number.ParseFromTitle("Action Comics 2022 Annual (2022) #1 (Action Comics (2016-))");
            var number2 = Number.ParseFromTitle("Action Comics (2016-) #1042");
            var number3 = Number.ParseFromTitle("Action Comics #933 (2011-2016)");
            var number4 = Number.ParseFromTitle("Action Comics (1987) #1042");
            var number5 = Number.ParseFromTitle("Action Comics [1987] #1042");
            var number6 = Number.ParseFromTitle("Farmhand T01 (French Edition)");

            Assert.AreEqual("1", number1);
            Assert.AreEqual("1042", number2);
            Assert.AreEqual("933", number3);
            Assert.AreEqual("1042", number4);
            Assert.AreEqual("1042", number5);
            Assert.AreEqual("01", number6);
        }

        public void TestSeriesParsePage()
        {

        }
    }
}
