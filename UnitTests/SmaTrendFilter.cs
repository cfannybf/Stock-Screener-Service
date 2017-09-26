using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Screener;
using Screener.Filters;

namespace UnitTests
{
    /// <summary>
    /// Summary description for SmaTrendFilter
    /// </summary>
    [TestClass]
    public class SmaTrendFilter
    {
        [TestMethod]
        public void FilterUpperTrend()
        {
            var companies = generateCompanies();
            var filter = new SmaOverAnotherSmaFilter(3, 5);

            var result = filter.Filter(companies);
            var first = result[0];
            Assert.AreEqual(1, result.Length);
            Assert.AreEqual("TrendUp", first.Name);
        }

        [TestMethod]
        public void FilterLowerTrend()
        {
            var companies = generateCompanies();
            var filter = new SmaOverAnotherSmaFilter(5, 3);

            var result = filter.Filter(companies);
            var first = result[0];
            Assert.AreEqual(1, result.Length);
            Assert.AreEqual("TrendDown", first.Name);
        }

        private Company[] generateCompanies()
        {
            var companies = new Company[]
            {
                new Company()
                {
                    Name = "TrendUp",
                    Chart = new Candle[]
                    {
                        new Candle() {Close = 1 },
                        new Candle() {Close = 2 },
                        new Candle() {Close = 3 },
                        new Candle() {Close = 4 },
                        new Candle() {Close = 5 },
                        new Candle() {Close = 6 },
                        new Candle() {Close = 7 },
                        new Candle() {Close = 8 },
                        new Candle() {Close = 9 },
                        new Candle() {Close = 10 },
                    }
                },
                new Company()
                {
                    Name = "NoTrend",
                    Chart = new Candle[]
                    {
                        new Candle() {Close = 1 },
                        new Candle() {Close = 1 },
                        new Candle() {Close = 1 },
                        new Candle() {Close = 1 },
                        new Candle() {Close = 1 },
                        new Candle() {Close = 1 },
                        new Candle() {Close = 1 },
                        new Candle() {Close = 1 },
                        new Candle() {Close = 1 },
                        new Candle() {Close = 1 },
                    }
                },
                new Company()
                {
                    Name = "TrendDown",
                    Chart = new Candle[]
                    {
                        new Candle() {Close = 10 },
                        new Candle() {Close = 9 },
                        new Candle() {Close = 8 },
                        new Candle() {Close = 7 },
                        new Candle() {Close = 6 },
                        new Candle() {Close = 5 },
                        new Candle() {Close = 4 },
                        new Candle() {Close = 3 },
                        new Candle() {Close = 2 },
                        new Candle() {Close = 1 },
                    }
                }
            };

            return companies;
        }
    }
}
