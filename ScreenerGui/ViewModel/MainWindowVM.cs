using Screener;
using Screener.Filters;
using ScreenerGui.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScreenerGui.ViewModel
{
    public class MainWindowVM : ViewModelBase
    {
        private List<CompanyVM> _companies;
        private IQuoteDownloader _downloader;
        private SymbolLoader _symbolLoader;

        public MainWindowVM(IQuoteDownloader downloader, SymbolLoader loader)
        {
            _downloader = downloader;
            _symbolLoader = loader;
            _companies = new List<CompanyVM>();
        }

        public List<CompanyVM> Companies
        {
            get
            {
                return _companies;
            }
            set
            {
                _companies = value;
                OnPropertyChanged();
            }
        }

        public event EventHandler CompaniesUpdatedHandler;

        public event EventHandler<ProgressChangedEventArgs> ProgressChangedHandler;

        public void LoadCompanies(decimal percentage)
        {
            var count = _symbolLoader.GetSymbols().Count();
            int index = 0;

            foreach (var symbol in _symbolLoader.GetSymbols())
            {
                try
                {
                    index++;
                    var company = _downloader.GetQuote(symbol.Symbol);
                    _companies.Add(new CompanyVM(company));
                }
                catch (Exception s)
                {
                }
                finally
                {
                    ProgressChangedHandler?.Invoke(this, new ProgressChangedEventArgs() { Progress = (int)((float)index / count * 100.0) });
                }
            }

            var filtered = FilterCompanies(percentage);
            _companies.Clear();
            _companies.AddRange(filtered.ToList());

            CompaniesUpdatedHandler?.Invoke(this, new EventArgs());
        }

        private IEnumerable<CompanyVM> FilterCompanies(decimal? percentage)
        {
            if (percentage == null)
            {
                percentage = 0;
            }

            var companiesArr = _companies.Select(x => new Company() { Name = x.Name, Chart = x.Chart }).ToArray();
            companiesArr = new LifetimeFilter(110).Filter(companiesArr);
            companiesArr = new SmaOverAnotherSmaFilter(50, 100).Filter(companiesArr);
            companiesArr = new DonchianChannelFilter(20, 1, percentage.Value).Filter(companiesArr);

            var retval = companiesArr.ToList();

            return retval.Select(x => new CompanyVM(x));
        }
    }
}
