﻿using MJH.Interfaces;
using MJH.Models;
using System;
using System.IO;

namespace MJH.BusinessLogic.TextLogger
{
    internal class LoggingFile : ILoggingWriter
    {
        internal string LoggingFileLocation;

        internal string LoggingFileName;

        public bool Exists()
        {
            var directoryInfo = new DirectoryInfo(LoggingFileLocation);

            return directoryInfo.Exists;
        }

        public void Create()
        {
            var directoryInfo = new DirectoryInfo(LoggingFileLocation);

            directoryInfo.Create();
        }

        public void WriteToErrorLog(string loggingLevel, LoggingTypeModel.LogCategory logCategory, string error, DateTime dateTime)
        {
            var log = new FileInfo(LoggingFileLocation + "\\" + LoggingFileName);

            if (!log.Exists)
            {
                using (var streamWriter = new StreamWriter(LoggingFileLocation + "\\" + LoggingFileName, true))
                {
                    var textToWrite = $"\"Id\",\"LoggingLevel\",\"ErrorType\",\"Message\",\"DateTimeUTC\"";

                    streamWriter.WriteLine(textToWrite);
                    streamWriter.Flush();
                    streamWriter.Close();
                }
            }

            using (var streamWriter = new StreamWriter(LoggingFileLocation + "\\" + LoggingFileName, true))
            {
                var textToWrite = $"\"0\",\"{ loggingLevel }\",\"{logCategory}\",\"{error.Replace("\"", "")}\",\"{dateTime:yyyy-MM-dd HH:mm:ss}\"";

                streamWriter.WriteLine(textToWrite);
                streamWriter.Flush();
                streamWriter.Close();
            }
        }

        internal float GetSize()
        {
            var loggingFile = new FileInfo(LoggingFileLocation + "\\" + LoggingFileName);

            var fileSize = Convert.ToInt32(loggingFile.Length) / 1024f / 1024f;

            return fileSize;
        }

        public void Write(DateTime logDateTime, string sourceId, string logMessage)
        {
            var log = new FileInfo(LoggingFileLocation + "\\" + LoggingFileName);

            if (!log.Exists)
            {
                using (var streamWriter = new StreamWriter(LoggingFileLocation + "\\" + LoggingFileName, true))
                {
                    var textToWrite = $"\"Id\",\"SourceId\",\"Message\",\"DateTimeUTC\"";

                    streamWriter.WriteLine(textToWrite);
                    streamWriter.Flush();
                    streamWriter.Close();
                }
            }

            using (var streamWriter = new StreamWriter(LoggingFileLocation + "\\" + LoggingFileName, true))
            {
                var textToWrite = $"\"0\",\"{ sourceId }\",\"{logMessage.Replace("\"", "")}\",\"{logDateTime:yyyy-MM-dd HH:mm:ss}\"";

                streamWriter.WriteLine(textToWrite);
                streamWriter.Flush();
                streamWriter.Close();
            }
        }
    }
}
