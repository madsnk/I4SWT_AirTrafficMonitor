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
    }
}
