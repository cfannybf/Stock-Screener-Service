using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScreenerGui.ViewModel
{
    public class SymbolVM : ViewModelBase
    {
        private string _symbol;

        public string Symbol
        {
            get
            {
                return _symbol;
            }
            set
            {
                _symbol = value;
                OnPropertyChanged();
            }
        }
    }
}
