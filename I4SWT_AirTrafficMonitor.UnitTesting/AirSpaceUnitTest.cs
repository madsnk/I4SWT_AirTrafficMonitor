using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using NSubstitute;
using I4SWT_AirTrafficMonitor.Classes.AirSpace;
using I4SWT_AirTrafficMonitor.Classes.Tracks;
using I4SWT_AirTrafficMonitor.Classes.SeperationEvent;


namespace I4SWT_AirTrafficMonitor.UnitTesting
{
    [TestFixture]
    class AirSpaceUnitTest
    {
        private AirSpace _uut;

        private List<ITrack> _tracks;
        private List<ISeperationEvent> _seperationEvents;

        [SetUp]
        public void SetUp()
        {
            _uut = new AirSpace(10000, 10000, 90000, 90000, 500, 20000, 300, 5000);
            _tracks = new List<ITrack>();
            _seperationEvents = new List<ISeperationEvent>();

        }

        [Test]
        public void CalcTrackDistance_AddTwoDiffrentTrack_correctDistance()
        {
            _tracks.Add(Substitute.For<ITrack>());
            _tracks[0].Xcoor.Returns(1);
            _tracks[0].Ycoor.Returns(1);

            _tracks.Add(Substitute.For<ITrack>());
            _tracks[1].Xcoor.Returns(5);
            _tracks[1].Ycoor.Returns(4);

            Assert.That(_uut.CalcTrackDistance(_tracks[0], _tracks[1]), Is.EqualTo(5));
        }

        [Test]
        public void SortTracks()
        {
            _seperationEvents.Add(Substitute.For<ISeperationEvent>());

            _tracks.Add(Substitute.For<ITrack>());
            _tracks[0].Xcoor.Returns(10001);
            _tracks[0].Ycoor.Returns(10001);
            _tracks[0].Altitude.Returns((uint)1001);

            _tracks.Add(Substitute.For<ITrack>());
            _tracks[1].Xcoor.Returns(10002);
            _tracks[1].Ycoor.Returns(10002);
            _tracks[1].Altitude.Returns((uint)1000);

            _uut.SortTracks(ref _tracks,ref _seperationEvents);

           Assert.That(_seperationEvents.Count,Is.EqualTo(1));

        }
    }
}
