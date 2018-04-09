using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using I4SWT_AirTrafficMonitor.Classes.Tracks;
using NSubstitute;

namespace I4SWT_AirTrafficMonitor.Classes.Tracks
{
    public class FakeTrackFactory : ITrackFactory
    {
        private ITrack tempTrack = Substitute.For<ITrack>();
        public ITrack CreateTrack(string stringID)
        {
            tempTrack.Tag.Returns(stringID);
            return tempTrack;
            //return new Track(rawTrackData, 0, 0, 0, new DateTime(0));
        }
    }
}
