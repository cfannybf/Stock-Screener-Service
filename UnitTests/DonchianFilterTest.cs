using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Screener;
using Screener.Filters;

namespace UnitTests
{
    [TestClass]
    public class DonchianFilterTest
    {
        [TestMethod]
        public void DonchianUpBreakout()
        {
            var companies = GenerateCompanies();
            var filter = new DonchianChannelFilter(6, 1);

            var result = filter.Filter(companies);
            var company = result[0];

            Assert.AreEqual(1, result.Length);
            Assert.AreEqual("UpBreakout", company.Name);
        }

        [TestMethod]
        public void DonchianDownBreakout()
        {
            var companies = GenerateCompanies();
            var filter = new DonchianChannelFilter(6, -1);

            var result = filter.Filter(companies);
            var company = result[0];
            var company2 = result[1];

            Assert.AreEqual(2, result.Length);
            Assert.AreEqual("DownBreakout", company.Name);
            Assert.AreEqual("DownBreakout2", company2.Name);
        }

        [TestMethod]
        public void DonchianUpNearBreakout()
        {
            var companies = GenerateCompanies();
            var filter = new DonchianChannelFilter(6, 1, 0.05M);

            var result = filter.Filter(companies);
            var company = result[0];
            var company2 = result[1];

            Assert.AreEqual(2, result.Length);
            Assert.AreEqual("UpBreakout", company.Name);
            Assert.AreEqual("NearUpBreakout", company2.Name);
        }

        public Company[] GenerateCompanies()
        {
            return new Company[]
            {
                new Company()
                {
                    Name = "UpBreakout",
                    Chart = new Candle[]
                    {
                        new Candle() { High = 10, Low = 8 },
                        new Candle() { High = 9, Low = 7 },
                        new Candle() { High = 8, Low = 6 },
                        new Candle() { High = 7, Low = 5 },
                        new Candle() { High = 9, Low = 7 },
                        new Candle() { High = 10, Low = 8 }
                    }
                },
                new Company()
                {
                    Name = "NearUpBreakout",
                    Chart = new Candle[]
                    {
                        new Candle() { High = 100, Low = 80 },
                        new Candle() { High = 90, Low = 70 },
                        new Candle() { High = 80, Low = 60 },
                        new Candle() { High = 70, Low = 50 },
                        new Candle() { High = 90, Low = 70 },
                        new Candle() { High = 96, Low = 76 }
                    }
                },
                new Company()
                {
                    Name = "DownBreakout",
                    Chart = new Candle[]
                    {
                        new Candle() { Low = 1, High = 5 },
                        new Candle() { Low = 2, High = 6 },
                        new Candle() { Low = 3, High = 7 },
                        new Candle() { Low = 4, High = 8 },
                        new Candle() { Low = 2, High = 6 },
                        new Candle() { Low = 1, High = 5 }
                    }
                },
                new Company()
                {
                    Name = "DownBreakout2",
                    Chart = new Candle[]
                    {
                        new Candle() { Low = 2, High = 5 },
                        new Candle() { Low = 3, High = 6 },
                        new Candle() { Low = 4, High = 7 },
                        new Candle() { Low = 5, High = 8 },
                        new Candle() { Low = 2, High = 6 },
                        new Candle() { Low = 1, High = 5 }
                    }
                },
            };
        }
    }
}
