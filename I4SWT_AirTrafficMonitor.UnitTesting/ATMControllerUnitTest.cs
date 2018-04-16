﻿using System;
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

namespace I4SWT_AirTrafficMonitor.UnitTesting
{
    [TestFixture]
    class ATMControllerUnitTest
    {
        private ATMController _uut;
        private ITrack _track;
        private ITransponderReceiver _receiver;
        private IConsoleWrapper _console;
        private List<ITrack> _tracks;
        private ITrackFactory _trackFactory;
        private IAirSpace _airSpace;
        private List<ISeperationEvent> _seperationEvents;
        private ILog _log;
        private ISeperationEvent _seperationEvent;

        [SetUp]
        public void SetUp()
        {
            _console = Substitute.For<IConsoleWrapper>();
            _receiver = Substitute.For<ITransponderReceiver>();
            _trackFactory = new FakeTrackFactory();
            _track = Substitute.For<ITrack>();
            _tracks = new List<ITrack>();
            _airSpace = Substitute.For<IAirSpace>();
            _seperationEvent = Substitute.For<ISeperationEvent>();
            _seperationEvents = new List<ISeperationEvent>();
            _log = Substitute.For<ILog>();
            _uut = new ATMController(_receiver, _trackFactory,_console,_airSpace,_tracks, _seperationEvents,_log);
        }

        [Test]
        public void OnNewTrackData_AddNonExixtingData_DataAddedToList()
        {
            var fakeStrings = new List<string>
            {
                "ATR423"
            };

            _track = _trackFactory.CreateTrack(fakeStrings[0]);

            _receiver.TransponderDataReady += Raise.EventWith(new RawTransponderDataEventArgs(fakeStrings));

            Assert.That(_tracks.Count(x => x.Tag.Equals("ATR423")) == 1);
        }

        [Test]
        public void OnNewTrackData_AddExixtingData_OnlyOneCopyOfPlaneInList()
        {
            var fakeStrings = new List<string>
            {
                "ATR423"
            };

            _track = _trackFactory.CreateTrack(fakeStrings[0]);

            _receiver.TransponderDataReady += Raise.EventWith(new RawTransponderDataEventArgs(fakeStrings));
            _receiver.TransponderDataReady += Raise.EventWith(new RawTransponderDataEventArgs(fakeStrings));

            Assert.That(_tracks.Count(x => x.Tag.Equals("ATR423")) == 1);
        }

        [Test]
        public void OnNewTrackData_AddExixtingData_DataUpdatedInList()
        {
            var fakeStrings = new List<string>
            {
                "ATR423"
            };

            _receiver.TransponderDataReady += Raise.EventWith(new RawTransponderDataEventArgs(fakeStrings));
            _receiver.TransponderDataReady += Raise.EventWith(new RawTransponderDataEventArgs(fakeStrings));

            _track = _tracks.Find(x => x.Tag.Equals("ATR423"));

            _track.Received(1).UpdateTrack(_track);
        }

        [Test]
        public void SortTracks_AddRawData_SortTrackCalledWithCorrectTracks()
        {
            var fakeStrings = new List<string>
            {
                "XXX123",
                "YYY321"
            };

            _tracks.Add(_trackFactory.CreateTrack(fakeStrings[0]));
            _tracks.Add(_trackFactory.CreateTrack(fakeStrings[1]));

            _receiver.TransponderDataReady += Raise.EventWith(new RawTransponderDataEventArgs(fakeStrings));
    
            _airSpace.Received(1).SortTracks(ref _tracks,ref _seperationEvents);
        }

        [Test]
        public void Append_createFakeSeperationEvent_logRecivesAppend()
        {
            var fakeStrings = new List<string>
            {
                "XXX123"
            };

            _seperationEvent.csvFormat().Returns("test");
            _seperationEvents.Add(_seperationEvent);

            _receiver.TransponderDataReady += Raise.EventWith(new RawTransponderDataEventArgs(fakeStrings));

            _log.Received(1).Append("test");
        }
    }
}