using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using IntegrationTests.QuoteDownloadService;
using System.Linq;

namespace IntegrationTests
{
    [TestClass]
    public class QuoteDownloadServiceTest
    {
        [TestMethod]
        public void QuoteDownloadSmokeTest()
        {
            var quoteService = new QuoteDownloadServiceClient();
            var msg = quoteService.Echo("test");

            Assert.AreEqual("test", msg);
        }
    }
}
