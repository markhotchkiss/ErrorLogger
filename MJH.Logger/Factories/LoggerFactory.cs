using MJH.BusinessLogic.Configuration;
using MJH.Interfaces;
using MJH.Loggers;
using MJH.Models;
using System;
using System.IO;

namespace MJH.Factories
{
    internal class LoggerFactory
    {
        private readonly LoggerConfig _config;

        public LoggerFactory()
        {
            _config = new ConfigurationHandler().Read();
        }

        internal ILogger GetLoggerRepository()
        {
            ILogger repo;
            switch (_config.LoggerType)
            {
                case LoggingTypeModel.LogOutputType.TextFile:
                    var textOutput = new DirectoryInfo(_config.Text.FileInformation.LogFileLocation);
                    if (!textOutput.Exists)
                    {
                        textOutput.Create();
                    }
                    repo = new TextLogger
                    {
                        LogOutputFileLocation = _config.Text.FileInformation.LogFileLocation,
                        LogOutputFileName = _config.Text.FileInformation.LogFileName,
                        LoggingLevel = _config.LoggingLevel
                    };
                    break;
                case LoggingTypeModel.LogOutputType.SQL:
                    repo = new SqlLogger()
                    {
                        LoggingLevel = _config.LoggingLevel
                    };
                    break;
                case LoggingTypeModel.LogOutputType.SQLite:
                    var sqliteOutput = new DirectoryInfo(_config.SQLite.ServerInformation.LogFileLocation);
                    if (!sqliteOutput.Exists)
                    {
                        sqliteOutput.Create();
                    }
                    repo = new SqliteLogger()
                    {
                        LoggingLevel = _config.LoggingLevel
                    };
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            return repo;
        }
    }
}
