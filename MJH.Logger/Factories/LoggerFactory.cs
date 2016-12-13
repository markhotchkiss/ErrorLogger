using MJH.BusinessLogic.Configuration;
using MJH.Interfaces;
using MJH.Loggers;
using MJH.Models;
using System;

namespace MJH.Factories
{
    internal class LoggerFactory
    {
        private readonly LoggerConfig _config = new ConfigurationHandler().Read();

        internal ILogger GetLoggerRepository()
        {
            ILogger repo = null;
            switch (_config.LoggerType)
            {
                case LoggingTypeModel.LogOutputType.TextFile:
                    repo = new TextLogger
                    {
                        LogOutputFileLocation = _config.Text.FileInformation.LogFileLocation,
                        LogOutputFileName = _config.Text.FileInformation.LogFileName,
                        LoggingLevel = _config.LoggingLevel
                    };
                    break;
                case LoggingTypeModel.LogOutputType.SQL:
                    repo = new SqlLogger();
                    break;
                case LoggingTypeModel.LogOutputType.SQLite:
                    repo = new SqliteLogger();
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            return repo;
        }
    }
}
