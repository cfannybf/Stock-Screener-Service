using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Screener.Filters
{
    public class LifetimeFilter : IFilter
    {
        private int lifetime;

        public LifetimeFilter(int lifetime)
        {
            this.lifetime = lifetime;
        }

        public Company[] Filter(Company[] companies)
        {
            return companies.Where(x => x.Chart.Length >= lifetime).ToArray();
        }
    }
}
