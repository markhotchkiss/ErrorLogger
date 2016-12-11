using System;
using MJH.Interfaces;
using MJH.Models;

namespace MJH.Factories
{
    public class LoggerFactory
    {
        public ILogger GetLoggerRepository(LoggingTypeModel.LogOutputType outputType)
        {
            ILogger repo = null;
            switch (outputType)
            {
                case LoggingTypeModel.LogOutputType.TextFile:
                    repo = new TextLogger
                    {
                        LogOutputFileLocation = "C:\\Tests\\Logger",
                        LogOutputFileName = "ActivityLog.log",
                        LoggingLevel = LoggingLevel.Debug
                    };
                    break;
                case LoggingTypeModel.LogOutputType.SQL:
                    //TODO add new logger here.
                    break;
                case LoggingTypeModel.LogOutputType.SQLite:
                    //TODO add new logger here.
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(outputType), outputType, null);
            }

            return repo;
        }
    }
}
