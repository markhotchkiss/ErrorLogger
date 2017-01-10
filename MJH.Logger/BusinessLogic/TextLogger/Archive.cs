using MJH.BusinessLogic.Configuration;
using MJH.Interfaces;
using MJH.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace MJH.BusinessLogic.TextLogger
{
    public class Archive : IArchive, ILoggingPurge
    {
        public void ArchiveLogFile()
        {
            //Ensure that file exists before continuing
            var config = GetConfig();

            var fi = new FileInfo(config.Text.FileInformation.LogFileLocation + "\\" + config.Text.FileInformation.LogFileName);

            if (!fi.Exists)
            {
                return;
            }

            if (ShouldArchive())
            {
                var currentLoggingFile = config.Text.FileInformation.LogFileLocation + "\\" +
                                         config.Text.FileInformation.LogFileName;

                var archiveFolder = new DirectoryInfo(config.Text.FileInformation.ArchiveDirectory);

                var fileInfo = new FileInfo(currentLoggingFile);

                fileInfo.MoveTo(config.Text.FileInformation.ArchiveDirectory + "\\" + fileInfo.Name + "." + GetMaxLogFile(archiveFolder));
            }
        }

        public bool CheckArchiveFolderExists()
        {
            var archiveFolder = new DirectoryInfo(GetConfig().Text.FileInformation.ArchiveDirectory);

            if (archiveFolder.Exists)
            {
                return true;
            }

            return false;
        }

        public void CreateArchiveFolder()
        {
            var archiveFolder = new DirectoryInfo(GetConfig().Text.FileInformation.ArchiveDirectory);

            archiveFolder.Create();
        }

        private static float CheckLogFileSize()
        {
            var loggingFile = new LoggingFile
            {
                LoggingFileLocation = GetConfig().Text.FileInformation.LogFileLocation,
                LoggingFileName = GetConfig().Text.FileInformation.LogFileName
            };

            return loggingFile.GetSize();
        }

        private bool ShouldArchive()
        {
            if (CheckLogFileSize() > GetConfig().Text.LoggerInformation.MaxFileSize)
            {
                return true;
            }

            return false;
        }

        private static LoggerConfig GetConfig()
        {
            return new ConfigurationHandler().Read();
        }

        private static int GetMaxLogFile(DirectoryInfo diInfo)
        {
            var di = new DirectoryInfo(diInfo.FullName);

            var numberList = new List<int>();

            foreach (var file in di.EnumerateFiles())
            {
                var fileSplit = file.Name.Split('.');
                var number = Convert.ToInt32(fileSplit[fileSplit.Length - 1]);

                numberList.Add(number);
            }

            if (!numberList.Any())
            {
                return 1;
            }

            return numberList.Max() + 1;
        }

        public void Purge()
        {
            var config = GetConfig();

            var directoryInfo = new DirectoryInfo(config.Text.FileInformation.ArchiveDirectory);

            var listToRemove = new List<string>();

            var fileCount = 0;

            foreach (var file in directoryInfo.EnumerateFiles().OrderBy(d => d.LastWriteTime))
            {
                if (fileCount < config.Text.LoggerInformation.FileHistoryToKeep)
                {
                    fileCount++;
                    continue;
                }

                listToRemove.Add(file.FullName);
            }

            foreach (var file in listToRemove)
            {
                File.Delete(file);
            }
        }
    }
}
