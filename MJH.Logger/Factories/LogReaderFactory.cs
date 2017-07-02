using MJH.BusinessLogic.Configuration;
using MJH.BusinessLogic.Sql;
using MJH.BusinessLogic.TextLogger;
using MJH.Interfaces;
using MJH.Models;
using System;
using MJH.BusinessLogic.Sqlite;

namespace MJH.Factories
{
    internal class LogReaderFactory
    {
        private readonly LoggerConfig _config = new ConfigurationHandler().Read();

        internal ILogReader GetLogReaderRepository()
        {
            ILogReader repo = null;
            switch (_config.LoggerType)
            {
                case LoggingTypeModel.LogOutputType.TextFile:
                    repo = new TextFileReader();
                    break;
                case LoggingTypeModel.LogOutputType.SQL:
                    repo = new SqlReader();
                    break;
                case LoggingTypeModel.LogOutputType.SQLite:
                    repo = new SqliteReader();
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            return repo;
        }

        internal ILogReaderV2 GetLogReaderV2Repository()
        {
            ILogReaderV2 repo = null;
            switch (_config.LoggerType)
            {
                case LoggingTypeModel.LogOutputType.TextFile:
                    return null;
                case LoggingTypeModel.LogOutputType.SQL:
                    repo = new SqlReader();
                    break;
                case LoggingTypeModel.LogOutputType.SQLite:
                    repo = new SqliteReader();
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            return repo;
        }
    }
}
