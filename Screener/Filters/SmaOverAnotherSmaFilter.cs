using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Screener.Filters
{
    public class SmaOverAnotherSmaFilter : IFilter
    {
        private int sma1Period;
        private int sma2Period;

        public SmaOverAnotherSmaFilter(int upperSma, int lowerSma)
        {
            sma1Period = upperSma;
            sma2Period = lowerSma;
        }

        public Company[] Filter(Company[] companies)
        {
            List<Company> result = new List<Company>();

            foreach(var company in companies)
            {
                var sma1 = SMA.Get(sma1Period, company.Chart);
                var sma2 = SMA.Get(sma2Period, company.Chart);

                if (sma1 > sma2)
                {
                    result.Add(company);
                }
            }

            return result.ToArray();
        }
    }
}
