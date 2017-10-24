using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.IO;
using Screener;
using System.Configuration;

namespace QuoteService
{
    public class QuoteDownloader : IQuoteDownloader
    {
        private const string StooqUrl = "http://finance.google.com/finance/historical?q=WSE:{0}&output=csv";
        private const string MainPageStooqUrl = "https://stooq.pl/q/?s={0}";
        private const string FindNameToken = "&nbsp;";

        private string tempFile;

        public QuoteDownloader()
        {
            tempFile = ConfigurationManager.AppSettings["tempFileLocation"];
        }

        public Company GetQuote(string ticker)
        {
            try
            {
                downloadCsv(ticker);
                return getCompanyFromCsv(ticker);
            }
            catch (Exception e)
            {
                throw;
            }
            finally
            {
                if (File.Exists(tempFile))
                {
                    File.Delete(tempFile);
                }
            }
        }

        private void downloadCsv(string ticker)
        {
            using (var client = new WebClient())
            {
                client.DownloadFile(string.Format(StooqUrl, ticker.ToLower()), tempFile);
            }
        }

        private string getName(string ticker)
        {
            var request = (HttpWebRequest)WebRequest.Create(string.Format(MainPageStooqUrl, ticker));
            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            using (Stream stream = response.GetResponseStream())
            using (StreamReader reader = new StreamReader(stream))
            {
                var html = reader.ReadToEnd();
                var findTicker = html.IndexOf(string.Format("({0})", ticker));
                var start = html.LastIndexOf(FindNameToken, findTicker);
                start += FindNameToken.Length;

                var end = html.IndexOf("(", start);
                return html.Substring(start, end - start);
            }
        }

        private Candle[] loadCompanyChart()
        {
            var reslist = new List<Candle>();

            using (var sr = new StreamReader(tempFile))
            {
                sr.ReadLine();  //Skip header
                string line = string.Empty;

                while((line = sr.ReadLine()) != null)
                {
                    line = line.Replace(',', ';');
                    line = line.Replace('.', ',');

                    var split = line.Split(';');

                    var candle = new Candle()
                    {
                        Open = decimal.Parse(split[1]),
                        High = decimal.Parse(split[2]),
                        Low = decimal.Parse(split[3]),
                        Close = decimal.Parse(split[4]),
                        Volume = decimal.Parse(split[5])
                    };

                    reslist.Add(candle);
                }
            }

            return reslist.ToArray().Reverse().ToArray();
        }

        private Company getCompanyFromCsv(string ticker)
        {
            var name = getName(ticker).Trim();
            var chart = loadCompanyChart();

            return new Company()
            {
                Name = name,
                Chart = chart
            };
        }
    }
}