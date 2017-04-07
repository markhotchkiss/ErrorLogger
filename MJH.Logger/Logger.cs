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
        private static readonly ILogReaderV2 LogReaderV2;

        static Logger()
        {
            var loggerFactory = new LoggerFactory();
            LoggerInterface = loggerFactory.GetLoggerRepository();

            var logReaderFactory = new LogReaderFactory();
            LogReader = logReaderFactory.GetLogReaderRepository();
            LogReaderV2 = logReaderFactory.GetLogReaderV2Repository();
        }

        public static void LogError(LoggingTypeModel.LogCategory logCategory, Exception exception)
        {
            try
            {
                Console.WriteLine($"{DateTime.Now} - {logCategory.ToString()} - {exception.ToString()}");
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
                Console.WriteLine($"{DateTime.Now} - {logCategory.ToString()} - {exception.ToString()}");
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
                Console.WriteLine($"{DateTime.Now} - {logCategory.ToString()} - {exception.ToString()}");
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
                Console.WriteLine($"{DateTime.Now} - {logCategory.ToString()} - {message}");
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
                Console.WriteLine($"{DateTime.Now} - {logCategory.ToString()} - {message}");
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
                Console.WriteLine($"{DateTime.Now} - {logCategory.ToString()} - {message}");
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

        public static IReadOnlyCollection<Error> Read(int recordCount)
        {
            try
            {
                return LogReaderV2.ReadMaxRecordCount(recordCount);
            }
            catch
            {
                return null;
            }
        }

        public static IReadOnlyCollection<Error> Read(DateTime startDate, DateTime endDate)
        {
            var log = LogReaderV2.ReadBetweenDates(startDate, endDate);

            return log;
        }

        public static IReadOnlyCollection<Error> Read(LoggingTypeModel.LogCategory logCategory)
        {
            var log = LogReaderV2.ReadSpecificLevel(logCategory);

            return log;
        }
    }
}