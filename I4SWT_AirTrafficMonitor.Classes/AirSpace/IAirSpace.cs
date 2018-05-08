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
        List<ITrack> SortTracks(List<ITrack> tracks);

        List<ISeperationEvent> FindSeperationEvents(List<ITrack> tracks);
    }
}
