using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using I4SWT_AirTrafficMonitor.Classes.Tracks;

namespace I4SWT_AirTrafficMonitor.UnitTesting.Fakes
{
    class FakeTrackfactory : ITrackFactory
    {
        private int _createTrackCalls = 0;
        public ITrack CreateTrack(string rawTrackData)
        {
            _createTrackCalls++;
            return new FakeTrack(rawTrackData);
        }

        public int ReceivedCreateTrack()
        {
            return _createTrackCalls;
        }
    }
}
