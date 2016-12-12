using MJH.Models;
using System;

namespace MJH.Interfaces
{
    public interface ILogger
    {

        void LogError(LoggingTypeModel.LogCategory logCategory, Exception exception);

        void LogInfo(LoggingTypeModel.LogCategory logCategory, Exception exception);

        void LogDebug(LoggingTypeModel.LogCategory logCategory, Exception exception);

        void LogError(LoggingTypeModel.LogCategory logCategory, string message);

        void LogInfo(LoggingTypeModel.LogCategory logCategory, string message);

        void LogDebug(LoggingTypeModel.LogCategory logCategory, string message);
    }


}
