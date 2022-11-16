using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using AmazonScrapper.Data.Jobs;

namespace Tests
{
    [TestClass]
    public class CollectionTest
    {
        [TestMethod]
        public void TestJobsCollection()
        {
            foreach (var item in new JobsCollection())
            {
                Assert.IsInstanceOfType(item, typeof(IJob));
                Assert.IsInstanceOfType(item, typeof(Job));
            }
        }
    }
}
