using Devices.Common.Helpers;
using System;
using System.Threading.Tasks;
using USBPortEventHandler.Devices.Core.SerialPort;

namespace USBPortEventHandler
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Starting port monitor...");

            SerialPortMonitor serialPortMonitor = new SerialPortMonitor();
            serialPortMonitor.ComportEventOccured += OnComPortEventOccured;
            serialPortMonitor.StartMonitoring();

            Console.WriteLine("... Port monitor started.\n");

            ConsoleKeyInfo keypressed = new ConsoleKeyInfo();

            while (keypressed.Key != ConsoleKey.Q)
            {
                keypressed = GetKeyPressed();
            }

            Console.WriteLine("Stopping port monitor...");
            serialPortMonitor.StopMonitoring();
            Console.WriteLine("... Port Monitor stopped.");
        }

        private static Task OnComPortEventOccured(PortEventType comPortEvent, string portNumber)
        {
            switch (comPortEvent)
            {
                case PortEventType.Insertion:
                {
                    Console.WriteLine($"PORT EVENT: {comPortEvent} - on PORT={portNumber} <=====");
                    break;
                }
                case PortEventType.Removal:
                {
                    Console.WriteLine($"PORT EVENT: {comPortEvent}   - on PORT={portNumber} =====>");
                    break;
                }
            }
            return Task.CompletedTask;
        }

        static private ConsoleKeyInfo GetKeyPressed()
        {
            return Console.ReadKey(true);
        }
    }

}
