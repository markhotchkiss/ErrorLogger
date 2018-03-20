using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MJH.BusinessLogic.Configuration;
using MJH.Interfaces;
using MJH.Loggers;
using MJH.Models;

namespace MJH.Factories
{
    internal class TransactionFactory
    {
        private readonly LoggerConfig _config;

        public TransactionFactory()
        {
            _config = new ConfigurationHandler().Read();
        }

        internal ITransaction GetTransactionRepository()
        {
            ITransaction result = null;
            switch (_config.LoggerType)
            {
                case LoggingTypeModel.LogOutputType.TextFile:
                    result = new TextLogger
                    {
                        LogOutputFileLocation = _config.Text.FileInformation.LogFileLocation,
                        LogOutputFileName = _config.Text.FileInformation.LogFileName
                    };
                    break;
                case LoggingTypeModel.LogOutputType.SQL:
                    result = new SqlLogger();
                    break;
                case LoggingTypeModel.LogOutputType.SQLite:
                    result = new SqliteLogger();
                    break;
                default:
                    throw new ArgumentOutOfRangeException();

            }
            return result;
        }
    }
}
