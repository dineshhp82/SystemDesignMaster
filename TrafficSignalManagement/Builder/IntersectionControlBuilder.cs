using TrafficSignalManagement.Constants;
using TrafficSignalManagement.Entites;
using TrafficSignalManagement.Notifier;
using TrafficSignalManagement.Observer;
using TrafficSignalManagement.Repository;

namespace TrafficSignalManagement.Builder
{
    public class IntersectionControlBuilder
    {
        SignalRepository signalRepository = new();
        ObserverNotifier observerNotifier = new(); 
        

        private IntersectionControl Control => new(signalRepository, observerNotifier, new TimingConfig(
            FixedDurations.DefaultGreen,
            FixedDurations.DefaultYellow,
            FixedDurations.DefaultRed));


        public IntersectionControlBuilder AddSignal(Signal signal)
        {
            signalRepository.AddSignal(signal);
            return this;
        }

        public IntersectionControlBuilder AddObserver(IIntersectionObserver observer)
        {
            observerNotifier.Subscribe(observer);
            return this;
        }

        public IntersectionControl Build() => Control;
    }
}
