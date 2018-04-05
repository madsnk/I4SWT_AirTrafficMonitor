using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using I4SWT_AirTrafficMonitor.Classes;
using I4SWT_AirTrafficMonitor.Classes.Tracks;
using TransponderReceiver;

namespace I4SWT_AirTrafficMonitor.UnitTesting
{
    [TestFixture]
    public class ATMUnitTest
    {
        private ITrackFactory _trackList;
        private IConsoleWrapper _output;
        private ITransponderReceiver _transponder;

        [SetUp]
        public void SetUp()
        {
            _output = new ConsoleWrapper();
            _trackList = new StandardTrackFactory;
        }


    }
}
