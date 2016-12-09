using MJH.Classes;
using MJH.Interfaces;
using MJH.Models;
using System;

namespace MJH
{
    public class Logger : ILogger
    {
        public string LogOutputFileLocation { get; set; }
        public string LogOutputFileName { get; set; }
        public LoggingLevel LoggingLevel { get; set; }

        private readonly LoggingFile _loggingFile;

        public Logger()
        {
            _loggingFile = new LoggingFile();
        }

        public void LogError(LogCategory logCategory, Exception exception)
        {
            if (!SetLoggingLevel().Error)
            {
                return;
            }

            SetupLogLocation();
            _loggingFile.Write("ERROR", logCategory, GenerateError.GetException(exception), DateTime.Now);
        }

        public void LogInfo(LogCategory logCategory, Exception exception)
        {
            if (!SetLoggingLevel().Info)
            {
                return;
            }

            SetupLogLocation();
            _loggingFile.Write("INFO", logCategory, GenerateError.GetException(exception), DateTime.Now);
        }

        public void LogDebug(LogCategory logCategory, Exception exception)
        {
            if (!SetLoggingLevel().Debug)
            {
                return;
            }

            SetupLogLocation();
            _loggingFile.Write("DEBUG", logCategory, GenerateError.GetException(exception), DateTime.Now);
        }

        public void LogError(LogCategory logCategory, string message)
        {
            if (!SetLoggingLevel().Error)
            {
                return;
            }

            SetupLogLocation();
            _loggingFile.Write("ERROR", logCategory, message, DateTime.Now);
        }

        public void LogInfo(LogCategory logCategory, string message)
        {
            if (!SetLoggingLevel().Info)
            {
                return;
            }

            SetupLogLocation();
            _loggingFile.Write("INFO", logCategory, message, DateTime.Now);
        }

        public void LogDebug(LogCategory logCategory, string message)
        {
            if (!SetLoggingLevel().Debug)
            {
                return;
            }

            SetupLogLocation();
            _loggingFile.Write("DEBUG", logCategory, message, DateTime.Now);
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

        private LoggingLevelModel SetLoggingLevel()
        {
            return new LoggingLevelEnabled().Decide(LoggingLevel);
        }
    }
}
