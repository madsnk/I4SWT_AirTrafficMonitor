using System;
using System.Collections.Generic;
using System.Linq;
using I4SWT_AirTrafficMonitor.Classes;
using I4SWT_AirTrafficMonitor.Classes.AirSpace;
using NUnit.Framework;
using NSubstitute;
using I4SWT_AirTrafficMonitor.Classes.Controllers;
using I4SWT_AirTrafficMonitor.Classes.Log;
using I4SWT_AirTrafficMonitor.Classes.Tracks;
using I4SWT_AirTrafficMonitor.Classes.SeperationEvent;
using TransponderReceiver;
using System.IO;

namespace I4SWT_AirTrafficMonitor.IntegrationTesting
{
    [TestFixture]
    class IT1_ATMControllerLog
    {
        private ATMController _uut_atmController;
        private ILog _uut_log;

        private ITrack _track;
        private ITransponderReceiver _receiver;
        private IConsoleWrapper _console;
        private List<ITrack> _tracks;
        private ITrackFactory _trackFactory;
        private IAirSpace _airSpace;
        private List<ISeperationEvent> _seperationEvents;
        private ISeperationEvent _seperationEvent;
        private string _timeString;

        [SetUp]
        public void SetUp()
        {
            _console = Substitute.For<IConsoleWrapper>();
            _receiver = Substitute.For<ITransponderReceiver>();
            _trackFactory = Substitute.For<ITrackFactory>();
            _track = Substitute.For<ITrack>();
            _tracks = new List<ITrack>();
            _airSpace = Substitute.For<IAirSpace>();
            _seperationEvent = Substitute.For<ISeperationEvent>();
            _seperationEvents = new List<ISeperationEvent>();


            _uut_log = new Log("IntergrationTestLog");
            _timeString = DateTime.Now.ToString("yyyy-MM-dd HH.mm.ss");

            _uut_atmController = new ATMController(_receiver, _trackFactory, _console, _airSpace, _tracks, _seperationEvents, _uut_log);
        }

        [Test]
        public void Append_createFakeSeperationEvent_dataInLog()
        {
            _airSpace.FindSeperationEvents(Arg.Any<List<ITrack>>()).Returns(new List<ISeperationEvent> { _seperationEvent });

            var fakeStrings = new List<string>
            {
                "XXX123"
            };

            _seperationEvent.csvFormat().Returns("2018-04-12 08.59.39.481;XXX123;YYY456\n");

            _receiver.TransponderDataReady += Raise.EventWith(new RawTransponderDataEventArgs(fakeStrings));

            var s = File.ReadAllLines("IntergrationTestLog" + "_" + _timeString + ".csv");
            Assert.AreEqual("2018-04-12 08.59.39.481;XXX123;YYY456", s[1]);
        }
    }

}
