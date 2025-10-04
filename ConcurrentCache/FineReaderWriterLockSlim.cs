namespace ConcurrentCache
{
    /*
     If you must update parts of the cache, not replace the whole thing:
     Pro: You can update individual entries.

    Con: Readers acquire a read lock (slower under heavy load).
     */
    internal class FineReaderWriterLockSlim
    {
        private readonly ReaderWriterLockSlim _lock = new();
        private readonly Dictionary<string, string> _cache = new();

        public string GetValue(string key)
        {
            _lock.EnterReadLock();
            try
            {
                _cache.TryGetValue(key, out var value);
                return value ?? "N/A";
            }
            finally
            {
                _lock.ExitReadLock();
            }
        }

        public void UpdateCache(string key, string value)
        {
            _lock.EnterWriteLock();
            try
            {
                _cache[key] = value;
            }
            finally
            {
                _lock.ExitWriteLock();
            }
        }
    }
}
