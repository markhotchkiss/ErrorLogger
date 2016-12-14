using MJH.BusinessLogic.Configuration;
using MJH.Models;
using NUnit.Framework;
using System;
using System.IO;

namespace MJH.UnitTests
{
    [TestFixture]
    public class UnitTestsSqlite
    {
        private LoggerConfig _config;

        [OneTimeSetUp]
        public void InitialiseTests()
        {
            _config = new ConfigurationHandler().Read();

            _config.LoggerType = LoggingTypeModel.LogOutputType.SQLite;

            new ConfigurationHandler().Write(_config);

            if (_config.LoggerType != LoggingTypeModel.LogOutputType.SQLite)
            {
                throw new Exception("Set the logger type to SQLite in the Config file before running these tests.");
            }
        }

        [Test, Order(1)]
        public void WriteSqliteError()
        {
            for (int i = 0; i < 10; i++)
            {
                try
                {
                    throw new Exception("ErrorLogger", new Exception("This is my inner exception"));
                }
                catch (Exception exception)
                {
                    Logger.LogError(LoggingTypeModel.LogCategory.Process, exception);
                }
            }

            //Check that the log file exists with text inside
            var fileInfo = new FileInfo(_config.SQLite.ServerInformation.LogFileLocation + "\\" + _config.SQLite.ServerInformation.LogFileName);
            Assert.True(fileInfo.Exists);
        }

        [Test, Order(2)]
        public void WriteSqliteInfo()
        {
            for (int i = 0; i < 10; i++)
            {
                try
                {
                    throw new Exception("ErrorLogger", new Exception("This is my inner exception"));
                }
                catch (Exception exception)
                {
                    Logger.LogInfo(LoggingTypeModel.LogCategory.Process, exception);
                }
            }

            //Check that the log file exists with text inside
            var fileInfo = new FileInfo(_config.SQLite.ServerInformation.LogFileLocation + "\\" + _config.SQLite.ServerInformation.LogFileName);
            Assert.True(fileInfo.Exists);
        }

        [Test, Order(3)]
        public void WriteSqliteDebug()
        {
            for (int i = 0; i < 10; i++)
            {
                try
                {
                    throw new Exception("ErrorLogger", new Exception("This is my inner exception"));
                }
                catch (Exception exception)
                {
                    Logger.LogDebug(LoggingTypeModel.LogCategory.Process, exception);
                }
            }

            //Check that the log file exists with text inside
            var fileInfo = new FileInfo(_config.SQLite.ServerInformation.LogFileLocation + "\\" + _config.SQLite.ServerInformation.LogFileName);
            Assert.True(fileInfo.Exists);
        }

        [Test, Order(4)]
        public void Read()
        {
            var log = Logger.Read();
            Assert.IsNotNull(log);
        }
    }
}
