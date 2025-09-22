using TrafficSignalManagement.Entites;
using TrafficSignalManagement.Notifier;
using TrafficSignalManagement.Repository;
using TrafficSignalManagement.State;

namespace TrafficSignalManagement
{

    //Coordinator -> Orchestrates state transitions.

    public class IntersectionControl
    {
        private bool _stopRequested = false;
        public IState CurrentState { get; set; }

        private readonly SignalRepository _signalRepo;
        private readonly ObserverNotifier _notifier;
        private readonly TimingConfig _config;

        public IntersectionControl(SignalRepository repo, ObserverNotifier notifier, TimingConfig config)
        {
            _signalRepo = repo;
            _notifier = notifier;
            _config = config;
            CurrentState = new NSGreenState();
        }

        public Signal GetSignal(string loc) => _signalRepo.GetSignal(loc);

        public TimingConfig Config => _config;

        public ObserverNotifier Notifier => _notifier;

        public void Start()
        {
            _stopRequested = false;
            Console.WriteLine($"[{DateTime.UtcNow:HH:mm:ss.fff}] Intersection control started with state {CurrentState.Name}");

            while (!_stopRequested)
            {
                CurrentState.Enter(this);
                CurrentState.Exit(this);
                CurrentState = CurrentState.Next(this);
            }

            Console.WriteLine("Intersection control stopped.");
        }

        public void Stop() => _stopRequested = true;

        public void ForceNext()
        {
            Console.WriteLine($"[{DateTime.UtcNow:HH:mm:ss.fff}] Operator forced next state.");
            _notifier.NotifyOverride(GetSignal("NS"), "Forced transition");
            CurrentState = CurrentState.Next(this);
        }
    }
}
