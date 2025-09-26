namespace RoundRobinSystem
{
    public class RoundRobinLoadBalancer
    {

        // ever-increasing index; we use Interlocked.Increment to get the next slot
        private long _counter = -1;

        private readonly ServerRegistry _registry;

        public RoundRobinLoadBalancer(ServerRegistry registry)
        {
            _registry = registry ?? throw new ArgumentNullException(nameof(registry));
        }


        public Server? GetNext()
        {
            var snapshot = _registry.GetSnapshot();

            if (snapshot == null || snapshot.Length == 0) return null;

            long idx = Interlocked.Increment(ref _counter);
            
            // Increment counter atomically and use modulo of snapshot length
            // Using long to avoid fast wrap-around on high throughput
            int pos = (int)(idx % snapshot.Length);

            //read server id

            var serverId = snapshot[pos];

            var server = _registry.GetServer(serverId);

            if (server == null)
            {
                // Rare: server removed after snapshot taken. Fallback to rebuilding snapshot and retry once.
                // For simplicity, we return null here to keep code simpler — production code can retry.
                return null;
            }

            server.IncrementRequestCount();
            return server;
        }


        public (int totalRegistered, int healthyCount, (Guid id, string endpoint, long requests)[] perServer) GetStats()
        {
            var (total, healthy) = _registry.GetCounts();

            var all = _registry.GetSnapshot()
                .Select(id =>
                {
                    var s = _registry.GetServer(id)!;

                    return (s.Id, s.Endpoint, s.RequestCount);
                }).ToArray();

            return (total, healthy, all);
        }
    }
}
