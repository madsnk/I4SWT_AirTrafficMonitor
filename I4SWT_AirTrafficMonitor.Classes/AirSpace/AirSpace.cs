using System;
using System.Collections.Generic;
using System.Diagnostics;
using I4SWT_AirTrafficMonitor.Classes.SeperationEvent;
using I4SWT_AirTrafficMonitor.Classes.Tracks;

namespace I4SWT_AirTrafficMonitor.Classes.AirSpace
{
    public class AirSpace : IAirSpace
    {
        public AirSpace(int SWBoundaryXCoor, int SWBoundaryYCoor, int NEBoundaryXCoor, int NEBoundaryYCoor,
            int LowerAltitudeBoundary, int UpperAltitudeBoundary, int VerticalSeperationTolerance, int HorizontalSeperationTolerance)
        {
            _swBoundaryXCoor = SWBoundaryXCoor;
            _swBoundaryYCoor = SWBoundaryYCoor;
            _neBoundaryXCoor = NEBoundaryXCoor;
            _neBoundaryYCoor = NEBoundaryYCoor;
            _lowerAltitudeBoundary = LowerAltitudeBoundary;
            _upperAltitudeBoundary = UpperAltitudeBoundary;
            _verticalSeperationTolerance = VerticalSeperationTolerance;
            _horizontalSeperationTolerance = HorizontalSeperationTolerance;
        }

        public void SortTracks(ref List<ITrack> tracks, ref List<ISeperationEvent> activeSeperationEvents)
        {
            tracks.RemoveAll(track => track.Xcoor < _swBoundaryXCoor || track.Xcoor > _neBoundaryXCoor);
            tracks.RemoveAll(track => track.Ycoor < _swBoundaryYCoor || track.Ycoor > _neBoundaryYCoor);
            tracks.RemoveAll(
                track => track.Altitude < _lowerAltitudeBoundary || track.Altitude > _upperAltitudeBoundary);

            // Clear Seperation events and determine current ones
            activeSeperationEvents.Clear();

            // for all tracks in list
            for (int i = 0; i < tracks.Count; i++)
            {
                for (int p = i + 1; p < tracks.Count; p++)
                {
                    var horizontalDist = CalcTrackDistance(tracks[i], tracks[p]);
                    var verticalDist = Math.Abs(tracks[i].Altitude - tracks[p].Altitude);

                    if (horizontalDist <= _horizontalSeperationTolerance &&
                        verticalDist <= _verticalSeperationTolerance)
                    {
                        var timeOfOccurrence = (tracks[i].TimeStamp > tracks[p].TimeStamp ? tracks[i].TimeStamp : tracks[p].TimeStamp);
                        activeSeperationEvents.Add(new SeperationEvent.SeperationEvent(tracks[i].Tag, tracks[p].Tag, (int)verticalDist, horizontalDist, timeOfOccurrence));
                    }
                }
            }
        }

        public int CalcTrackDistance(ITrack track, ITrack track2)
        {
            return (int)Math.Sqrt(Math.Pow((track.Xcoor - track2.Xcoor), 2) + Math.Pow((track.Ycoor - track2.Ycoor), 2));
        }

        private int _swBoundaryXCoor;
        private int _swBoundaryYCoor;
        private int _neBoundaryXCoor;
        private int _neBoundaryYCoor;
        private int _lowerAltitudeBoundary;
        private int _upperAltitudeBoundary;
        private int _verticalSeperationTolerance;
        private int _horizontalSeperationTolerance;
    }
}
