using TrafficSignalManagement.Entites;
using TrafficSignalManagement.Observer;

namespace TrafficSignalManagement.Notifier
{
    public class ObserverNotifier
    {
        private readonly List<IIntersectionObserver> _observers = new();

        public void Subscribe(IIntersectionObserver observer) => _observers.Add(observer);

        public void NotifySignalChanged(Signal signal)
        {
            foreach (var o in _observers) 
                o.OnSignalChanged(signal, signal.CurrentLight);
        }

        public void NotifyOverride(Signal signal, string reason)
        {
            foreach (var o in _observers) 
                o.OnOverrideTriggered(signal, reason);
        }
    }

}
