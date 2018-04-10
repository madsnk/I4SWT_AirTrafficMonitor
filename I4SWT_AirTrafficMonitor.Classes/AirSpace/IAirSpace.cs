using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using I4SWT_AirTrafficMonitor.Classes.Tracks;
using I4SWT_AirTrafficMonitor.Classes.SeperationEvent;

namespace I4SWT_AirTrafficMonitor.Classes.AirSpace
{
    public interface IAirSpace
    {
        void SortTracks(ref List<ITrack> tracks, ref List<ISeperationEvent> activeSeperationEvents);
    }
}
