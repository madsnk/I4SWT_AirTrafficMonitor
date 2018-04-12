using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace I4SWT_AirTrafficMonitor.Classes.Log
{
    public class Log : ILog
    {
        private string _filePath;
        private StringBuilder sb = new StringBuilder();
        //private DirectoryInfo di =  new DirectoryInfo();

        public Log(string filePath)
        {
            _filePath = filePath + String.Format("_{0}.csv", DateTime.Now.ToString("yyyy-MM-dd HH.mm.ss"));
            File.AppendAllText(_filePath, "Time Of Occurrence;First Track Tag;Second Track Tag\n");
        }

        public void Append(string newData)
        {
            File.AppendAllText(_filePath, newData);
        }
    }
}

