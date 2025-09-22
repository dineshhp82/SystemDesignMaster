using TrafficSignalManagement.Builder;
using TrafficSignalManagement.Entites;
using TrafficSignalManagement.Observer;

namespace TrafficSignalManagement
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Traffic Signal System");

            var controller = new IntersectionControlBuilder()
                .AddSignal(new Signal("NS"))
                .AddSignal(new Signal("EW"))
                .AddObserver(new CentralTrafficMonitor())
                .Build();  // Red is typically Green + Yellow of the other direction

            // Run in a separate thread to allow operator actions
            var thread = new Thread(controller.Start);
            thread.Start();

            // Simulate operator override after 8 seconds
            Thread.Sleep(8000);
            controller.ForceNext();

            // Stop after 20 seconds
            Thread.Sleep(20000);
            controller.Stop();
            thread.Join();


            Console.WriteLine("=== Simulation End ===");
        }
    }
}
