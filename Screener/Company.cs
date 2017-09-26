using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Screener
{
    public class Company
    {
        public string Name { get; set; }

        public Candle[] Chart { get; set; }
    }
}
