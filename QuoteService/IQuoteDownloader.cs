using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ScreenerDto;

namespace QuoteService
{
    public interface IQuoteDownloader
    {
        CompanyDto GetQuote(string ticker);
    }
}
