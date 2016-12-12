using MJH.Classes;
using MJH.Interfaces;
using MJH.Models;
using System;

namespace MJH.Loggers
{
    public class SqliteLogger : ILogger
    {
        public string LogOutputFileLocation { get; set; }
        public LoggingLevel LoggingLevel { get; set; }

        private readonly LoggingSqlite _logger;

        public SqliteLogger()
        {
            _logger = new LoggingSqlite();
        }
        public void LogError(LoggingTypeModel.LogCategory logCategory, Exception exception)
        {
            if (!LoggingLevelEnabled.Decide(LoggingLevel).Error)
            {
                return;
            }

            _logger.Write("ERROR", logCategory, GenerateError.GetException(exception), DateTime.Now);
        }

        public void LogInfo(LoggingTypeModel.LogCategory logCategory, Exception exception)
        {
            if (!LoggingLevelEnabled.Decide(LoggingLevel).Info)
            {
                return;
            }

            _logger.Write("INFO", logCategory, GenerateError.GetException(exception), DateTime.Now);
        }

        public void LogDebug(LoggingTypeModel.LogCategory logCategory, Exception exception)
        {
            if (!LoggingLevelEnabled.Decide(LoggingLevel).Debug)
            {
                return;
            }

            _logger.Write("DEBUG", logCategory, GenerateError.GetException(exception), DateTime.Now);
        }

        public void LogError(LoggingTypeModel.LogCategory logCategory, string message)
        {
            if (!LoggingLevelEnabled.Decide(LoggingLevel).Error)
            {
                return;
            }

            _logger.Write("ERROR", logCategory, message, DateTime.Now);
        }

        public void LogInfo(LoggingTypeModel.LogCategory logCategory, string message)
        {
            if (!LoggingLevelEnabled.Decide(LoggingLevel).Info)
            {
                return;
            }

            _logger.Write("INFO", logCategory, message, DateTime.Now);
        }

        public void LogDebug(LoggingTypeModel.LogCategory logCategory, string message)
        {
            if (!LoggingLevelEnabled.Decide(LoggingLevel).Debug)
            {
                return;
            }

            _logger.Write("DEBUG", logCategory, message, DateTime.Now);
        }
    }
}
