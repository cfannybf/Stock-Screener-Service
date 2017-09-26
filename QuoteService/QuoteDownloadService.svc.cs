﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using ScreenerDto;

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

        public CompanyDto[] GetCompanies(string[] tickers)
        {
            var downloader = new QuoteDownloader();
            var result = tickers.Select(x => downloader.GetQuote(x)).ToArray();

            return result;
        }
    }
}
