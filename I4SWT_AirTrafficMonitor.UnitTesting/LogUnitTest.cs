using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using I4SWT_AirTrafficMonitor.Classes.Log;
using System.IO;

namespace I4SWT_AirTrafficMonitor.UnitTesting
{
    [TestFixture]
    class LogUnitTest
    {
        private ILog _uut;
        private string timeString;
        [SetUp]
        public void Setup()
        {
            timeString = DateTime.Now.ToString("yyyy-MM-dd HH.mm.ss");
            _uut = new Log("UnitTestLog");
            
        }

        [Test]
        public void Log_newLog_LogExixst()
        {
            FileAssert.Exists("UnitTestLog" + "_" + timeString + ".csv");
        }

        [Test]
        public void Log_newLog_HeaderIsCorrect()
        {
            var s = File.ReadAllLines("UnitTestLog" + "_" + timeString + ".csv");
            Assert.AreEqual("Time Of Occurrence;First Track Tag;Second Track Tag", s[0]);
        }

        [Test]
        public void Append_addData_dataInLog()
        {
            _uut.Append("2018-04-12 08.59.39.481;XXX123;YYY456\n");

            var s = File.ReadAllLines("UnitTestLog" + "_" + timeString + ".csv");
            Assert.AreEqual("2018-04-12 08.59.39.481;XXX123;YYY456", s[1]);
        }
        

    }
}
