using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Screener
{
    public static class DonchianChannell
    {
        public static decimal High(int period, Candle[] candles, int index = -1)
        {
            if (index < 0)
            {
                index = candles.Length - 1;
            }

            var c = candles.Take(index + 1).Skip(index - period + 1);

            return c.Max(x => x.High);
        }

        public static decimal Low(int period, Candle[] candles, int index = -1)
        {
            if (index < 0)
            {
                index = candles.Length - 1;
            }

            var c = candles.Take(index + 1).Skip(index - period + 1);

            return c.Min(x => x.Low);
        }
    }
}
