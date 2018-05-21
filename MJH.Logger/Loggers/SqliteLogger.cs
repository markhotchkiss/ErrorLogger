using MJH.BusinessLogic.Generic;
using MJH.BusinessLogic.Sqlite;
using MJH.Interfaces;
using MJH.Models;
using System;
using System.Collections.Generic;

namespace MJH.Loggers
{
    public class SqliteLogger : ILogger, ITransaction
    {
        public LoggingLevel LoggingLevel { get; set; }

        private readonly LoggingSqlite _logger;

        public SqliteLogger()
        {
            _logger = new LoggingSqlite();

            if (!_logger.Exists())
            {
                _logger.Create();
            }
        }
        public void LogError(LoggingTypeModel.LogCategory logCategory, Exception exception)
        {
            if (!LoggingLevelEnabled.Decide(LoggingLevel).Error)
            {
                return;
            }

            _logger.WriteToErrorLog("ERROR", logCategory, GenerateError.GetException(exception), DateTime.Now);
        }

        public void LogInfo(LoggingTypeModel.LogCategory logCategory, Exception exception)
        {
            if (!LoggingLevelEnabled.Decide(LoggingLevel).Info)
            {
                return;
            }

            _logger.WriteToErrorLog("INFO", logCategory, GenerateError.GetException(exception), DateTime.Now);
        }

        public void LogDebug(LoggingTypeModel.LogCategory logCategory, Exception exception)
        {
            if (!LoggingLevelEnabled.Decide(LoggingLevel).Debug)
            {
                return;
            }

            _logger.WriteToErrorLog("DEBUG", logCategory, GenerateError.GetException(exception), DateTime.Now);
        }

        public void LogError(LoggingTypeModel.LogCategory logCategory, string message)
        {
            if (!LoggingLevelEnabled.Decide(LoggingLevel).Error)
            {
                return;
            }

            _logger.WriteToErrorLog("ERROR", logCategory, AllowedCharacters.Replace(message), DateTime.Now);
        }

        public void LogInfo(LoggingTypeModel.LogCategory logCategory, string message)
        {
            if (!LoggingLevelEnabled.Decide(LoggingLevel).Info)
            {
                return;
            }

            _logger.WriteToErrorLog("INFO", logCategory, AllowedCharacters.Replace(message), DateTime.Now);
        }

        public void LogDebug(LoggingTypeModel.LogCategory logCategory, string message)
        {
            if (!LoggingLevelEnabled.Decide(LoggingLevel).Debug)
            {
                return;
            }

            _logger.WriteToErrorLog("DEBUG", logCategory, AllowedCharacters.Replace(message), DateTime.Now);
        }

        public IReadOnlyCollection<Error> ReadLog()
        {
            throw new NotImplementedException();
        }

        public void LogTransaction(DateTime logDateTime, string sourceId, string logMessage)
        {
            _logger.WriteToTransactionLog(sourceId, logMessage, logDateTime);
        }
    }
}
