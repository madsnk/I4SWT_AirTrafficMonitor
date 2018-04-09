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
        private ITrack _uut;

        [SetUp]
        public void Setup()
        {
            _uut = new Track("XXX123", 1, 1, 100, DateTime.Now);
        }

        // Test CalcCourse
        [Test]
        public void CalcCourse_FirstQuadrant45deg_AngleIs45()
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
    }
}
