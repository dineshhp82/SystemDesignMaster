namespace DistibutedLogger
{
    public class SizeBasedRollingStrategy : ILogRollingStrategy
    {
        private readonly string _baseFileName;
        private readonly long _maxFileSizeInBytes;
        private int _fileIndex = 0;

        public SizeBasedRollingStrategy(string baseFileName, long maxFileSizeInByts)
        {
            _baseFileName = baseFileName;
            _maxFileSizeInBytes = maxFileSizeInByts * 1024 * 1024;
        }

        public string GetLogFile()
        {
            string fileName;

            do
            {
                fileName = $"{_baseFileName}_{_fileIndex}.log";
                FileInfo fileInfo = new FileInfo(fileName);

                if (!fileInfo.Exists || fileInfo.Length < _maxFileSizeInBytes)
                {
                    return fileName;
                }
                _fileIndex++;
            }
            while (true);
        }
    }
}
