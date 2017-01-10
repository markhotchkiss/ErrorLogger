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
            try
            {
                LoggerInterface.LogError(logCategory, exception);
            }
            catch
            {
            }
        }

        public static void LogInfo(LoggingTypeModel.LogCategory logCategory, Exception exception)
        {
            try
            {
                LoggerInterface.LogInfo(logCategory, exception);
            }
            catch
            {
            }

        }

        public static void LogDebug(LoggingTypeModel.LogCategory logCategory, Exception exception)
        {
            try
            {
                LoggerInterface.LogDebug(logCategory, exception);
            }
            catch
            {
            }

        }

        public static void LogError(LoggingTypeModel.LogCategory logCategory, string message)
        {
            try
            {
                LoggerInterface.LogError(logCategory, message);
            }
            catch
            {
            }
        }

        public static void LogInfo(LoggingTypeModel.LogCategory logCategory, string message)
        {
            try
            {
                LoggerInterface.LogInfo(logCategory, message);
            }
            catch
            {
            }

        }

        public static void LogDebug(LoggingTypeModel.LogCategory logCategory, string message)
        {
            try
            {
                LoggerInterface.LogDebug(logCategory, message);
            }
            catch
            {
            }
        }

        public static IReadOnlyCollection<Error> Read()
        {
            try
            {
                return LogReader.Read();
            }
            catch
            {
                return null;
            }

        }
    }
}