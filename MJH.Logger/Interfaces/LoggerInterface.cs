using Microsoft.Build.Framework;
using System;

namespace MJH.Interfaces
{
    public interface ILogger
    {
        [Required]
        string LogOutputFileLocation { get; set; }

        [Required]
        string LoggingLevel { get; set; }

        void LogError(LogCategory logCategory, Exception exception);

        void LogInfo(LogCategory logCategory, Exception exception);

        void LogDebug(LogCategory logCategory, Exception exception);
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
