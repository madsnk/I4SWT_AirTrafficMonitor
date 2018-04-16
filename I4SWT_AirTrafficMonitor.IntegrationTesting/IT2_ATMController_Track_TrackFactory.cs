using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using I4SWT_AirTrafficMonitor.Classes;
using I4SWT_AirTrafficMonitor.Classes.AirSpace;
using I4SWT_AirTrafficMonitor.Classes.Controllers;
using I4SWT_AirTrafficMonitor.Classes.Log;
using I4SWT_AirTrafficMonitor.Classes.SeperationEvent;
using I4SWT_AirTrafficMonitor.Classes.Tracks;
using NSubstitute;
using NUnit.Framework;
using TransponderReceiver;

namespace I4SWT_AirTrafficMonitor.IntegrationTesting
{
    class IT2_ATMController_Track_TrackFactory
    {
        //Class fields
        // Included units
        ATMController _drivenUut_controller;
        private ITrackFactory _trackFactory;
        private List<ITrack> _tracks;
        private ILog _log;

        // Fakes
        private ITransponderReceiver _receiver;
        private IConsoleWrapper _console;
        private List<ISeperationEvent> _seperationEvents;
        private IAirSpace _airSpace;

        [SetUp]
        public void Setup()
        {
            _receiver = Substitute.For<ITransponderReceiver>();
            _console = Substitute.For<IConsoleWrapper>();
            _seperationEvents = new List<ISeperationEvent>();
            _airSpace = Substitute.For<IAirSpace>();

            _log = new Log("path");
            _trackFactory = new StandardTrackFactory();
            _tracks = new List<ITrack>();
            _drivenUut_controller = new ATMController(_receiver, _trackFactory, _console, _airSpace, _tracks, _seperationEvents, _log);
        }

        [Test]
        public void CreateTrack_createNewTrackFromRawTrackData_TrackIsCreatedCorrectly()
        {
            var testData = new List<string>
            {
                "XXX123;10000;10000;15000;20171122112233100"
            };

            _receiver.TransponderDataReady += Raise.EventWith(new RawTransponderDataEventArgs(testData));
            Assert.That(_drivenUut_controller.GetTracks()[0].Xcoor, Is.EqualTo(10000));
            Assert.That(_drivenUut_controller.GetTracks()[0].Tag, Is.EqualTo("XXX123"));
            Assert.That(_drivenUut_controller.GetTracks()[0].TimeStamp, Is.EqualTo(new DateTime(2017, 11, 22, 11, 22, 33, 100)));
        }

        [Test]
        public void UpdateTrack_updateTrackObjWithNewRawTrackData_VelocityUpdatedCorrectly()
        {
            var testData1 = new List<string>
            {
                "XXX123;10000;10000;15000;20171122112233100"
            };
            var testData2 = new List<string>
            {
                "XXX123;10000;10200;15000;20171122112234100" // Add one second and 200m in y coordinate
            };

            _receiver.TransponderDataReady += Raise.EventWith(new RawTransponderDataEventArgs(testData1));
            _receiver.TransponderDataReady += Raise.EventWith(new RawTransponderDataEventArgs(testData2));

            Assert.That(_drivenUut_controller.GetTracks()[0].Velocity, Is.EqualTo(200));
        }

        [Test]
        public void UpdateTrack_updateTrackObjWithNewRawTrackData_CourseUpdatedCorrectly()
        {
            var testData1 = new List<string>
            {
                "XXX123;10000;10000;15000;20171122112233100"
            };
            var testData2 = new List<string>
            {
                "XXX123;12000;12000;15000;20171122112213100" // Add 10 second and 2000m in y and x coordinate(45deg)
            };

            _receiver.TransponderDataReady += Raise.EventWith(new RawTransponderDataEventArgs(testData1));
            _receiver.TransponderDataReady += Raise.EventWith(new RawTransponderDataEventArgs(testData2));

            Assert.That(_drivenUut_controller.GetTracks()[0].Course, Is.EqualTo(45));
        }
    }
}
