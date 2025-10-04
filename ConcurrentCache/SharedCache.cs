namespace ConcurrentCache
{

    /*
     Here, we maintain a reference to the entire cache dictionary, and when refresh happens,
     we replace it atomically — so readers never block.


    Benefits:
    - Readers are lock-free.
    - Refresh happens atomically (via Interlocked.Exchange).
    - Only one refresh runs at a time (due to Monitor.TryEnter).
    - No race condition, no blocking readers.
     */
    public class SharedCache
    {
        private readonly object _refreshLock = new(); // for refresh control
        private Dictionary<string, string> _cache = new(); // current data

        public string GetValue(string key)
        {
            // No locking — readers just use the reference
            _cache.TryGetValue(key, out var value);
            return value ?? "N/A";
        }


        public async Task RefreshAsync()
        {
            if (!Monitor.TryEnter(_refreshLock))
                return; // another refresh in progress — skip

            try
            {
                Console.WriteLine("Refreshing cache...");
                var newData = await LoadFreshDataAsync();

                // atomic swap (readers never blocked)
                Interlocked.Exchange(ref _cache, newData);

                Console.WriteLine("Cache refreshed.");
            }
            finally
            {
                Monitor.Exit(_refreshLock);
            }
        }


        private async Task<Dictionary<string, string>> LoadFreshDataAsync()
        {
            await Task.Delay(1000); // simulate fetch
            return new Dictionary<string, string>
            {
                ["A"] = DateTime.Now.ToString(),
                ["B"] = Guid.NewGuid().ToString()
            };
        }
    }
}
