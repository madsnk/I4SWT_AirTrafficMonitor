using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using I4SWT_AirTrafficMonitor.Classes.Tracks;

namespace I4SWT_AirTrafficMonitor.UnitTesting.Fakes
{
    class FakeTrack : ITrack
    {
        public FakeTrack(string tag)
        {
            Tag = tag;
            Xcoor = 0;
            Ycoor = 0;
            Altitude = 0;
            TimeStamp =  new DateTime(2000, 10, 10, 5, 50, 20);
            Velocity = 0;
            Course = 0;
        }

        public void UpdateTrack(ITrack track)
        {
        }

        public uint CalcCourse(int xComponent, int yComponent)
        {
            return 0;
        }

        public string Tag { get; }
        public int Xcoor { get; }
        public int Ycoor { get; }
        public int Altitude { get; }
        public uint Velocity { get; }
        public uint Course { get; }
        public DateTime TimeStamp { get; }
    }
}
