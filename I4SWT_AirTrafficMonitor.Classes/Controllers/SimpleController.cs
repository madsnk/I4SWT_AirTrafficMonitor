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
        private ITrack _tempTrack; 

        private List<ITrack> _tracks;

        private ITransponderReceiver _receiver;

        private ITrackFactory _trackFactory;

        public SimpleController(ITransponderReceiver receiver)
        {
            _trackFactory = new StandardTrackFactory();
            _receiver = receiver;
            _receiver.TransponderDataReady += OnNewTrackData;
        }

        public void OnNewTrackData(object sender, RawTransponderDataEventArgs e)
        {
            List<String> rawData = e.TransponderData;

            // for each raw track in eventArgs
            foreach (string track in rawData)
            {
                //create Track object (using TrackFactory)
                _tempTrack = _trackFactory.CreateTrack(track);

                string test = _tempTrack.Tag;

                //if object tag already in tracks list
                int objInList = _tracks.FindIndex(x => x.Tag == _tempTrack.Tag);
                if (objInList != -1)
                {
                    //update track in list
                    _tracks[objInList] = _tempTrack;
                }
                else
                {
                    //add to list
                    _tracks.Add(_tempTrack);
                }
            }
        }
    }
}
