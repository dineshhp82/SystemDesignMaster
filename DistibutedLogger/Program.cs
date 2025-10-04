namespace DistibutedLogger
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Parallel.For(0, 100, i =>
            {
                Logger.Instance.LogInfo($"Message {i}");
            });

            Console.WriteLine("Logs written safely!");
        }
    }
}
