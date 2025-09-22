using TrafficSignalManagement.Entites;

namespace TrafficSignalManagement.Observer
{
    public class CentralTrafficMonitor : IIntersectionObserver
    {
        public void OnOverrideTriggered(Signal signal, string reason)
          => Console.WriteLine($"[Monitor] Override at {signal.Location}: {reason}");

        public void OnSignalChanged(Signal signal, TrafficLight newLight)
          => Console.WriteLine($"[Monitor] {signal.Location} changed to {newLight.Color} for {newLight.DurationInSeconds}s");
    }
}
