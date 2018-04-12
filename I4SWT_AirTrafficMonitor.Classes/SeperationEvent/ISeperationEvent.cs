using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace I4SWT_AirTrafficMonitor.Classes.SeperationEvent
{
    public interface ISeperationEvent
    {
        string FirstTrackTag { get; }

        string SecondTrackTag { get; }

        int VerticalSeperation { get; }

        int HorizontalSeperation { get; }

        DateTime TimeOfOccurrence { get; }
    }
}
