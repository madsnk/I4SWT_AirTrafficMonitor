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
    }
}
