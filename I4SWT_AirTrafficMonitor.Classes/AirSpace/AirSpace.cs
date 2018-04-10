using System;
using System.Collections.Generic;
using I4SWT_AirTrafficMonitor.Classes.SeperationEvent;
using I4SWT_AirTrafficMonitor.Classes.Tracks;

namespace I4SWT_AirTrafficMonitor.Classes.AirSpace
{
    public class AirSpace : IAirSpace
    {
        public AirSpace(int SWBoundaryXCoor, int SWBoundaryYCoor, int NEBoundaryXCoor, int NEBoundaryYCoor,
            int LowerAltitudeBoundary, int UpperAltitudeBoundary)
        {
            _swBoundaryXCoor = SWBoundaryXCoor;
            _swBoundaryYCoor = SWBoundaryYCoor;
            _neBoundaryXCoor = NEBoundaryXCoor;
            _neBoundaryYCoor = NEBoundaryYCoor;
            _lowerAltitudeBoundary = LowerAltitudeBoundary;
            _upperAltitudeBoundary = UpperAltitudeBoundary;
        }

        public void SortTracks(ref List<ITrack> tracks, ref List<ISeperationEvent> activeSeperationEvents)
        {
            throw new NotImplementedException();
        }

        private int _swBoundaryXCoor;
        private int _swBoundaryYCoor;
        private int _neBoundaryXCoor;
        private int _neBoundaryYCoor;
        private int _lowerAltitudeBoundary;
        private int _upperAltitudeBoundary;
    }
}
