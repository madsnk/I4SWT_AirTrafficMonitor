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
        public void SortTracks_add3CloseTracksWithinAirspace_3SeparationEvent()
        {
            

            _tracks.Add(Substitute.For<ITrack>());
            _tracks[0].Xcoor.Returns(10003);
            _tracks[0].Ycoor.Returns(10003);
            _tracks[0].Altitude.Returns(1003);

            _tracks.Add(Substitute.For<ITrack>());
            _tracks[1].Xcoor.Returns(10001);
            _tracks[1].Ycoor.Returns(10001);
            _tracks[1].Altitude.Returns(1001);

            _tracks.Add(Substitute.For<ITrack>());
            _tracks[2].Xcoor.Returns(10002);
            _tracks[2].Ycoor.Returns(10002);
            _tracks[2].Altitude.Returns(1002);

            _tracks = _uut.SortTracks(_tracks);
            _seperationEvents = _uut.FindSeperationEvents(_tracks);

            Assert.That(_seperationEvents.Count,Is.EqualTo(3));
        }

        [Test]
        public void SortTracks_add3CloseTracksOutOfAirspace_0SeparationEvent()
        {


            _tracks.Add(Substitute.For<ITrack>());
            _tracks[0].Xcoor.Returns(1003);
            _tracks[0].Ycoor.Returns(1003);
            _tracks[0].Altitude.Returns(1003);

            _tracks.Add(Substitute.For<ITrack>());
            _tracks[1].Xcoor.Returns(1001);
            _tracks[1].Ycoor.Returns(1001);
            _tracks[1].Altitude.Returns(1001);

            _tracks.Add(Substitute.For<ITrack>());
            _tracks[2].Xcoor.Returns(1002);
            _tracks[2].Ycoor.Returns(1002);
            _tracks[2].Altitude.Returns(1002);

            _tracks = _uut.SortTracks(_tracks);
            _seperationEvents = _uut.FindSeperationEvents(_tracks);

            Assert.That(_seperationEvents.Count, Is.EqualTo(0));

        }

        [Test]
        public void SortTracks_add2CloseTracksInAirspaceSWBoundary_1SeparationEvent()
        {
            _tracks.Add(Substitute.For<ITrack>());
            _tracks[0].Xcoor.Returns(10000);
            _tracks[0].Ycoor.Returns(10000);
            _tracks[0].Altitude.Returns(501);

            _tracks.Add(Substitute.For<ITrack>());
            _tracks[1].Xcoor.Returns(10000);
            _tracks[1].Ycoor.Returns(10001);
            _tracks[1].Altitude.Returns(500);


            _tracks = _uut.SortTracks(_tracks);
            _seperationEvents = _uut.FindSeperationEvents(_tracks);

            Assert.That(_seperationEvents.Count, Is.EqualTo(1));

        }
    }
}
