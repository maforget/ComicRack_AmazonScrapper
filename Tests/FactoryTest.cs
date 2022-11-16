using Microsoft.VisualStudio.TestTools.UnitTesting;
using AmazonScrapper.Tools;
using AmazonScrapper.Data.Jobs;

namespace Tests
{
    [TestClass]
    public class FactoryTest
    {
        [TestMethod]
        public void TestFactoryInterface()
        {
            foreach (var item in Factory.GetEnumerableOfInterface<IJob>())
            {
                Assert.IsInstanceOfType(item, typeof(IJob));
            }

            foreach (var item in Factory.GetEnumerableOfInterface<Job>())
            {
                Assert.AreNotSame(item.GetType(), typeof(Job));
                Assert.IsInstanceOfType(item, typeof(Job));
            }
        }

        [TestMethod]
        public void TestFactoryType()
        {
            foreach (var item in Factory.GetEnumerableOfType<Job>())
            {
                Assert.IsInstanceOfType(item, typeof(Job));
            }
        }
    }
}
