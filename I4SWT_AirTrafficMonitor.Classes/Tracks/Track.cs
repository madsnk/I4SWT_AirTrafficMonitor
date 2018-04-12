using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace I4SWT_AirTrafficMonitor.Classes.Tracks
{
    public class Track : ITrack
    {
        private string _tag;

        private uint _altitude;

        private uint _velocity;

        private uint _course;

        private int _oldXcoor;

        private int _oldYcoor;

        private DateTime _oldTimeStamp;
        // Constructor
        public Track(string tag, int xcoor, int ycoor, uint altitude, DateTime timeStamp)
        {
            Tag = tag;
            Xcoor = xcoor;
            Ycoor = ycoor;
            Altitude = altitude;
            TimeStamp = timeStamp;
            Velocity = 0;
            Course = 0;
        }
        public void UpdateTrack(ITrack track)
        {
            if (track.Tag != _tag)
            {
                throw new TrackException($"Track tag {track} does not correspond to this instance track tag");
            }
            else
            {
                Altitude = track.Altitude;

                _oldXcoor = Xcoor;
                _oldYcoor = Ycoor;

                Xcoor = track.Xcoor;
                Ycoor = track.Ycoor;

                _oldTimeStamp = TimeStamp;

                var timeDiffSec = track.TimeStamp.Subtract(_oldTimeStamp).TotalSeconds;

                var distance = Math.Sqrt(Math.Pow((_oldXcoor - Xcoor), 2) + Math.Pow((_oldYcoor - Ycoor), 2));

                Velocity = (uint) (distance / timeDiffSec);

                // Course Calculation
                var xDirection = Xcoor - _oldXcoor;
                var yDirection = Ycoor - _oldYcoor;

                Course = CalcCourse(xDirection, yDirection);
            }
        }

        public uint CalcCourse(int xComponent, int yComponent)
        {
            var opposite = Math.Abs(xComponent);
            var adjacent = Math.Abs(yComponent);
            // if in 1. quadrant
            if (xComponent >= 0 && yComponent >= 0)
            {
                if (yComponent == 0)
                {
                    return 90;
                }
                var radians = Math.Atan(opposite / adjacent);

                return (uint)(radians * (180 / Math.PI));
            }
            // if in 2. quadrant
            else if (xComponent <= 0 && yComponent >= 0)
            {
                if (yComponent == 0)
                {
                    return 270;
                }
                var radians = Math.Atan(opposite / adjacent);

                return 360 - (uint)(radians * (180 / Math.PI));
            }
            // if in 3.
            else if (xComponent <= 0 && yComponent <= 0)
            {
                var radians = Math.Atan(opposite / adjacent);

                return 180 + (uint)(radians * (180 / Math.PI));
            }
            // if in 4.
            else if (xComponent >= 0 && yComponent <= 0)
            {
                var radians = Math.Atan(opposite / adjacent);

                return 180 - (uint)(radians * (180 / Math.PI));
            }
            else
            {
                return 0;
            }
        }

        public string Tag
        {
            get => _tag;

            private set
            {
                if (value.Length == 6)
                {
                    _tag = value;
                }
                else
                {
                    throw new TrackException($"String {value} is not a valid tag");
                }
            }
        }

        public int Xcoor { get; private set; }

        public int Ycoor { get; private set; }

        public uint Altitude { get; private set; }
        public uint Velocity { get; private set; }
        public uint Course { get; private set; }
        public DateTime TimeStamp { get; private set; }

        public override string ToString()
        {
            //return "Person: " + Name + " " + Age;
            return "Tag:          " + Tag + "\r\n" + $"Altitude:     " + Altitude + " m\r\n" + "Velocity:     " + Velocity + " m/s\r\n" + "Coordinates:  (" + Xcoor +", " + Ycoor + ")\r\n" + "Course:       " + Course + "deg";
        }
    }

    public class TrackException : Exception
    {
        public TrackException(string s) : base(s)
        {
        }
    }
}
