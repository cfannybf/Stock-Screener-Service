using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using ScreenerDto;
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
            var companiesDto = new List<CompanyDto>();
            var downloader = new QuoteDownloader();

            foreach (var ticker in tickers)
            {
                try
                {
                    var company = downloader.GetQuote(ticker);
                    companiesDto.Add(company);
                }
                catch (Exception)
                {
                    unprocessed.Add(ticker);
                }
            }

            var companies = companiesDto.Select(x => new Company()
            {
                Name = x.Name,
                Chart = x.Chart.Select(y => new Candle()
                {
                    Open = y.Open,
                    High = y.High,
                    Low = y.Low,
                    Close = y.Close,
                    Volume = y.Volume
                }).ToArray()
            }).ToArray();

            //TODO: Add params
            companies = new LifetimeFilter(110).Filter(companies);
            companies = new SmaOverAnotherSmaFilter(50, 100).Filter(companies);
            companies = new DonchianChannelFilter(20, 1, percentage.Value).Filter(companies);

            var retval = companies.Select(x => x.Name).ToList();

            if (unprocessed.Count > 0)
            {
                retval.Add("Couldn't process: " + string.Join(";", unprocessed.ToArray()));
            }

            return retval.ToArray();
        }
    }
}
