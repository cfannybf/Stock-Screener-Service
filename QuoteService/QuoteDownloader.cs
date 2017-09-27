using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ScreenerDto;
using System.Net;
using System.IO;

namespace QuoteService
{
    public class QuoteDownloader : IQuoteDownloader
    {
        private const string StooqUrl = "http://finance.google.com/finance/historical?q=WSE:{0}&output=csv";
        private const string MainPageStooqUrl = "https://stooq.pl/q/d/?s={0}";
        private const string tempFile = "C:\\Users\\Cfanny\\Documents\\Visual Studio 2015\\Projects\\Screener\\quote.temp";
        private const string FindNameToken = "Dane historyczne:";

        public CompanyDto GetQuote(string ticker)
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
                var start = html.IndexOf(FindNameToken);
                start += FindNameToken.Length + 1;

                var end = html.IndexOf("(", start);
                return html.Substring(start, end - start);
            }
        }

        private CandleDto[] loadCompanyChart()
        {
            var reslist = new List<CandleDto>();

            using (var sr = new StreamReader(tempFile))
            {
                sr.ReadLine();  //Skip header
                string line = string.Empty;

                while((line = sr.ReadLine()) != null)
                {
                    line = line.Replace(',', ';');
                    line = line.Replace('.', ',');

                    var split = line.Split(';');

                    var candle = new CandleDto()
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

        private CompanyDto getCompanyFromCsv(string ticker)
        {
            var name = getName(ticker).Trim();
            var chart = loadCompanyChart();

            return new CompanyDto()
            {
                Name = name,
                Chart = chart
            };
        }
    }
}