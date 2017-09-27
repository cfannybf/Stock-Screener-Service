using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Screener;
using Screener.Filters;

namespace UnitTests
{
    [TestClass]
    public class GeneralFilterTest
    {
        [TestMethod]
        public void LifetimeFilterTest()
        {
            var companies = new List<Company>();
            companies.Add(new Company()
            {
                Name = "Short",
                Chart = new Candle[]
                {
                    new Candle()
                }
            });

            companies.Add(new Company()
            {
                Name = "Long",
                Chart = new Candle[]
                {
                    new Candle(),
                    new Candle(),
                    new Candle()
                }
            });

            var result = new LifetimeFilter(2).Filter(companies.ToArray());
            Assert.AreEqual(1, result.Length);
            Assert.AreEqual("Long", result[0].Name);
        }
    }
}
