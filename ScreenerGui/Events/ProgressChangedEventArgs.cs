using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScreenerGui.Events
{
    public class ProgressChangedEventArgs : EventArgs
    {
        public int Progress { get; set; }
    }
}
