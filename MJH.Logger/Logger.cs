using System;
using MJH.Factories;
using MJH.Interfaces;
using MJH.Models;

namespace MJH
{
    public static class Logger
    {
        private static readonly ILogger _logger;

        static Logger()
        {
            _logger = new LoggerFactory().GetLoggerRepository(LoggingTypeModel.LogOutputType.TextFile);
        }

        public static void LogError(LoggingTypeModel.LogCategory logCategory, Exception exception)
        {
            _logger.LogError(logCategory, exception);
        }

        public static void LogInfo(LoggingTypeModel.LogCategory logCategory, Exception exception)
        {
            _logger.LogInfo(logCategory, exception);
        }

        public static void LogDebug(LoggingTypeModel.LogCategory logCategory, Exception exception)
        {
            _logger.LogDebug(logCategory, exception);
        }

        public static void LogError(LoggingTypeModel.LogCategory logCategory, string message)
        {
            _logger.LogError(logCategory, message);
        }

        public static void LogInfo(LoggingTypeModel.LogCategory logCategory, string message)
        {
            _logger.LogInfo(logCategory, message);
        }

        public static void LogDebug(LoggingTypeModel.LogCategory logCategory, string message)
        {
            _logger.LogDebug(logCategory, message);
        }
    }
}
