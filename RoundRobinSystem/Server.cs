namespace RoundRobinSystem
{
    /*
     Simple immutable metadata for a server endpoint.
    'RequestCount' is updated atomically.
     */
    public class Server
    {
        public Guid Id { get; }
        public string Endpoint { get; }

        // Health is made volatile to allow lock-free reads/writes
        public volatile bool IsHealthy;

        // Request count uses Interlocked increments
        private long _requestCount;

        public long RequestCount => Interlocked.Read(ref _requestCount);

        public Server(Guid id, string endpoint, bool isHealty = true)
        {
            Id = id;
            Endpoint = endpoint ?? throw new ArgumentNullException(nameof(endpoint));
            IsHealthy = isHealty;
        }

        // Called by the load balancer when this server is selected
        public void IncrementRequestCount()
        {
            Interlocked.Increment(ref _requestCount);
        }

        public override string ToString() => $"{Endpoint} (Id={Id}, Healthy={IsHealthy}, Requests={RequestCount})";
    }
}
