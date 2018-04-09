using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace I4SWT_AirTrafficMonitor.Classes
{
    public interface IConsoleWrapper
    {
        void Report(string arg);
        void Clear();
    }

}
