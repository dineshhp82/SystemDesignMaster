namespace ConcurrentCache
{
    public class ReaderFirstAsync
    {
        private readonly object _lock = new();
        private Dictionary<string, string> _cache = new();
        private DateTime _lastRefresh = DateTime.MinValue;
        private readonly TimeSpan _refreshInterval = TimeSpan.FromSeconds(10);

        public async ValueTask<string> GetValueAsync(string key)
        {
            if (DateTime.UtcNow - _lastRefresh > _refreshInterval)
            {
                lock (_lock)
                {
                    if (DateTime.UtcNow - _lastRefresh > _refreshInterval)
                    {
                        Console.WriteLine($"[{DateTime.Now:T}] Refreshing cache...");
                        _cache = LoadFreshDataAsync().GetAwaiter().GetResult();
                        _lastRefresh = DateTime.UtcNow;
                    }
                }
            }

            _cache.TryGetValue(key, out var value);
            return value ?? "N/A";
        }

        private async Task<Dictionary<string, string>> LoadFreshDataAsync()
        {
            await Task.Delay(500); // simulate API/DB
            return new Dictionary<string, string>
            {
                ["A"] = DateTime.Now.ToString(),
                ["B"] = Guid.NewGuid().ToString()
            };
        }
    }
}
