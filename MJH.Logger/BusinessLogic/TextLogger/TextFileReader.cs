using CsvHelper;
using MJH.BusinessLogic.Configuration;
using MJH.Entities;
using MJH.Interfaces;
using MJH.Models;
using System.Collections.Generic;
using System.IO;

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
            var csv = new CsvReader(new StreamReader(_config.Text.FileInformation.LogFileLocation + "\\" +
                                                     _config.Text.FileInformation.LogFileName));

            csv.Configuration.QuoteAllFields = true;
            csv.Configuration.HasHeaderRecord = true;

            var errorLog = csv.GetRecords<Error>();

            var logFile = new List<Error>();

            foreach (var error in errorLog)
            {
                var log = new Error
                {
                    LoggingLevel = error.LoggingLevel,
                    ErrorType = error.ErrorType,
                    Message = error.Message,
                    DateTimeUTC = error.DateTimeUTC
                };

                logFile.Add(log);
            }

            return logFile;
        }
    }
}
