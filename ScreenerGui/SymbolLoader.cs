using ScreenerGui.ViewModel;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScreenerGui
{
    public class SymbolLoader
    {
        private string _symbolFile = ConfigurationManager.AppSettings["SymbolsFileName"];

        private List<SymbolVM> _symbols = null;

        public IEnumerable<SymbolVM> GetSymbols()
        {
            if (_symbols == null)
            {
                _symbols = new List<SymbolVM>();

                using (var sr = new StreamReader(_symbolFile))
                {
                    string line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        _symbols.Add(new SymbolVM()
                        {
                            Symbol = line
                        });
                    }
                }
            }

            return _symbols;
        }
    }
}
