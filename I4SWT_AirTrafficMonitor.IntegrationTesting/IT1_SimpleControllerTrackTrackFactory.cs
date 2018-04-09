using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using I4SWT_AirTrafficMonitor.Classes;
using I4SWT_AirTrafficMonitor.Classes.Controllers;
using I4SWT_AirTrafficMonitor.Classes.Tracks;
using NSubstitute;
using NUnit.Framework;
using NUnit.Framework.Internal;
using TransponderReceiver;

namespace I4SWT_AirTrafficMonitor.IntegrationTesting
{
    [TestFixture]
    class IT1_SimpleControllerTrackTrackFactory
    {
        private ITrackFactory _testTrackFactory;
        private List<ITrack> _testTrackList;
        private IConsoleWrapper _consoleWrapper;
        private ITransponderReceiver _testReceiver;
        private SimpleController _uut;

        [SetUp]
        public void Setup()
        {
            _testTrackFactory = new StandardTrackFactory();
            _testTrackList = new List<ITrack>();
            _consoleWrapper = new ConsoleWrapper();
            _testReceiver = Substitute.For<ITransponderReceiver>();
            _uut = new SimpleController(_testReceiver, _consoleWrapper, _testTrackFactory, _testTrackList);
        }

        [Test]
        public void DummyTest()
        {
            Assert.That(true, Is.EqualTo(true));
        }

        [Test]
        public void OnNewTrackData_OneNewTrackIntoEmptyTrackList_TrackListHasNewTrack()
        {
            var testData = new List<string>
            {
                "XXX123;10000;10000;15000;201711221133100"
            };

            _testReceiver.TransponderDataReady += Raise.EventWith(new RawTransponderDataEventArgs(testData));

            ITrack _expectedTrack = new Track("XXX123", 10000, 10000, 15000, new DateTime(2017, 11, 22, 11, 33, 100));

            Assert.That(_uut.GetTracks()[0].ToString(), Is.EqualTo(_expectedTrack.ToString())); // Should override Equals() instead probably
        }
    }
}
