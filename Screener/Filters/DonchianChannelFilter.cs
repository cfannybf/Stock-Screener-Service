using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Screener.Filters
{
    public class DonchianChannelFilter : IFilter
    {
        private decimal percentage;
        private int period;
        private int direction;

        public DonchianChannelFilter(int period, int direction, decimal percentage = 0)
        {
            this.percentage = percentage;
            this.period = period;
            this.direction = direction;
        }

        public Company[] Filter(Company[] companies)
        {
            List<Company> result = new List<Company>();
            foreach (var company in companies)
            {
                if (direction > 0)
                {
                    var donchian = DonchianChannell.High(period, company.Chart);
                    var last = company.Chart.Last().High;
                    var percentage = (donchian - last) / donchian;
                    if (last >= donchian || (percentage > 0 && percentage < this.percentage))
                    {
                        result.Add(company);
                    }
                }
                else
                {
                    var donchian = DonchianChannell.Low(period, company.Chart);
                    var last = company.Chart.Last().Low;
                    var percentage = (last - donchian) / last;
                    if (last <= donchian || (percentage > 0 && percentage < this.percentage))
                    {
                        result.Add(company);
                    }
                }
            }

            return result.ToArray();
        }
    }
}
