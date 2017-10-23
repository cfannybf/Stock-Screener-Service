using Screener;
using Screener.Filters;
using ScreenerGui.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ScreenerGui
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private SymbolLoader _symbolLoader;
        private IQuoteDownloader _downloader;
        private List<CompanyVM> _companies;

        public MainWindow()
        {
            InitializeComponent();
            _symbolLoader = new SymbolLoader();
            _companies = new List<CompanyVM>();
            _downloader = new QuoteService.QuoteDownloader();

            Bind();
        }

        private void Bind()
        {
            symbolsListView.ItemsSource = _symbolLoader.GetSymbols().Select(x => x.Symbol).ToList();
            companyDataGrid.ItemsSource = _companies;
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            LoadCompanies();
        }

        private void LoadCompanies()
        {
            foreach (var symbol in _symbolLoader.GetSymbols())
            {
                try
                {
                    var company = _downloader.GetQuote(symbol.Symbol);
                    _companies.Add(new CompanyVM(company));
                }
                catch (Exception s)
                {
                }
            }

            _companies = FilterCompanies(Convert.ToDecimal(percentageTextBox.Text) / 100).ToList();
            companyDataGrid.ItemsSource = _companies;
        }

        private CompanyVM[] FilterCompanies(decimal? percentage)
        {
            if (percentage == null)
            {
                percentage = 0;
            }

            var companiesArr = _companies.Select(x => new Company() { Name = x.Name, Chart = x.Chart }).ToArray();
            //TODO: Add params
            companiesArr = new LifetimeFilter(110).Filter(companiesArr);
            companiesArr = new SmaOverAnotherSmaFilter(50, 100).Filter(companiesArr);
            companiesArr = new DonchianChannelFilter(20, 1, percentage.Value).Filter(companiesArr);

            var retval = companiesArr.ToList();

            return retval.Select(x => new CompanyVM(x)).ToArray();
        }
    }
}
