using Screener;
using Screener.Filters;
using ScreenerGui.Events;
using ScreenerGui.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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

        public event EventHandler<ProgressChangedEventArgs> ProgressChangedHandler;
        public event EventHandler CompaniesUpdatedHandler;

        public MainWindow()
        {
            InitializeComponent();
            _symbolLoader = new SymbolLoader();
            _mainWindow = new MainWindowVM(new QuoteService.QuoteDownloader(), _symbolLoader);

            CompaniesUpdatedHandler += OnCompaniesUpdated;
            ProgressChangedHandler += OnProgressChanged;

            _mainWindow.CompaniesUpdatedHandler += (s, e) =>
            {
                CompaniesUpdatedHandler(s, e);
            };

            _mainWindow.ProgressChangedHandler += (s, e) =>
            {
                ProgressChangedHandler(s, e);
            };

            Bind();
        }

        private void Bind()
        {
            symbolsListView.ItemsSource = _symbolLoader.GetSymbols().Select(x => x.Symbol).ToList();
            companyDataGrid.ItemsSource = _mainWindow.Companies;
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            Thread t = new Thread((d) => _mainWindow.LoadCompanies((decimal)d));
            t.Start(Convert.ToDecimal(percentageTextBox.Text) / 100);
        }

        private void OnCompaniesUpdated(object sender, EventArgs e)
        {
            if (!Dispatcher.CheckAccess())
            {
                var CompaniesUpdated = CompaniesUpdatedHandler as EventHandler;
                Dispatcher.Invoke(CompaniesUpdated, new object[] { sender, e });
            }
            else
            {
                companyDataGrid.Items.Refresh();
                searchProgressBar.Value = 0;
            }
        }

        private void OnProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            if (!Dispatcher.CheckAccess())
            {
                var ProgressChanged = ProgressChangedHandler as EventHandler<ProgressChangedEventArgs>;
                Dispatcher.Invoke(ProgressChanged, new object[] { sender, e });
            }
            else
            {
                searchProgressBar.Value = e.Progress;
            }
        }
    }
}
