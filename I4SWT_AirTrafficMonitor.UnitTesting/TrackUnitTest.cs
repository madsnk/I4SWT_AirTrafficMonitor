using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using I4SWT_AirTrafficMonitor.Classes;
using I4SWT_AirTrafficMonitor.Classes.Tracks;

namespace I4SWT_AirTrafficMonitor.UnitTesting
{
    [TestFixture]
    class TrackUnitTest
    {
        private string testTag = "XXX123";
        private DateTime testTime = new DateTime(2017, 10, 10, 10, 10, 10, 0);
        private int testXPos = 100;
        private int testYPos = 100;
        private int testAlt = 3000;
        private ITrack _uut;

        [SetUp]
        public void Setup()
        {
            _uut = new Track(testTag, testXPos, testYPos, testAlt, testTime);
        }

        // Test CalcCourse
        [Test]
        public void CalcCourse_FirstQuadrant45deg_CourseIs45()
        {
            Assert.That(_uut.CalcCourse(1000, 1000), Is.EqualTo(45));
        }

        [Test]
        public void CalcCourse_SecondQuadrant45deg_CourseIs315()
        {
            Assert.That(_uut.CalcCourse(-500, 500), Is.EqualTo(315));
        }

        [Test]
        public void CalcCourse_ThirdQuadrant45deg_CourseIs225()
        {
            Assert.That(_uut.CalcCourse(-7000, -7000), Is.EqualTo(225));
        }

        [Test]
        public void CalcCourse_FourthQuadrant45deg_CourseIs135()
        {
            Assert.That(_uut.CalcCourse(10000, -10000), Is.EqualTo(135));
        }

        [Test]
        public void CalcCourse_DirectlyNorth_CourseIs0()
        {
            Assert.That(_uut.CalcCourse(0, 120), Is.EqualTo(0));
        }

        [Test]
        public void CalcCourse_DirectlySoucth_CourseIs180()
        {
            Assert.That(_uut.CalcCourse(0, -1500), Is.EqualTo(180));
        }

        [Test]
        public void CalcCourse_DirectlyEast_CourseIs90()
        {
            Assert.That(_uut.CalcCourse(2000, 0), Is.EqualTo(90));
        }

        [Test]
        public void CalcCourse_DirectlyWest_CourseIs270()
        {
            Assert.That(_uut.CalcCourse(-2400, 0), Is.EqualTo(270));
        }

        [Test]
        public void UpdateTrack_TrackDoesNotReferToSameTag_ThrowsTrackException()
        {
            ITrack testTrack = new Track("ZZZ123", 0, 0, 0, new DateTime(2017, 10, 10, 10, 10, 10, 0));
            Assert.That(() => _uut.UpdateTrack(testTrack), Throws.TypeOf<TrackException>());
        }

        [Test]
        public void Constructor_InvalidTrackId_ThrowsTrackException()
        {
            ITrack testTrack;
            Assert.That(
                () => testTrack = new Track("XXX1234", 0, 0, 0, new DateTime(2017, 10, 10, 10, 10, 10, 0)), Throws.TypeOf<TrackException>());
        }

        [TestCase(100, 0, 1, 100)]
        [TestCase(1200, 0, 10, 120)]
        [TestCase(0, 2800, 20, 140)]
        [TestCase(300, 400, 10, 50)]
        [TestCase(-100, 0, 1, 100)]
        [TestCase(-300, -400, 10, 50)]
        public void UpdateTrack_MovingDifferentDirections_VelocityIsCorrect(int xDifference, int yDifference, int timeDiffSec, int expectedVel)
        {
            var t2 = testTime.AddSeconds(timeDiffSec);
            ITrack testTrack = new Track(testTag, testXPos+xDifference, testYPos+yDifference, testAlt, t2);

            _uut.UpdateTrack(testTrack);
            Assert.That(_uut.Velocity, Is.EqualTo(expectedVel));
        }

        [TestCase(1000)]
        [TestCase(-1000)]
        [TestCase(0)]
        public void UpdateTrack_NewAltitude_AltitudeIsCorrect(int altitudeDiff)
        {
            var newAlt = testAlt + altitudeDiff;
            ITrack testTrack = new Track(testTag, testXPos+1000, testYPos+1000, newAlt, testTime.AddSeconds(10));

            _uut.UpdateTrack(testTrack);
            Assert.That(_uut.Altitude, Is.EqualTo(testAlt+altitudeDiff));
        }

        [TestCase(100, 100, 45)]
        [TestCase(100, -100, 135)]
        [TestCase(-100, 100, 315)]
        [TestCase(-100, -100, 225)]
        [TestCase(0, 100, 0)]
        [TestCase(100, 0, 90)]
        [TestCase(0, -1000, 180)]
        [TestCase(-1200, 0, 270)]
        public void UpdateTrack_MoveDifferentDirections_CourseIsCorrect(int xDifference, int yDifference, int expectedCourse)
        {
            ITrack testTrack = new Track(testTag, testXPos + xDifference, testYPos + yDifference, testAlt, testTime.AddSeconds(10));

            _uut.UpdateTrack(testTrack);
            Assert.That(_uut.Course, Is.EqualTo(expectedCourse));
        }

        [Test]
        public void UpdateTrack_SeveralUpdates_VelocityIsCorrect()
        {
            int alt = 18000;
            var xStart = 80000;
            var yStart = 80000;

            ITrack _uutTrack = new Track(testTag, xStart, yStart, alt, testTime);

            ITrack updateTestTrack1 = new Track(testTag, xStart - 270, yStart, alt, testTime.AddSeconds(1));
            ITrack updateTestTrack2 = new Track(testTag, xStart - 2 * 270, yStart, alt, testTime.AddSeconds(2));
            ITrack updateTestTrack3 = new Track(testTag, xStart - 3 * 270, yStart, alt, testTime.AddSeconds(3));

            _uutTrack.UpdateTrack(updateTestTrack1);
            _uutTrack.UpdateTrack(updateTestTrack2);
            _uutTrack.UpdateTrack(updateTestTrack3);

            Assert.That(_uutTrack.Velocity, Is.EqualTo(270));
        }
    }
}
