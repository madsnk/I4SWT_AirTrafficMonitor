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
        private ITrack _uut_track;
        private ISeperationEvent _uut_seperationEvent;
        private IAirSpace _uut_airSpace;

        private ITransponderReceiver _receiver;
        private IConsoleWrapper _console;
        private List<ITrack> _tracks;
        private List<ISeperationEvent> _seperationEvents;

        private string testTag = "XXX123";
        private DateTime testTime = new DateTime(2017, 10, 10, 10, 10, 10, 0);
        private int testXPos = 100;
        private int testYPos = 100;
        private int testAlt = 3000;

        //private string _timeString;

        [SetUp]
        public void SetUp()
        {
            _console = Substitute.For<IConsoleWrapper>();
            _receiver = Substitute.For<ITransponderReceiver>();


            _tracks = new List<ITrack>();

            _uut_seperationEvent = new SeperationEvent();
            _seperationEvents = new List<ISeperationEvent>();

            _uut_trackFactory = new StandardTrackFactory();
            _uut_track = new Track(testTag, testXPos, testYPos, testAlt, testTime);
            _uut_airSpace = new AirSpace(10000, 10000, 90000, 90000, 500, 20000, 300, 5000);

            _uut_log = new Log("IntergrationTestLog");

            _uut_atmController = new ATMController(_receiver, _uut_trackFactory, _console, _uut_airSpace, _tracks,
                _seperationEvents, _uut_log);
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


    }
    
}
