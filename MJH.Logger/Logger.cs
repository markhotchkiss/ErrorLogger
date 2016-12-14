using MJH.Entities;
using MJH.Factories;
using MJH.Interfaces;
using MJH.Models;
using System;
using System.Collections.Generic;

namespace MJH
{
    public static class Logger
    {
        private static readonly ILogger LoggerInterface;
        private static readonly ILogReader LogReader;

        static Logger()
        {
            var loggerFactory = new LoggerFactory();
            LoggerInterface = loggerFactory.GetLoggerRepository();

            var logReaderFactory = new LogReaderFactory();
            LogReader = logReaderFactory.GetLogReaderRepository();
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

        public static IReadOnlyCollection<Error> Read()
        {
            return LogReader.Read();
        }
    }
}