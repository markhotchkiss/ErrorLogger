using MJH.Classes;
using MJH.Interfaces;
using System;

namespace MJH
{
    public class Logger : ILogger
    {
        public string LogOutputFileLocation { get; set; }
        public string LogOutputFileName { get; set; }
        public string LoggingLevel { get; set; }

        private readonly LoggingFile _loggingFile;

        public Logger()
        {
            _loggingFile = new LoggingFile();
        }

        public void LogError(LogCategory logCategory, Exception exception)
        {
            SetupLogLocation();
            _loggingFile.Write("ERROR", logCategory, GenerateError.GetException(exception), DateTime.Now);
        }

        public void LogInfo(LogCategory logCategory, Exception exception)
        {
            SetupLogLocation();
            _loggingFile.Write("INFO", logCategory, GenerateError.GetException(exception), DateTime.Now);
        }

        public void LogDebug(LogCategory logCategory, Exception exception)
        {
            SetupLogLocation();
            _loggingFile.Write("DEBUG", logCategory, GenerateError.GetException(exception), DateTime.Now);
        }

        private void SetupLogLocation()
        {
            _loggingFile.LoggingFileLocation = LogOutputFileLocation;
            _loggingFile.LoggingFileName = LogOutputFileName;

            if (!_loggingFile.Exists())
            {
                _loggingFile.Create();
            }
        }
    }
}
