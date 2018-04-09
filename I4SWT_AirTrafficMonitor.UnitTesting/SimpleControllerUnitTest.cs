using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using I4SWT_AirTrafficMonitor.Classes;
using NUnit.Framework;
using NSubstitute;
using I4SWT_AirTrafficMonitor.Classes.Controllers;
using I4SWT_AirTrafficMonitor.Classes.Tracks;
using TransponderReceiver;

namespace I4SWT_AirTrafficMonitor.UnitTesting
{
    [TestFixture]
    class SimpleControllerUnitTest
    {
        private SimpleController _uut;
        //private ITrack _track;
        private ITransponderReceiver _receiver;
        private IConsoleWrapper _console;
        private List<ITrack> _tracks;

        [SetUp]
        public void SetUp()
        {
            _console = Substitute.For<IConsoleWrapper>();
            _receiver = Substitute.For<ITransponderReceiver>();
            _uut = new SimpleController(_receiver,_console);
        }

        [Test]
        public void someTest()
        {
            //var args = new RawTransponderDataEventArgs() { TransponderData = "rawstringTest" };
            var fakeStrings = new List<string>
            {
                "ATR423;39045;12932;14000;20151006213456789"
            };

            _receiver.TransponderDataReady += Raise.EventWith(new RawTransponderDataEventArgs(fakeStrings));

            _console.Received().Report("OnNewTrackData Called");
        }



    }
}
