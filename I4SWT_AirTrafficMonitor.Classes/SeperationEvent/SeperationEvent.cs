using System;

namespace I4SWT_AirTrafficMonitor.Classes.SeperationEvent
{
    public struct SeperationEvent : ISeperationEvent
    {
        public SeperationEvent(string tag, string tag2, int verticalSeperation, int horizontalSeperation)
        {
            FirstTrackTag = tag;
            SecondTrackTag = tag2;
            VerticalSeperation = verticalSeperation;
            HorizontalSeperation = horizontalSeperation;
        }
        public string FirstTrackTag { get; }

        public string SecondTrackTag { get; }

        public int VerticalSeperation { get; }

        public int HorizontalSeperation { get; }

        public override string ToString()
        {
            throw new NotImplementedException();
        }
    }
}