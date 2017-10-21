using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using QuoteService;
using Screener;

namespace UnitTests
{
    [TestClass]
    public class QuoteDownloaderTest
    {
        [TestMethod]
        public void DownloadSingleQuoteTest()
        {
            IQuoteDownloader downloader = new QuoteDownloader();
            var result = downloader.GetQuote("ENG");

            Assert.AreEqual("Energa SA", result.Name);
            Assert.AreNotEqual(0, result.Chart.Length);
        }

        [TestMethod]
        public void DownloadQuoteNameCheck()
        {
            IQuoteDownloader downloader = new QuoteDownloader();
            var result = downloader.GetQuote("ENG");

            Assert.AreEqual("Energa SA", result.Name);
        }

        [TestMethod]
        public void DownloadQuoteChartCheck()
        {
            IQuoteDownloader downloader = new QuoteDownloader();
            var result = downloader.GetQuote("ENG");

            Assert.AreNotEqual(0, result.Chart[0].Open);
            Assert.AreNotEqual(0, result.Chart[0].High);
            Assert.AreNotEqual(0, result.Chart[0].Low);
            Assert.AreNotEqual(0, result.Chart[0].Close);
        }
    }
}
