﻿using MJH.BusinessLogic.Configuration;
using MJH.Interfaces;
using MJH.Loggers;
using MJH.Models;
using System;

namespace MJH.Factories
{
    internal class LoggerFactory
    {
        private LoggerConfig _config;

        public LoggerFactory()
        {
            _config = new ConfigurationHandler().Read();
        }

        internal ILogger GetLoggerRepository()
        {
            _config = new ConfigurationHandler().Read();

            ILogger repo;
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
                    repo = new SqlLogger()
                    {
                        LoggingLevel = _config.LoggingLevel
                    };
                    break;
                case LoggingTypeModel.LogOutputType.SQLite:
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
