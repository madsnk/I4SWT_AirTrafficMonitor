using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using I4SWT_AirTrafficMonitor.Classes.Tracks;

namespace I4SWT_AirTrafficMonitor.Classes.Tracks
{
    public class FakeTrackFactory : ITrackFactory
    {
        public ITrack CreateTrack(string rawTrackData)
        {
            return new Track(rawTrackData, 0, 0, 0, new DateTime(0));
        }
    }
}
