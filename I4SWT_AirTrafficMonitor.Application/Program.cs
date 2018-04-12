using System;
using System.Collections.Generic;
using I4SWT_AirTrafficMonitor.Classes;
using I4SWT_AirTrafficMonitor.Classes.AirSpace;
using I4SWT_AirTrafficMonitor.Classes.Controllers;
using I4SWT_AirTrafficMonitor.Classes.Log;
using I4SWT_AirTrafficMonitor.Classes.SeperationEvent;
using I4SWT_AirTrafficMonitor.Classes.Tracks;
using TransponderReceiver;

namespace I4SWT_AirTrafficMonitor.Application
{
    class Program
    {
        static void Main(string[] args)
        {
            ATMController controller;
            ITransponderReceiver receiver = TransponderReceiverFactory.CreateTransponderDataReceiver();
            IConsoleWrapper console = new ConsoleWrapper();
            ITrackFactory trackFactory = new StandardTrackFactory();
            List<ITrack> tracks = new List<ITrack>();
            List<ISeperationEvent> seperationEvents = new List<ISeperationEvent>(); 
            IAirSpace airspace = new AirSpace(10000, 10000, 90000, 90000, 500, 20000, 300, 5000);
            ILog log = new Log("testlog");

            controller = new ATMController(receiver, trackFactory, console, airspace, tracks, seperationEvents,log);

            Console.ReadLine();
        }
    }
}
