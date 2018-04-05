using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace I4SWT_AirTrafficMonitor.Classes.Tracks
{
    public interface ITrackFactory
    {
        ITrack CreateTrack(string rawTrackData);
    }
}
