using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using I4SWT_AirTrafficMonitor.Classes.Tracks;
using TransponderReceiver;

namespace I4SWT_AirTrafficMonitor.Classes.Controllers
{
    class SimpleController
    {
        private List<ITrack> _tracks;

        private ITransponderReceiver _receiver;

        public SimpleController(ITransponderReceiver receiver)
        {
            _receiver = receiver;
            _receiver.TransponderDataReady += OnNewTrackData;
        }

        public void OnNewTrackData(object sender, RawTransponderDataEventArgs e)
        {
            // for each raw track in eventArgs
                // create Track object (using TrackFactory)
                    // if object tag is not already in tracks list
                        // add to list
                    // else
                        // update track in list
        }
    }
}
