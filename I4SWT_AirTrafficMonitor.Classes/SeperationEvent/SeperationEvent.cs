using System;

namespace I4SWT_AirTrafficMonitor.Classes.SeperationEvent
{
    public struct SeperationEvent : ISeperationEvent
    {
        public SeperationEvent(string tag, string tag2, int verticalSeperation, int horizontalSeperation, DateTime timeOfOccurrence)
        {
            FirstTrackTag = tag;
            SecondTrackTag = tag2;
            VerticalSeperation = verticalSeperation;
            HorizontalSeperation = horizontalSeperation;
            TimeOfOccurrence = timeOfOccurrence;
        }
        public string FirstTrackTag { get; }

        public string SecondTrackTag { get; }

        public int VerticalSeperation { get; }

        public int HorizontalSeperation { get; }

        public DateTime TimeOfOccurrence { get; }

        public override string ToString()
        {
            return TimeOfOccurrence.ToString("yyyy-MM-dd HH.mm.ss.fff")  + ";" + FirstTrackTag + ";" + SecondTrackTag + "\n";
        }
    }
}