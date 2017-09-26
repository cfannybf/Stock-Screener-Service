using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Screener;

namespace UnitTests
{
    [TestClass]
    public class IndicatorTest
    {
        [TestMethod]
        public void SmaTest()
        {
            var candleList = new Candle[]
            {
                new Candle() {Close = 1 },
                new Candle() {Close = 2 },
                new Candle() {Close = 3 },
                new Candle() {Close = 4 },
                new Candle() {Close = 5 },
            };

            var smaValue = SMA.Get(5, candleList);
            Assert.AreEqual(3, smaValue);
        }

        [TestMethod]
        public void SmaLastIndexTest()
        {
            var candleList = new Candle[]
            {
                new Candle() {Close = 0 },
                new Candle() {Close = 1 },
                new Candle() {Close = 2 },
                new Candle() {Close = 3 },
                new Candle() {Close = 4 },
                new Candle() {Close = 5 },
            };

            var smaValue = SMA.Get(5, candleList);
            Assert.AreEqual(3, smaValue);
        }

        [TestMethod]
        public void SmaNotLastIndexTest()
        {
            var candleList = new Candle[]
            {
                new Candle() {Close = 0 },
                new Candle() {Close = 1 },
                new Candle() {Close = 2 },
                new Candle() {Close = 3 },
                new Candle() {Close = 4 },
                new Candle() {Close = 5 },
            };

            var smaValue = SMA.Get(5, candleList, 4);
            Assert.AreEqual(2, smaValue);
        }

        [TestMethod]
        public void DonchianHighTest()
        {
            var candleList = new Candle[]
            {
                new Candle() { High = 1 },
                new Candle() { High = 1 },
                new Candle() { High = 1 },
                new Candle() { High = 3 },
                new Candle() { High = 1 },
                new Candle() { High = 1 },
                new Candle() { High = 1 }
            };

            var highValue = DonchianChannell.High(5, candleList);
            Assert.AreEqual(3, highValue);
        }

        [TestMethod]
        public void DonchianLowTest()
        {
            var candleList = new Candle[]
            {
                new Candle() { Low = 3 },
                new Candle() { Low = 3 },
                new Candle() { Low = 3 },
                new Candle() { Low = 1 },
                new Candle() { Low = 3 },
                new Candle() { Low = 3 },
                new Candle() { Low = 3 }
            };

            var lowValue = DonchianChannell.Low(5, candleList);
            Assert.AreEqual(1, lowValue);
        }

        [TestMethod]
        public void DonchianLowMiddleTest()
        {
            var candleList = new Candle[]
            {
                new Candle() { Low = 2 },
                new Candle() { Low = 3 },
                new Candle() { Low = 3 },
                new Candle() { Low = 1 },
                new Candle() { Low = 3 },
                new Candle() { Low = 3 },
                new Candle() { Low = 3 }
            };

            var lowValue = DonchianChannell.Low(3, candleList, 2);
            Assert.AreEqual(2, lowValue);
        }

        [TestMethod]
        public void DonchianHighMiddleTest()
        {
            var candleList = new Candle[]
            {
                new Candle() { High = 1 },
                new Candle() { High = 2 },
                new Candle() { High = 1 },
                new Candle() { High = 3 },
                new Candle() { High = 1 },
                new Candle() { High = 1 },
                new Candle() { High = 1 }
            };

            var highValue = DonchianChannell.High(3, candleList, 2);
            Assert.AreEqual(2, highValue);
        }
    }
}
