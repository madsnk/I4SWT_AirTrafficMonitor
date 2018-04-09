using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using I4SWT_AirTrafficMonitor.Classes.Tracks;
using NUnit.Framework;

namespace I4SWT_AirTrafficMonitor.UnitTesting
{
    [TestFixture]
    class StandardTrackFactoryUnitTest
    {
        [Test]
        public void CreateTrack_ObjectifyTrackFromRawData_TrackObjectIsCorrect()
        {
            ITrackFactory testTrackFactory = new StandardTrackFactory();
            string rawTestTrackData = "XXX123;10000;12000;15000;20171212121220111";
            ITrack testTrack = testTrackFactory.CreateTrack(rawTestTrackData);

            DateTime testTime = new DateTime(2017, 12, 12, 12, 12, 20, 111);

            Assert.That(testTrack.Tag, Is.EqualTo("XXX123"));
            Assert.That(testTrack.Xcoor, Is.EqualTo(10000));
            Assert.That(testTrack.Ycoor, Is.EqualTo(12000));
            Assert.That(testTrack.Altitude, Is.EqualTo(15000));
            Assert.That(testTrack.TimeStamp, Is.EqualTo(testTime));
        }

        [Test]
        public void CreateTrack_ObjectifyTrackFromRawDataCoordinateWithLeadingZeros_TrackObjectIsCorrect()
        {
            ITrackFactory testTrackFactory = new StandardTrackFactory();
            string rawTestTrackData = "XXX123;10000;00500;15000;20171212121220111";
            ITrack testTrack = testTrackFactory.CreateTrack(rawTestTrackData);

            DateTime testTime = new DateTime(2017, 12, 12, 12, 12, 20, 111);

            Assert.That(testTrack.Tag, Is.EqualTo("XXX123"));
            Assert.That(testTrack.Xcoor, Is.EqualTo(10000));
            Assert.That(testTrack.Ycoor, Is.EqualTo(500));
            Assert.That(testTrack.Altitude, Is.EqualTo(15000));
            Assert.That(testTrack.TimeStamp, Is.EqualTo(testTime));
        }
    }
}
