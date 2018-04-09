using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using I4SWT_AirTrafficMonitor.Classes;
using TransponderReceiver;
using I4SWT_AirTrafficMonitor.Classes.Controllers;
using I4SWT_AirTrafficMonitor.Classes.Tracks;

namespace I4SWT_AirTrafficMonitor.InitialIntegration.TestApplication
{
    class Program
    {


        static void Main(string[] args)
        {
            SimpleController controller;
            ITransponderReceiver receiver = TransponderReceiverFactory.CreateTransponderDataReceiver();
            IConsoleWrapper console = new ConsoleWrapper();
            ITrackFactory trackFactory = new FakeTrackFactory();
            List<ITrack> tracks = new List<ITrack>();

            controller = new SimpleController(receiver, console, trackFactory, tracks);

            Console.ReadLine();
        }
    }
}
