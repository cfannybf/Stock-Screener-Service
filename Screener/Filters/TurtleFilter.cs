using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Screener.Filters
{
    public class TurtleFilter : IFilter
    {
        public TurtleFilter(int fastSma, int slowSma, int donchianChannel, int allowShort)
        {

        }

        public Company[] Filter(Company[] companies)
        {
            throw new NotImplementedException();
        }
    }
}
