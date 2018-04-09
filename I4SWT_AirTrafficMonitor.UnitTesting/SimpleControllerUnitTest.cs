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
        private List<ITrack> _tracks;
        private ITrackFactory _trackFactory;

        [SetUp]
        public void SetUp()
        {
            _console = Substitute.For<IConsoleWrapper>();
            _receiver = Substitute.For<ITransponderReceiver>();
            _trackFactory = new FakeTrackFactory();
            _track = Substitute.For<ITrack>();
            _tracks = new List<ITrack>();
            _uut = new SimpleController(_receiver,_console,_trackFactory,_tracks);
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

            //_track = _trackFactory.CreateTrack(fakeStrings[0]);

            _receiver.TransponderDataReady += Raise.EventWith(new RawTransponderDataEventArgs(fakeStrings));
            _receiver.TransponderDataReady += Raise.EventWith(new RawTransponderDataEventArgs(fakeStrings));

            _track = _tracks.Find(x => x.Tag.Equals("ATR423"));

            _track.Received(1).UpdateTrack(_track);
        }
    }
}
