using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using NSubstitute;
using I4SWT_AirTrafficMonitor.Classes.SeperationEvent;

namespace I4SWT_AirTrafficMonitor.UnitTesting
{
    [TestFixture]
    class SeperationEventUnitTest
    {
        private ISeperationEvent _uut;

        [SetUp]
        public void SetUp()
        {
            _uut = new SeperationEvent("XXX123","YYY321",1,2,new DateTime(2018,4,12,8,59,39,481));

        }
        [Test]
        public void csvFormat_newSeperationEvent_getExpectedString()
        {
            Assert.AreEqual("2018-04-12 08.59.39.481;XXX123;YYY321\n", _uut.csvFormat());
        }

        [Test]
        public void getHorizontalSep_setFromConstructor_resultIsCorrect()
        {
            Assert.That(_uut.HorizontalSeperation, Is.EqualTo(2));
        }

        [Test]
        public void getVerticalSep_setFromConstructor_resultIsCorrect()
        {
            Assert.That(_uut.VerticalSeperation, Is.EqualTo(1));
        }
    }
}
