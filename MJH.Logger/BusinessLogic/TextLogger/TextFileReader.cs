using CsvHelper;
using MJH.BusinessLogic.Configuration;
using MJH.Entities;
using MJH.Interfaces;
using MJH.Models;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace MJH.BusinessLogic.TextLogger
{
    public class TextFileReader : ILogReader
    {
        private readonly LoggerConfig _config;

        public TextFileReader()
        {
            _config = new ConfigurationHandler().Read();
        }

        public IReadOnlyCollection<Error> Read()
        {
            var fs = new FileStream(_config.Text.FileInformation.LogFileLocation + "\\" +
                                    _config.Text.FileInformation.LogFileName, FileMode.Open, FileAccess.Read,
                FileShare.ReadWrite);

            var csv = new CsvReader(new StreamReader(fs));

            csv.Configuration.QuoteAllFields = true;
            csv.Configuration.HasHeaderRecord = true;

            var errorLog = csv.GetRecords<Error>();

            var logFile = errorLog.Select(error => new Error
            {
                LoggingLevel = error.LoggingLevel,
                ErrorType = error.ErrorType,
                Message = error.Message,
                DateTimeUTC = error.DateTimeUTC
            }).ToList();

            fs.Close();

            return logFile;
        }
    }
}
