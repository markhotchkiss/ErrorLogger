using MJH.BusinessLogic.Configuration;
using MJH.Interfaces;
using MJH.Models;
using System;
using System.Data.SQLite;
using System.IO;

namespace MJH.BusinessLogic.Sqlite
{
    internal class LoggingSqlite : ILoggingWriter
    {
        private readonly string _dbName;
        private readonly string _dbLocation;

        private readonly SQLiteConnection _dbConnection;
        private readonly string _databasePassword = string.Empty;

        public LoggingSqlite()
        {
            var config = new ConfigurationHandler().Read();

            _dbName = config.SQLite.ServerInformation.LogFileName;
            _dbLocation = config.SQLite.ServerInformation.LogFileLocation;

            _dbConnection = new SQLiteConnection($"Data Source={_dbLocation + "\\" + _dbName};Version=3;Password={_databasePassword};");
        }

        public bool Exists()
        {
            //Check that the DB Exists.
            var dbFile = new FileInfo(ConfigurationHandler.AssemblyDirectory + "\\ErrorLog.db");

            return dbFile.Exists;
        }

        public void Create()
        {
            //Create a new DB with required tables.
            SQLiteConnection.CreateFile(_dbLocation + "\\" + _dbName);
            CreateErrorTable();
        }

        private void CreateErrorTable()
        {
            const string createCommand =
                "CREATE TABLE Error (Id INTEGER PRIMARY KEY AUTOINCREMENT, LoggingLevel nvarchar(10), ErrorType nvarchar(200), Message nvarchar(4000), DateTimeUTC DateTime)";
            ExecuteSqLiteNonQuery(createCommand);
        }

        private int ExecuteSqLiteNonQuery(string command)
        {
            _dbConnection.Open();

            var sqliteCommand = new SQLiteCommand(command, _dbConnection);
            var result = sqliteCommand.ExecuteNonQuery();
            _dbConnection.Close();

            return result;
        }

        public void Write(string loggingLevel, LoggingTypeModel.LogCategory logCategory, string error, DateTime dateTime)
        {
            ExecuteSqLiteNonQuery($"INSERT INTO Error VALUES(NULL,'{loggingLevel}','{logCategory}','{error}','{dateTime:yyyy-MM-dd HH:mm:ss}')");
        }
    }
}