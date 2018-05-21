using MJH.Models;
using NUnit.Framework;

namespace MJH.UnitTests
{
    [TestFixture]
    public class UnitTestsSql
    {
        private LoggerConfig _config;

        [OneTimeSetUp]
        public void InitialiseTests()
        {
            //_config = new ConfigurationHandler().Read();

            //_config.LoggerType = LoggingTypeModel.LogOutputType.SQL;

            //new ConfigurationHandler().Write(_config);

            //if (_config.LoggerType != LoggingTypeModel.LogOutputType.SQL)
            //{
            //    throw new Exception("Set the logger type to SQL in the Config file before running these tests.");
            //}
        }

        [Test]
        public void CreateSqlConnection()
        {
            //var sqlConnectionCreator = new SqlConnectionCreator(_config);
            //var sqlConnection = sqlConnectionCreator.BuildSqlConnection(Database.DefaultErrorLogger);

            //try
            //{
            //    sqlConnection.Open();

            //    Assert.That(sqlConnection.State == ConnectionState.Open);
            //}
            //catch (Exception exception)
            //{
            //    Assert.That(exception == null);
            //}

        }

        [Test, Order(1)]
        public void WriteSqlError()
        {
            //for (int i = 0; i < 10000; i++)
            //{
            //    Logger.LogError(LoggingTypeModel.LogCategory.Process, new Exception("ErrorLogger", new Exception("This is my inner exception")));
            //}
        }

        [Test, Order(2)]
        public void Read_MaxRecordCount()
        {
            //const int recordsToRead = 10;

            //var result = Logger.Read(recordsToRead);

            //Assert.AreEqual(recordsToRead, result.Count);
        }

        [Test, Order(3)]
        public void Read_ReadBetweenDates()
        {

        }

        [Test, Order(4)]
        public void Read_ReadSpecificCategory()
        {

        }

        [Test, Order(5)]
        public void Write_Transaction()
        {
            //for (int i = 0; i < 10000; i++)
            //{
            //    Logger.LogTransaction("My Source", "This is a transaction message");
            //}
        }
    }
}
