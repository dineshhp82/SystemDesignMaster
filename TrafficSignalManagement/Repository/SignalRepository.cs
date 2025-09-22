using TrafficSignalManagement.Entites;

namespace TrafficSignalManagement.Repository
{
    public class SignalRepository
    {
        private readonly List<Signal> _signals = new();

        public void AddSignal(Signal signal) => _signals.Add(signal);
        public Signal GetSignal(string location) =>
            _signals.First(s => s.Location.Equals(location, StringComparison.OrdinalIgnoreCase));
    }

}
