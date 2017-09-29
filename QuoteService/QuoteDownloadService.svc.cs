using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using Screener;
using Screener.Filters;

namespace QuoteService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    public class QuoteDownloadService : IQuoteDownloadService
    {
        public string Echo(string message)
        {
            return message;
        }

        public string[] FilterCompanies(string[] tickers, decimal? percentage)
        {
            if (percentage == null)
            {
                percentage = 0;
            }

            var unprocessed = new List<string>();
            var companies = new List<Company>();
            var downloader = new QuoteDownloader();

            foreach (var ticker in tickers)
            {
                try
                {
                    var company = downloader.GetQuote(ticker);
                    companies.Add(company);
                }
                catch (Exception)
                {
                    unprocessed.Add(ticker);
                }
            }

            var companiesArr = companies.ToArray();
            //TODO: Add params
            companiesArr = new LifetimeFilter(110).Filter(companiesArr);
            companiesArr = new SmaOverAnotherSmaFilter(50, 100).Filter(companiesArr);
            companiesArr = new DonchianChannelFilter(20, 1, percentage.Value).Filter(companiesArr);

            var retval = companiesArr.Select(x => x.Name).ToList();

            if (unprocessed.Count > 0)
            {
                retval.Add("Couldn't process: " + string.Join(";", unprocessed.ToArray()));
            }

            return retval.ToArray();
        }
    }
}
