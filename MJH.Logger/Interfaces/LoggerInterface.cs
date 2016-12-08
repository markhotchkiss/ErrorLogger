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

        void LogError(LogCategory logCategory, Exception exception);

        void LogInfo(LogCategory logCategory, Exception exception);

        void LogDebug(LogCategory logCategory, Exception exception);

        void LogError(LogCategory logCategory, string message);

        void LogInfo(LogCategory logCategory, string message);

        void LogDebug(LogCategory logCategory, string message);
    }

    public enum LogCategory
    {
        File,
        Process,
        Service,
        Api,
        Sdk,
        Http,
        Https,
        Sql,
        Email,
        Sms
    }
}
