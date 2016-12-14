using MJH.BusinessLogic.Configuration;
using MJH.Entities;
using MJH.Interfaces;
using MJH.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
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
            var streamReader =
                new StreamReader(_config.Text.FileInformation.LogFileLocation + "\\" +
                                 _config.Text.FileInformation.LogFileName);

            string line;
            var counter = 0;
            var logFile = new List<Error>();

            while ((line = streamReader.ReadLine()) != null)
            {
                var splitLine = line.Split(',');

                DateTime dateTime;
                DateTime.TryParseExact(splitLine[3], "dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture, DateTimeStyles.None,
                    out dateTime);

                var log = new Error
                {
                    LoggingLevel = splitLine[0],
                    ErrorType = splitLine[1],
                    Message = splitLine[2],
                    DateTimeUTC = dateTime
                };

                logFile.Add(log);

                counter++;
            }

            streamReader.Close();

            return logFile;
        }
    }
}
