using MJH.BusinessLogic.Configuration;
using MJH.Entities;
using MJH.Interfaces;
using MJH.Models;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Globalization;

namespace MJH.BusinessLogic.Sqlite
{
    internal class SqliteReader : ILogReader
    {
        private readonly string _dbName;
        private readonly string _dbLocation;

        private readonly SQLiteConnection _dbConnection;
        private readonly string _databasePassword = string.Empty;

        private readonly LoggerConfig _config;

        public SqliteReader()
        {
            _config = new ConfigurationHandler().Read();

            _dbName = _config.SQLite.ServerInformation.LogFileName;
            _dbLocation = _config.SQLite.ServerInformation.LogFileLocation;

            _dbConnection = new SQLiteConnection($"Data Source={_dbLocation + "\\" + _dbName};Version=3;Password={_databasePassword};");
        }

        public IReadOnlyCollection<Error> Read()
        {
            var errorCollection = new List<Error>();

            var command = "SELECT * FROM Error";

            var reader = ExecuteSqLiteNonQuery(command);

            while (reader.Read())
            {
                DateTime dateTime;
                DateTime.TryParseExact(reader["DateTimeUTC"].ToString(), "dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture, DateTimeStyles.None,
                    out dateTime);

                var error = new Error
                {
                    LoggingLevel = reader["LoggingLevel"].ToString(),
                    ErrorType = reader["ErrorType"].ToString(),
                    Message = reader["Message"].ToString(),
                    DateTimeUTC = dateTime
                };

                errorCollection.Add(error);
            }

            _dbConnection.Close();

            return errorCollection;
        }

        private SQLiteDataReader ExecuteSqLiteNonQuery(string command)
        {
            _dbConnection.Open();

            var sqliteCommand = new SQLiteCommand(command, _dbConnection);
            var result = sqliteCommand.ExecuteReader();
            return result;
        }
    }
}
