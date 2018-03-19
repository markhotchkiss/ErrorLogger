using MJH.BusinessLogic.Generic;
using MJH.BusinessLogic.Sql;
using MJH.Interfaces;
using MJH.Models;
using System;
using MJH.Entities;

namespace MJH.Loggers
{
    public class SqlLogger : ILogger, ITransaction
    {
        public LoggingLevel LoggingLevel { get; set; }

        private readonly LoggingSql _logger;

        public SqlLogger()
        {
            _logger = new LoggingSql();

            if (!_logger.Exists())
            {
                _logger.Create();
            }

            _logger.Purge();
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

        public void LogTransaction(DateTime logDateTime, string sourceId, string logMessage)
        {
            _logger.Write(logDateTime, sourceId, logMessage);
        }
    }
}