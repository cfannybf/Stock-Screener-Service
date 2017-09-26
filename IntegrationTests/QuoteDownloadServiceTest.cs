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

        [TestMethod]
        public void QuoteDownloadSingleCompanyTest()
        {
            var quoteService = new QuoteDownloadServiceClient();
            var result = quoteService.GetCompanies(new string[] { "ENG" });

            Assert.AreEqual(1, result.Length);
            var company = result[0];

            Assert.AreEqual("Energa SA", company.Name);
            Assert.AreNotEqual(0, company.Chart.Length);
        }

        [TestMethod]
        public void QuoteDownloadMultipleCompanyTest()
        {
            var quoteService = new QuoteDownloadServiceClient();
            var result = quoteService.GetCompanies(new string[] { "ENG", "GBK" });

            Assert.AreEqual(2, result.Length);
            var eng = result.FirstOrDefault(x => x.Name == "Energa SA");
            var gbk = result.FirstOrDefault(x => x.Name == "Getback");

            Assert.IsNotNull(eng);
            Assert.IsNotNull(gbk);
        }
    }
}
