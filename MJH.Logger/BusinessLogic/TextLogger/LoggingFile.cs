using MJH.Interfaces;
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

        public void Write(string loggingLevel, LoggingTypeModel.LogCategory logCategory, string error, DateTime dateTime)
        {
            using (var streamWriter = new StreamWriter(LoggingFileLocation + "\\" + LoggingFileName, true))
            {
                var textToWrite = $"{ loggingLevel },{logCategory},{error},{dateTime}";

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
    }
}
