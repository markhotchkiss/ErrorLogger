using MJH.Models;
using System;
using System.IO;

namespace MJH.Classes
{
    internal class LoggingFile
    {
        internal string LoggingFileLocation;

        internal string LoggingFileName;

        internal bool Exists()
        {
            var directoryInfo = new DirectoryInfo(LoggingFileLocation);

            return directoryInfo.Exists;
        }

        internal void Create()
        {
            var directoryInfo = new DirectoryInfo(LoggingFileLocation);

            directoryInfo.Create();
        }

        internal void Write(string loggingLevel, LoggingTypeModel.LogCategory logCategory, string error, DateTime dateTime)
        {
            using (var streamWriter = new StreamWriter(LoggingFileLocation + "\\" + LoggingFileName, true))
            {
                var textToWrite = $"{ loggingLevel } : {logCategory} : {dateTime} : {error}";

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
