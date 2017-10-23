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
        private MainWindowVM _mainWindow;

        public MainWindow()
        {
            InitializeComponent();
            _symbolLoader = new SymbolLoader();
            _mainWindow = new MainWindowVM(new QuoteService.QuoteDownloader(), _symbolLoader);
            _mainWindow.CompaniesUpdatedHandler += OnCompaniesUpdated;

            Bind();
        }

        private void Bind()
        {
            symbolsListView.ItemsSource = _symbolLoader.GetSymbols().Select(x => x.Symbol).ToList();
            companyDataGrid.ItemsSource = _mainWindow.Companies;
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            _mainWindow.LoadCompanies(Convert.ToDecimal(percentageTextBox.Text) / 100);
        }

        private void OnCompaniesUpdated(object sender, EventArgs e)
        {
            companyDataGrid.Items.Refresh();
        }
    }
}
