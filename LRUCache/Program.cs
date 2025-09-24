namespace LRUCache
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("LRU Cache");

            var cache  = new LRUCache<int, string>(3);

            cache.Set(1, "A");
            cache.Set(2, "B");
            cache.Set(3, "C");

            Console.WriteLine(cache.Get(2)); // B, moves key 2 to head
            cache.Set(4, "D"); // Evicts key 1 (LRU)

            Console.WriteLine(cache.Get(1)); // null
            Console.WriteLine(cache.Get(2)); // B
            Console.WriteLine(cache.Get(3)); // C
            Console.WriteLine(cache.Get(4)); // D
        }
    }
}
