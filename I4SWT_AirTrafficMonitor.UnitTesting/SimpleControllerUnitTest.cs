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
        private ITrack _track;
        private ITransponderReceiver _receiver;
        private IConsoleWrapper _console;

        [SetUp]
        public void SetUp()
        {
            _receiver = Substitute.For<ITransponderReceiver>();
            _uut = new SimpleController(_receiver,_console);
        }

        [Test]
        public void someTest()
        {
            //var args = new RawTransponderDataEventArgs() { TransponderData = "rawstringTest" };
            var fakeStrings = new List<string>();

            fakeStrings.Add("dummyString");

            _receiver.TransponderDataReady += Raise.EventWith(new RawTransponderDataEventArgs(fakeStrings));

            //Assert.That(_uut.);

        }



    }
}
