using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
                    repo = new TextLogger();
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(outputType), outputType, null);
            }

            return repo;
        }
    }
}
