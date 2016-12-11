using System;
using MJH.Factories;
using MJH.Interfaces;
using MJH.Models;

namespace MJH
{
    public class Logger
    {
        private readonly ILogger _logger;

        public Logger(LoggingTypeModel.LogOutputType loggerOutputType)
        {
            _logger = new LoggerFactory().GetLoggerRepository(loggerOutputType);
        }

        public void LogError(LoggingTypeModel.LogCategory logCategory, Exception exception)
        {
            _logger.LogError(logCategory, exception);
        }

        public void LogInfo(LoggingTypeModel.LogCategory logCategory, Exception exception)
        {
            _logger.LogInfo(logCategory, exception);
        }

        public void LogDebug(LoggingTypeModel.LogCategory logCategory, Exception exception)
        {
            _logger.LogDebug(logCategory, exception);
        }

        public void LogError(LoggingTypeModel.LogCategory logCategory, string message)
        {
            _logger.LogError(logCategory, message);
        }

        public void LogInfo(LoggingTypeModel.LogCategory logCategory, string message)
        {
            _logger.LogInfo(logCategory, message);
        }

        public void LogDebug(LoggingTypeModel.LogCategory logCategory, string message)
        {
            _logger.LogDebug(logCategory, message);
        }
    }
}
