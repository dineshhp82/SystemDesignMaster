namespace ConcurrentCache
{
    //Reader first  double check and lock
    //classic concurrency optimization 
    //Often used with caching or lazy initialization.

    //
    //Readers to not block each other.
    //Only one thread to refresh/update when needed.
    //Avoid unnecessary locks (locks are expensive).


    /*
    if (cache is valid)
     return cached value
    else
    lock
        if (still invalid)
            refresh cache
    return cached value
    99% of threads skip the lock entirely.



    - Most readers skip the lock (super fast).
    - Only the first reader after expiry does the refresh.
    - Others continue to use old data until refresh completes.
    - No race conditions, no multiple refreshes.
     
     */
    public class ReaderFirst
    {
        private readonly object _lock = new();
        private Dictionary<string, string> _cache = new();

        private DateTime _lastRefresh = DateTime.MinValue;
        private readonly TimeSpan _refreshInterval = TimeSpan.FromSeconds(10);

        public string GetValue(string key)
        {
            // 1️⃣ Reader-first check (no lock)
            if (DateTime.UtcNow - _lastRefresh > _refreshInterval)
            {
                lock (_lock)
                {
                    // 2️⃣ Double-check (still expired?)
                    if (DateTime.UtcNow - _lastRefresh > _refreshInterval)
                    {
                        Console.WriteLine($"[{DateTime.Now:T}] Refreshing cache...");
                        _cache = RefreshCache();
                        _lastRefresh = DateTime.UtcNow;
                    }
                }
            }

            // 3️⃣ Serve data immediately (no lock)
            _cache.TryGetValue(key, out var value);
            return value ?? "N/A";
        }

        private Dictionary<string, string> RefreshCache()
        {
            // Simulate external call
            Thread.Sleep(500);
            return new Dictionary<string, string>
            {
                ["A"] = DateTime.Now.ToString(),
                ["B"] = Guid.NewGuid().ToString()
            };
        }

        private bool IsCacheValid()
        {
            return DateTime.UtcNow - _lastRefresh < _refreshInterval;
        }
    }
}
