using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using I4SWT_AirTrafficMonitor.Classes.Tracks;

namespace I4SWT_AirTrafficMonitor.Classes.Tracks
{
    public class StandardTrackFactory : ITrackFactory
    {
        public ITrack CreateTrack(string rawTrackData)
        {
            string[] rawdataSplit = rawTrackData.Split(';');

            string tag = rawdataSplit[0];
            int xcoor = Convert.ToInt32(rawdataSplit[1]);
            int ycoor = Convert.ToInt32(rawdataSplit[2]);
            uint altiude = Convert.ToUInt32(rawdataSplit[3]);
            DateTime timeStamp = DateTime.ParseExact(rawdataSplit[4], "yyyyMMddHHmmssfff", CultureInfo.InvariantCulture);

            return new Track(tag, xcoor, ycoor, altiude, timeStamp);
        }
    }
}
