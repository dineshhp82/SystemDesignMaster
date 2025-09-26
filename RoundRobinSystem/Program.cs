namespace RoundRobinSystem
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Round Robin Server");

            var registry = new ServerRegistry();
            var lb = new RoundRobinLoadBalancer(registry);

            var s1 = new Server(Guid.NewGuid(), "http://10.0.0.1");
            var s2 = new Server(Guid.NewGuid(), "http://10.0.0.2");
            var s3 = new Server(Guid.NewGuid(), "http://10.0.0.3");

            registry.AddServer(s1);
            registry.AddServer(s2);
            registry.AddServer(s3);

            for (int i = 0; i < 10; i++)
            {
                var selected = lb.GetNext();
                Console.WriteLine($"Selected: {selected}");
            }

            // Remove a server and continue
            registry.Remove(s2.Id);
            var selected2 = lb.GetNext();
            Console.WriteLine($"Selected after removal: {selected2}");

            Console.ReadLine();
        }
    }
}
