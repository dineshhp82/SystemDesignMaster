namespace DistibutedLogger
{
    public class TimeBasedRollingStrategy : ILogRollingStrategy
    {
        private readonly string _baseFileName;
        private readonly string _period; // "hour" or "day"

        public TimeBasedRollingStrategy(string baseFileName, string period = "day")
        {
            _baseFileName = baseFileName;
            _period = period.ToLower();
        }


        public string GetLogFile()
        {
            string suffix = _period switch
            {
                "hour" => DateTime.UtcNow.ToString("yyyyMMdd_HH"),
                _ => DateTime.UtcNow.ToString("yyyyMMdd") // default: day
            };

            return $"{_baseFileName}_{suffix}.log";
        }
    }
}
