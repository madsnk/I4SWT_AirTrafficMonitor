using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Castle.Core.Internal;
using I4SWT_AirTrafficMonitor.Classes.AirSpace;
using I4SWT_AirTrafficMonitor.Classes.SeperationEvent;
using I4SWT_AirTrafficMonitor.Classes.Tracks;
using I4SWT_AirTrafficMonitor.Classes.Log;
using TransponderReceiver;

namespace I4SWT_AirTrafficMonitor.Classes.Controllers
{
    public class ATMController
    {
        private ITrack _tempTrack;
        private List<ITrack> _tracks;
        private ITransponderReceiver _receiver;
        private ITrackFactory _trackFactory;
        private IConsoleWrapper _console;
        private IAirSpace _myAirSpace;
        private List<ISeperationEvent> _activeSeperationEvents;
        private ILog _log;

        public ATMController(ITransponderReceiver receiver, ITrackFactory trackFactory, IConsoleWrapper console,
            IAirSpace airspace, List<ITrack> tracks, List<ISeperationEvent> seperationEvents,ILog log)
        {
            _receiver = receiver;
            _trackFactory = trackFactory;
            _console = console;
            _myAirSpace = airspace;
            _tracks = tracks;
            _activeSeperationEvents = seperationEvents;
            _log = log;

            _receiver.TransponderDataReady += OnNewTrackData;
        }

        void OnNewTrackData(object sender, RawTransponderDataEventArgs eventArgs)
        {
            _console.Clear();
            DrawPlane();

            List<String> rawTrackData = eventArgs.TransponderData;

            // for each raw track in eventArgs
            foreach (string track in rawTrackData)
            {
                //_console.Report(track);
                _tempTrack = _trackFactory.CreateTrack(track);

                if (!_tracks.Any())
                {
                    _tracks.Add(_tempTrack);
                    //_console.Report(_tempTrack.ToString());
                }
                else
                {
                    //if object tag already in tracks list
                    int objInList = _tracks.FindIndex(x => x.Tag == _tempTrack.Tag);
                    if (objInList != -1)
                    {
                        _tracks[objInList].UpdateTrack(_tempTrack);
                    }
                    else
                    {
                        //add to list
                        _tracks.Add(_tempTrack);
                    }
                }
            }
            _myAirSpace.SortTracks(ref _tracks, ref _activeSeperationEvents);

            Log(_activeSeperationEvents);

            PrintRawData(rawTrackData);
            Draw(_tracks, _activeSeperationEvents);

        }

        void Log(List<ISeperationEvent> seperations)
        {
            foreach (var seperation in seperations)
            {
                //_console.Report("SEPARATION EVENT");
                _log.Append(seperation.csvFormat());
            }
        }

        void Draw(List<ITrack> tracks, List<ISeperationEvent> seperations)
        {
            _console.Report("\r\n********************************************************************");
            _console.Report("***************** All Tracks in monitored Airspace *****************");
            _console.Report("********************************************************************");
            foreach (var track in tracks)
            {
                _console.Report(track.ToString() + "\r\n");
            }

            _console.Report("\r\n********************************************************************");
            _console.Report("******************** Current Seperation Events *********************");
            _console.Report("********************************************************************");
            foreach (var seperation in seperations)
            {
                _console.Report(seperation.ToString());
            }
        }

        void PrintRawData(List<string> rawData)
        {
            _console.Report("********************************************************************");
            _console.Report("    All raw trackdata beeing received:\r\n");
            foreach (var track in rawData)
            {
                _console.Report(track);
            }
        }

        void DrawPlane()
        {
            var plane = new[]
            {
                @"                                    |                              ",
                @"Air Traffic Monitor                 |                              ",
                @"Ultimate Edition                    |                              ",
                @"				  .-'-.                            ",
                @"                                 ' ___ '                           ",
                @"                       ---------'  .-.  '---------                 ",
                @"       _________________________'  '-'  '_________________________ ",
                @"        ''''''-|---|--/    \==][^',_m_,'^][==/    \--|---|-''''''  ",
                @"                      \    /  ||/   H   \||  \    /                ",
                @"                       '--'   OO   O|O   OO   '--'                 ",
            };

            foreach (var line in plane)
            {
                _console.Report(line);
            }
        }
    }
}
