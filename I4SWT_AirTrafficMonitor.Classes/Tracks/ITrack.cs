using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace I4SWT_AirTrafficMonitor.Classes.Tracks
{
    public interface ITrack
    {
        void UpdateTrack(ITrack track);

        string Tag { get; }

        int Xcoor { get; }

        int Ycoor { get; }

        uint Altitude { get; }

        uint Velocity { get; }

        uint Course { get; }
    }
}
