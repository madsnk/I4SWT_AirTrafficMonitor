﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace I4SWT_AirTrafficMonitor.Classes.Tracks
{
    public interface ITrack
    {
        void UpdateTrack(ITrack track);

        uint CalcCourse(int xComponent, int yComponent);

        string Tag { get; }

        int Xcoor { get; }

        int Ycoor { get; }

        int Altitude { get; }

        uint Velocity { get; }

        uint Course { get; }

        DateTime TimeStamp { get; }
    }
}
