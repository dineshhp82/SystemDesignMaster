using System.Collections.Concurrent;

namespace RoundRobinSystem
{
    //Registry responsible for storing servers and exposing a consistent snapshot of healthy servers Ids for fast round robin selection
    public class ServerRegistry
    {
        // Main storage of server objects
        private readonly ConcurrentDictionary<Guid, Server> _servers = new();

        // Lock to protect snapshot rebuilds (Add/Remove/Health changes)
        private readonly object _snapshotLock = new();

        // Volatile snapshot of server IDs (only healthy ones). Readers read this array without locking.
        private volatile Guid[] _healthySnapshot = Array.Empty<Guid>();

        public void AddServer(Server server)
        {
            if (server == null)
                throw new ArgumentNullException(nameof(server));

            if (!_servers.TryAdd(server.Id, server))
            {
                throw new InvalidOperationException($"Server with Id {server.Id} already exists.");
            }

            // If server is healthy, include it in the snapshot
            if (server.IsHealthy)
            {
                //Rebuild snapshot to include new healthy server
                RebuildSnapshot();
            }
        }

        public bool Remove(Guid guid)
        {
            if (_servers.TryRemove(guid, out var removedServer))
            {
                // If removed server was healthy, rebuild snapshot
                if (removedServer.IsHealthy)
                {
                    //Rebuild snapshot to exclude removed server
                    RebuildSnapshot();
                }

                return true;
            }
            else
            {
                throw new KeyNotFoundException($"Server with Id {guid} not found.");
            }
        }

        //Lookup server by id; returns null if not found.
        public Server GetServer(Guid guid)
        {
            if (_servers.TryGetValue(guid, out var server))
            {
                return server;
            }
            else
            {
                throw new KeyNotFoundException($"Server with Id {guid} not found.");
            }
        }

        //Mark server health and rebuild snapshot if health status changed.
        public bool MarkHealth(Guid serverId, bool isHealthy)
        {
            if (_servers.TryGetValue(serverId, out var server))
            {
                bool previous = server.IsHealthy;
                server.IsHealthy = isHealthy;

                if (previous != isHealthy)
                {
                    RebuildSnapshot();
                }

                return true;
            }

            return false;
        }

        //Get snapshot(fast, lock-free). Returns array reference(immutable usage expected).
        public Guid[] GetSnapshot() => _healthySnapshot;

        //Rebuild the health snapshot (only healthy servers' ids). This is called under a lock to prevent concurrent rebuilds.
        private void RebuildSnapshot()
        {
            lock (_snapshotLock)
            {
                // Build a new array from current servers that are healthy
                var snapshot = _servers.Values
                    .Where(s => s.IsHealthy)
                    .Select(s => s.Id)
                    .ToArray();

                // Atomically replace the snapshot
                Interlocked.Exchange(ref _healthySnapshot, snapshot);
            }
        }

        //For observability: returns count of registered servers and snapshot length
        public (int totalRegistered, int healthyCount) GetCounts() => (_servers.Count, _healthySnapshot.Length);
    }
}
