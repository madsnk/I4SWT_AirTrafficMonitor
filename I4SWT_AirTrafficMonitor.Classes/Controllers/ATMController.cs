using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using I4SWT_AirTrafficMonitor.Classes.AirSpace;
using I4SWT_AirTrafficMonitor.Classes.SeperationEvent;
using I4SWT_AirTrafficMonitor.Classes.Tracks;
using TransponderReceiver;

namespace I4SWT_AirTrafficMonitor.Classes.Controllers
{
    class ATMController
    {
        private ITrack _tempTrack;
        private List<ITrack> _tracks;
        private ITransponderReceiver _receiver;
        private ITrackFactory _trackFactory;
        private IConsoleWrapper _console;
        private IAirSpace _myAirSpace;
        private List<ISeperationEvent> _activeSeperationEvents;

        public ATMController(ITransponderReceiver receiver, ITrackFactory trackFactory, IConsoleWrapper console,
            IAirSpace airspace, List<ITrack> tracks, List<ISeperationEvent> seperationEvents)
        {
            _receiver = receiver;
            _trackFactory = trackFactory;
            _console = console;
            _myAirSpace = airspace;
            _tracks = tracks;
            _activeSeperationEvents = seperationEvents;

            _receiver.TransponderDataReady += OnNewTrackData;
        }

        void OnNewTrackData(object sender, RawTransponderDataEventArgs eventArgs)
        {
            _console.Clear();
            _console.Report("OnNewTrackData Called");

            List<String> rawTrackData = eventArgs.TransponderData;

            // for each raw track in eventArgs
            foreach (string track in rawTrackData)
            {
                _tempTrack = _trackFactory.CreateTrack(track);

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
