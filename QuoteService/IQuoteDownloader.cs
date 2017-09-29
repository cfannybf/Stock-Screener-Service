using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Screener;

namespace QuoteService
{
    public interface IQuoteDownloader
    {
        Company GetQuote(string ticker);
    }
}
