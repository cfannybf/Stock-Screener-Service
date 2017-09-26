using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace IntegrationTests
{
    [TestClass]
    public class QuoteDownloadServiceTest
    {
        [TestMethod]
        public void QuoteDownloadSmokeTest()
        {
            var quoteService = new QuoteDownloadService.QuoteDownloadServiceClient();
            var msg = quoteService.Echo("test");

            Assert.AreEqual("test", msg);
        }
    }
}
