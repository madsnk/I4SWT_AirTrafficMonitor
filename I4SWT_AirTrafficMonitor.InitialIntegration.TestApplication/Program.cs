using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransponderReceiver;
using I4SWT_AirTrafficMonitor.Classes.Controllers;

namespace I4SWT_AirTrafficMonitor.InitialIntegration.TestApplication
{
    class Program
    {


        static void Main(string[] args)
        {
            SimpleController controller;
            ITransponderReceiver receiver = TransponderReceiverFactory.CreateTransponderDataReceiver();

            controller = new SimpleController(receiver);

            Console.ReadLine();
        }
    }
}
