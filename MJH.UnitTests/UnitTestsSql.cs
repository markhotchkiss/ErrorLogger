using MJH.BusinessLogic.Configuration;
using MJH.Models;
using NUnit.Framework;
using System;

namespace MJH.UnitTests
{
    [TestFixture]
    public class UnitTestsSql
    {
        private LoggerConfig _config;

        [OneTimeSetUp]
        public void InitialiseTests()
        {
            _config = new ConfigurationHandler().Read();

            _config.LoggerType = LoggingTypeModel.LogOutputType.SQL;

            new ConfigurationHandler().Write(_config);

            if (_config.LoggerType != LoggingTypeModel.LogOutputType.SQL)
            {
                throw new Exception("Set the logger type to SQL in the Config file before running these tests.");
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
        }
    }
}
