using MJH.Classes;
using MJH.Interfaces;
using MJH.Models;
using System;

namespace MJH.Loggers
{
    public class TextLogger : ILogger
    {
        public string LogOutputFileLocation { get; set; }
        public string LogOutputFileName { get; set; }
        public LoggingLevel LoggingLevel { get; set; }

        private readonly LoggingFile _loggingFile;

        public TextLogger()
        {
            _loggingFile = new LoggingFile();

            CheckArchive();
        }

        public void LogError(LoggingTypeModel.LogCategory logCategory, Exception exception)
        {
            if (!SetLoggingLevel().Error)
            {
                return;
            }

            SetupLogLocation();

            _loggingFile.Write("ERROR", logCategory, GenerateError.GetException(exception), DateTime.Now);
        }

        public void LogInfo(LoggingTypeModel.LogCategory logCategory, Exception exception)
        {
            if (!SetLoggingLevel().Info)
            {
                return;
            }

            SetupLogLocation();
            _loggingFile.Write("INFO", logCategory, GenerateError.GetException(exception), DateTime.Now);
        }

        public void LogDebug(LoggingTypeModel.LogCategory logCategory, Exception exception)
        {
            if (!SetLoggingLevel().Debug)
            {
                return;
            }

            SetupLogLocation();
            _loggingFile.Write("DEBUG", logCategory, GenerateError.GetException(exception), DateTime.Now);
        }

        public void LogError(LoggingTypeModel.LogCategory logCategory, string message)
        {
            if (!SetLoggingLevel().Error)
            {
                return;
            }

            SetupLogLocation();
            _loggingFile.Write("ERROR", logCategory, message, DateTime.Now);
        }

        public void LogInfo(LoggingTypeModel.LogCategory logCategory, string message)
        {
            if (!SetLoggingLevel().Info)
            {
                return;
            }

            SetupLogLocation();
            _loggingFile.Write("INFO", logCategory, message, DateTime.Now);
        }

        public void LogDebug(LoggingTypeModel.LogCategory logCategory, string message)
        {
            if (!SetLoggingLevel().Debug)
            {
                return;
            }

            SetupLogLocation();
            _loggingFile.Write("DEBUG", logCategory, message, DateTime.Now);
        }

        private void SetupLogLocation()
        {
            _loggingFile.LoggingFileLocation = LogOutputFileLocation;
            _loggingFile.LoggingFileName = LogOutputFileName;

            if (!_loggingFile.Exists())
            {
                _loggingFile.Create();
            }
        }

        private LoggingLevelModel SetLoggingLevel()
        {
            return new LoggingLevelEnabled().Decide(LoggingLevel);
        }

        private void CheckArchive()
        {
            var archive = new Archive();

            if (!archive.CheckArchiveFolderExists())
            {
                archive.CreateArchiveFolder();
            }

            archive.ArchiveLogFile();
        }
    }
}
