using MJH.BusinessLogic.Configuration;
using MJH.BusinessLogic.TextLogger;
using MJH.Models;
using NUnit.Framework;
using System;
using System.IO;

namespace MJH.UnitTests
{
    [TestFixture]
    public class UnitTestsTextFile
    {
        private LoggerConfig _config;

        [OneTimeSetUp]
        public void InitialiseTests()
        {
            _config = new ConfigurationHandler().Read();

            _config.LoggerType = LoggingTypeModel.LogOutputType.TextFile;

            new ConfigurationHandler().Write(_config);

            if (_config.LoggerType != LoggingTypeModel.LogOutputType.TextFile)
            {
                throw new Exception("Set the logger type to TextFile in the Config file before running these tests.");
            }
        }

        [TearDown]
        public void Cleanup()
        {
            //If tests have failed, cleanup residual files.
            //var fileInfo = new FileInfo("D:\\Tests\\Logger\\Activity.log");
            //fileInfo.Delete();
        }

        [Test, Order(1)]
        public void LogError()
        {
            Logger.LogError(LoggingTypeModel.LogCategory.Process, new Exception("ErrorLogger", new Exception("This is my inner exception, and it contains a comma!")));

            var loggerConfig = _config.Text;

            //Check that the log file exists with text inside
            var fileInfo = new FileInfo(loggerConfig.FileInformation.LogFileLocation + "\\" + loggerConfig.FileInformation.LogFileName);
            Assert.True(fileInfo.Exists);

            var streamReader = new StreamReader(fileInfo.FullName);
            Assert.IsNotEmpty(streamReader.ReadToEnd());
            streamReader.Close();

            //File.Delete(fileInfo.FullName);
        }

        [Test, Order(2)]
        public void LogErrorWithNullInnerException()
        {
            Logger.LogError(LoggingTypeModel.LogCategory.Process, new Exception("ErrorLogger"));

            var loggerConfig = _config.Text;

            //Check that the log file exists with text inside
            var fileInfo = new FileInfo(loggerConfig.FileInformation.LogFileLocation + "\\" + loggerConfig.FileInformation.LogFileName);

            Assert.True(fileInfo.Exists);

            var streamReader = new StreamReader(fileInfo.FullName);
            var errorText = streamReader.ReadToEnd();
            streamReader.Close();

            Assert.IsNotEmpty(errorText);
            //Assert.That(!errorText.Contains("InnerException"));

            //File.Delete(fileInfo.FullName);
        }

        [Test, Order(3)]
        public void LogInfo()
        {
            Logger.LogInfo(LoggingTypeModel.LogCategory.Process, new Exception("ErrorLogger", new Exception("This is my inner exception")));

            //Check that the log file exists with text inside
            var loggerConfig = _config.Text;

            var fileInfo = new FileInfo(loggerConfig.FileInformation.LogFileLocation + "\\" + loggerConfig.FileInformation.LogFileName);
            Assert.True(fileInfo.Exists);

            var streamReader = new StreamReader(fileInfo.FullName);
            Assert.IsNotEmpty(streamReader.ReadToEnd());
            streamReader.Close();

            //File.Delete(fileInfo.FullName);
        }

        [Test, Order(4)]
        public void LogDebug()
        {
            Logger.LogDebug(LoggingTypeModel.LogCategory.Process, new Exception("ErrorLogger", new Exception("This is my inner exception")));

            var loggerConfig = _config.Text;

            //Check that the log file exists with text inside
            var fileInfo = new FileInfo(loggerConfig.FileInformation.LogFileLocation + "\\" + loggerConfig.FileInformation.LogFileName);
            Assert.True(fileInfo.Exists);

            var streamReader = new StreamReader(fileInfo.FullName);
            Assert.IsNotEmpty(streamReader.ReadToEnd());
            streamReader.Close();

            //File.Delete(fileInfo.FullName);
        }

        [Test, Order(5)]
        public void Read()
        {
            var log = Logger.Read();
            Assert.IsNotNull(log);
            Assert.That(log.Count > 0);
        }

        [Test, Order(6)]
        public void PurgeArchive()
        {
            var logger = new Archive();
            logger.Purge();
        }
    }
}