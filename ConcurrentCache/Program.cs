namespace ConcurrentCache
{
    internal class Program
    {
        /*
         * Can we use Concurrent dictionary then is it solve the problem of cache - No 
           Concurrent Dictionary alone not solve the problems
            - Concurrent Dictionary  on  perofrm the atomic get and set value of key not replace whole cache dictionary
            - Race condition still exist when multiple thread try to update the cache at same time
            - Consistency problem may be some reader read stale data and some read new data
            - Atomic replace of reference is required due to whole repalce of cache dictionary
         */

        static void Main(string[] args)
        {
            Console.WriteLine("Concurrent Cache");

            //ReaderFirst();

            //ReaderFirstAsync();

            var cache = new SharedCache();

            // Background refresh every 10 seconds
            _ = Task.Run(async () =>
            {
                while (true)
                {
                    await cache.RefreshAsync();
                    await Task.Delay(TimeSpan.FromSeconds(10));
                }
            });

            Parallel.For(0, 100, i =>
            {
                Console.WriteLine(cache.GetValue("A"));
            });

            Console.WriteLine("Finished");

            Console.ReadLine();
        }

        private static void ReaderFirstAsync()
        {
            ReaderFirstAsync readerFirst = new ReaderFirstAsync();
            Parallel.For(0, 200, i =>
            {
                Console.WriteLine(readerFirst.GetValueAsync("A"));
                Console.WriteLine(readerFirst.GetValueAsync("B"));
                Thread.Sleep(550);
            });
        }

        private static void ReaderFirst()
        {
            ReaderFirst readerFirst = new ReaderFirst();
            Parallel.For(0, 100, i =>
            {
                Console.WriteLine(readerFirst.GetValue("A"));
                Console.WriteLine(readerFirst.GetValue("B"));
                Thread.Sleep(550);
            });
        }
    }
}
