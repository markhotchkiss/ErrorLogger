using MJH.Factories;
using MJH.Interfaces;
using MJH.Models;
using System;

namespace MJH
{
    public static class Logger
    {
        private static readonly ILogger LoggerInterface;

        static Logger()
        {
            var loggerFactory = new LoggerFactory();
            LoggerInterface = loggerFactory.GetLoggerRepository();
        }

        public static void LogError(LoggingTypeModel.LogCategory logCategory, Exception exception)
        {
            LoggerInterface.LogError(logCategory, exception);
        }

        public static void LogInfo(LoggingTypeModel.LogCategory logCategory, Exception exception)
        {
            LoggerInterface.LogInfo(logCategory, exception);
        }

        public static void LogDebug(LoggingTypeModel.LogCategory logCategory, Exception exception)
        {
            LoggerInterface.LogDebug(logCategory, exception);
        }

        public static void LogError(LoggingTypeModel.LogCategory logCategory, string message)
        {
            LoggerInterface.LogError(logCategory, message);
        }

        public static void LogInfo(LoggingTypeModel.LogCategory logCategory, string message)
        {
            LoggerInterface.LogInfo(logCategory, message);
        }

        public static void LogDebug(LoggingTypeModel.LogCategory logCategory, string message)
        {
            LoggerInterface.LogDebug(logCategory, message);
        }
    }
}
