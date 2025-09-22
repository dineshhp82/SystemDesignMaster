using TrafficSignalManagement.Entites;

namespace TrafficSignalManagement.Observer
{
    public interface IIntersectionObserver
    {
        void OnSignalChanged(Signal signal, TrafficLight newLight);

        void OnOverrideTriggered(Signal signal, string reason);
    }
}
