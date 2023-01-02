using MJH.BusinessLogic.Configuration;
using MJH.Interfaces;
using MJH.Models;
using System;
using System.IO;
using Microsoft.Data.Sqlite;

namespace MJH.BusinessLogic.Sqlite
{
    internal class LoggingSqlite : ILoggingWriter, ILoggingPurge
    {
        private readonly string _dbName;
        private readonly string _dbLocation;

        private SqliteConnection _dbConnection;
        private readonly string _databasePassword = string.Empty;

        private readonly LoggerConfig _config;

        public LoggingSqlite()
        {
            _config = new ConfigurationHandler().Read();

            _dbName = _config.SQLite.ServerInformation.LogFileName;
            _dbLocation = _config.SQLite.ServerInformation.LogFileLocation;
        }

        public bool Exists()
        {
            //Check that the DB Exists.
            var dbFile = new FileInfo(_dbLocation + "\\" + _dbName);

            return dbFile.Exists;
        }

        public void Create()
        {
            //Create a new DB with required tables.

            using (_dbConnection =
                       new SqliteConnection(
                           $"Data Source={_dbLocation + "\\" + _dbName};Password={_databasePassword};"))
            {
                _dbConnection.Open();
            }

            CreateErrorTable();
            CreateTransactionTable();
        }

        private void CreateErrorTable()
        {
            const string createCommand =
                "CREATE TABLE Error (Id INTEGER PRIMARY KEY AUTOINCREMENT, LoggingLevel nvarchar(10), ErrorType nvarchar(200), Message nvarchar(4000), DateTimeUTC DateTime)";
            ExecuteSqLiteNonQuery(createCommand);
        }

        private void CreateTransactionTable()
        {
            const string createCommand =
                "CREATE TABLE TransactionLog (Id INTEGER PRIMARY KEY AUTOINCREMENT, DateTime datetime, SourceId nvarchar(4000), Message nvarchar(4000))";
            ExecuteSqLiteNonQuery(createCommand);
        }

        private int ExecuteSqLiteNonQuery(string command)
        {
            try
            {
                using (_dbConnection = new SqliteConnection($"Data Source={_dbLocation + "\\" + _dbName};Password={_databasePassword};"))
                {
                    _dbConnection.Open();

                    var sqliteCommand = new SqliteCommand(command, _dbConnection);

                    var result = sqliteCommand.ExecuteNonQuery();

                    _dbConnection.Close();

                    return result;
                }
            }
            catch(Exception e)
            {
                Console.WriteLine($"{DateTime.Now} - UNABLE TO ACCESS DATABASE, RECORD NOT WRITTEN.");

                return 0;
            }

        }

        public void WriteToErrorLog(string loggingLevel, LoggingTypeModel.LogCategory logCategory, string error, DateTime dateTime)
        {
            ExecuteSqLiteNonQuery($"INSERT INTO Error VALUES(NULL,'{loggingLevel}','{logCategory}','{error}','{dateTime:yyyy-MM-dd HH:mm:ss}')");
        }

        public void WriteToTransactionLog(string sourceId, string message, DateTime dateTime)
        {
            ExecuteSqLiteNonQuery(
                $"INSERT INTO TransactionLog VALUES(NULL, '{dateTime:yyyy-MM-dd HH:mm:ss}', '{sourceId}', '{message}')");
        }

        public void Purge()
        {
            if (_config.SQLite.LoggerInformation.HistoryToKeep == 0)
            {
                return;
            }

            ExecuteSqLiteNonQuery($"DELETE FROM Error WHERE DateTimeUTC < GETDATE()-{_config.SQLite.LoggerInformation.HistoryToKeep}");
        }
    }
}