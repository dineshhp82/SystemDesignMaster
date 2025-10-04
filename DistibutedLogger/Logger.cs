namespace DistibutedLogger
{
    public sealed class Logger
    {
        private static readonly Lazy<Logger> _instance = new(() => new Logger());
        private readonly object _lock = new();
        private readonly ILogRollingStrategy _rollingStrategy;

        private Logger()
        {
            //_logFilePath = $"log_{DateTime.UtcNow:yyyyMMdd}.txt"; // Default log file path
            //_rollingStrategy= new TimeBasedRollingStrategy("logs/app", "hour");
            _rollingStrategy = new SizeBasedRollingStrategy("logs/app", 1);
        }

        public static Logger Instance => _instance.Value;

        public void LogInfo(string message) => WriteLog("INFO", message);

        public void LogWarn(string message) => WriteLog("WARN", message);

        public void LogError(string message) => WriteLog("ERROR", message);

        private void WriteLog(string level, string message)
        {
            string logFile = _rollingStrategy.GetLogFile();
            var logEntry = $"{DateTime.UtcNow:yyyy-MM-dd HH:mm:ss.fff} [{level}] [Thread:{Thread.CurrentThread.ManagedThreadId}] {message}";
            lock (_lock)
            {
                Directory.CreateDirectory(Path.GetDirectoryName(logFile)!);
                System.IO.File.AppendAllText(logFile, logEntry + Environment.NewLine);
            }
        }
    }
}
