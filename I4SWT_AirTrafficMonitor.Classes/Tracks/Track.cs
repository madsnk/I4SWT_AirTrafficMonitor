using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace I4SWT_AirTrafficMonitor.Classes.Tracks
{
    class Track : ITrack
    {
        private string _tag;

        private uint _altitude;

        private uint _velocity;

        private uint _course;
        // Constructor
        public Track(string tag, int xcoor, int ycoor, uint altitude, uint velocity, uint course)
        {
            Tag = tag;
            Xcoor = xcoor;
            Ycoor = ycoor;
            Altitude = altitude;
            Velocity = velocity;
            Course = course;
        }
        public void UpdateTrack(ITrack track)
        {
            throw new NotImplementedException();
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
    }

    public class TrackException : Exception
    {
        public TrackException(string s) : base(s)
        {
        }
    }
}
