namespace TrafficSignalManagement.Entites
{
    public class Signal
    {
        public Guid Id { get; } = Guid.NewGuid();

        public string Location { get; }

        private TrafficLight _currentLight;

        public Signal(string location) => Location = location;

        public TrafficLight CurrentLight => _currentLight;

        public void SetLight(LightColor lightColor, int duration)
        {
            _currentLight = new TrafficLight(lightColor, duration);
            Console.WriteLine($"[{DateTime.UtcNow:HH:mm:ss.fff}] Signal {Location} -> {_currentLight.Color} for {_currentLight.DurationInSeconds}s");
        }
    }
}