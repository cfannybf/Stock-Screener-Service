using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Screener
{
    public static class SMA
    {
        public static decimal Get(int period, Candle[] candles, int index = -1)
        {
            if (index < 0)
            {
                index = candles.Length - 1;
            }

            decimal sum = 0;

            for (int i = index - period + 1; i <= index; i++)
            {
                sum += candles[i].Close;
            }

            return sum /= period;
        }
    }
}
