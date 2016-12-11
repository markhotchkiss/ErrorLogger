using Microsoft.Build.Framework;
using MJH.Models;
using System;

namespace MJH.Interfaces
{
    public interface ILogger
    {
        [Required]
        string LogOutputFileLocation { get; set; }

        [Required]
        LoggingLevel LoggingLevel { get; set; }

        void LogError(LoggingTypeModel.LogCategory logCategory, Exception exception);

        void LogInfo(LoggingTypeModel.LogCategory logCategory, Exception exception);

        void LogDebug(LoggingTypeModel.LogCategory logCategory, Exception exception);

        void LogError(LoggingTypeModel.LogCategory logCategory, string message);

        void LogInfo(LoggingTypeModel.LogCategory logCategory, string message);

        void LogDebug(LoggingTypeModel.LogCategory logCategory, string message);
    }


}
