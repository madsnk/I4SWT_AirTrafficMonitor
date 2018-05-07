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
    class IT3_ATMControllerSeparationEventAirSpace
    {
        private ATMController _uut_atmController;
        private ILog _uut_log;
        private ITrackFactory _uut_trackFactory;
        private IAirSpace _uut_airSpace;
        private List<ITrack> _uut_tracks;
        private List<ISeperationEvent> _uut_seperationEvents;

        private ITransponderReceiver _receiver;
        private IConsoleWrapper _console;
        
        private string _timeString;

        [SetUp]
        public void SetUp()
        {
            _console = Substitute.For<IConsoleWrapper>();
            _receiver = Substitute.For<ITransponderReceiver>();

            _uut_tracks = new List<ITrack>();

            _uut_seperationEvents = new List<ISeperationEvent>();

            _uut_trackFactory = new StandardTrackFactory();
            _uut_airSpace = new AirSpace(10000, 10000, 90000, 90000, 500, 20000, 300, 5000);

            _uut_log = new Log("IntergrationTestLog");
            _timeString = DateTime.Now.ToString("yyyy-MM-dd HH.mm.ss");

            _uut_atmController = new ATMController(_receiver, _uut_trackFactory, _console, _uut_airSpace, _uut_tracks,
                _uut_seperationEvents, _uut_log);
        }

        [TearDown]
        public void Cleanup()
        {
            File.Delete("IntergrationTestLog" + "_" + _timeString + ".csv");
        }

        [Test]
        public void Draw_AddTwoConflictingTracks_consoleWriteSeparantionEvent()
        {
            var fakeStrings = new List<string>
            {
                "XXX123;10000;10500;15000;20171212121220111",
                "YYY321;10001;10501;15001;20171212121220112"
            };

            _receiver.TransponderDataReady += Raise.EventWith(new RawTransponderDataEventArgs(fakeStrings));

            _console.Received(1).Report("Time:           2017-12-12 12.12.20.112" + "\r\nInvolved Tags:  XXX123, YYY321\r\nHorizontal Sep: 1\r\nVertical Sep:   1\r\n");
        }

        [Test]
        public void Log_AddTwoConflictingTracks_dataInLog()
        {
            var fakeStrings = new List<string>
            {
                "XXX123;10000;10500;15000;20171212121220111",
                "YYY321;10001;10501;15001;20171212121220112"
            };

            _receiver.TransponderDataReady += Raise.EventWith(new RawTransponderDataEventArgs(fakeStrings));

            var s = File.ReadAllLines("IntergrationTestLog" + "_" + _timeString + ".csv");
            Assert.AreEqual("2017-12-12 12.12.20.112;XXX123;YYY321", s[1]);
        }

    }
    
}
