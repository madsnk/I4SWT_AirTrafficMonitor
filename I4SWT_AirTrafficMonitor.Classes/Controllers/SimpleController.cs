using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using I4SWT_AirTrafficMonitor.Classes.Tracks;
using TransponderReceiver;

namespace I4SWT_AirTrafficMonitor.Classes.Controllers
{
    public class SimpleController
    {
        private ITrack _tempTrack;

        private List<ITrack> _tracks;// = new List<ITrack>();

        private ITransponderReceiver _receiver;

        private ITrackFactory _trackFactory;

        private IConsoleWrapper _console;

        public SimpleController(ITransponderReceiver receiver, IConsoleWrapper console, ITrackFactory trackFactory, List<ITrack> tracks)
        {
            _trackFactory = trackFactory;// = new StandardTrackFactory();

            _receiver = receiver;
            _receiver.TransponderDataReady += OnNewTrackData;

            _tracks = tracks;

            _console = console;
        }

        public void OnNewTrackData(object sender, RawTransponderDataEventArgs e)
        {
            _console.Clear();
            _console.Report("OnNewTrackData Called");

            List<String> rawData = e.TransponderData;

            // for each raw track in eventArgs
            foreach (string track in rawData)
            {
                //_console.Report(track + "\r\n");
                //create Track object (using TrackFactory)
                _tempTrack = _trackFactory.CreateTrack(track);

                string test = _tempTrack.Tag;

                if (!_tracks.Any())
                {
                    _tracks.Add(_tempTrack);
                    _console.Report(_tempTrack.ToString());
                }
                else
                {
                    //if object tag already in tracks list
                    int objInList = _tracks.FindIndex(x => x.Tag == _tempTrack.Tag);
                    if (objInList != -1)
                    {
                        //update track in list
                        //_tracks[objInList] = _tempTrack;
                        _tracks[objInList].UpdateTrack(_tempTrack);
                        _console.Report((_tracks[objInList].ToString() + "\r\n"));
                    }
                    else
                    {
                        //add to list
                        _tracks.Add(_tempTrack);
                        _console.Report(_tempTrack.ToString());
                    }
                }
            }
        }
    }
}
