using MJH.Interfaces;
using MJH.Models;
using System;

namespace MJH.Loggers
{
    public class SqlLogger : ILogger
    {
        public void LogError(LoggingTypeModel.LogCategory logCategory, Exception exception)
        {
            throw new NotImplementedException();
        }

        public void LogInfo(LoggingTypeModel.LogCategory logCategory, Exception exception)
        {
            throw new NotImplementedException();
        }

        public void LogDebug(LoggingTypeModel.LogCategory logCategory, Exception exception)
        {
            throw new NotImplementedException();
        }

        public void LogError(LoggingTypeModel.LogCategory logCategory, string message)
        {
            throw new NotImplementedException();
        }

        public void LogInfo(LoggingTypeModel.LogCategory logCategory, string message)
        {
            throw new NotImplementedException();
        }

        public void LogDebug(LoggingTypeModel.LogCategory logCategory, string message)
        {
            throw new NotImplementedException();
        }
    }
}
