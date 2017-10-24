using Screener;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScreenerGui.ViewModel
{
    public class CompanyVM : ViewModelBase
    {
        private Company _company;

        public string Name
        {
            get
            {
                return _company.Name;
            }
            set
            {
                _company.Name = value;
                OnPropertyChanged();
            }
        }

        public string LastPrice
        {
            get
            {
                return _company.Chart.Last().Close.ToString("0.##");
            }
        }

        public Candle[] Chart
        {
            get
            {
                return _company.Chart;
            }
        }

        public CompanyVM(Company company)
        {
            _company = company;
        }
    }
}
