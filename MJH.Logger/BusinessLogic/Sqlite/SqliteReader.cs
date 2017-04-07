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
    internal class SqliteReader : ILogReader, ILogReaderV2
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
            var command = "SELECT * FROM Error ORDER BY DateTimeUTC DESC";

            var sqliteCommand = new SQLiteCommand(command, _dbConnection);

            var reader = ExecuteSqLiteNonQuery(sqliteCommand);

            return CollectionConstructor(reader);
        }

        public IReadOnlyCollection<Error> ReadMaxRecordCount(int recordCount)
        {
            var command = "SELECT * FROM Error ORDER BY DateTimeUTC DESC LIMIT @MaxRecordCount;";

            var sqlitecommand = new SQLiteCommand(command, _dbConnection);
            sqlitecommand.Parameters.AddWithValue("@MaxRecordCount", recordCount);

            var reader = ExecuteSqLiteNonQuery(sqlitecommand);

            return CollectionConstructor(reader);
        }

        public IReadOnlyCollection<Error> ReadBetweenDates(DateTime startDate, DateTime endDate)
        {
            var command = "SELECT * FROM Error WHERE DateTimeUTC BETWEEN @StartDate AND @EndDate ORDER BY DateTimeUTC DESC";

            var sqlitecommand = new SQLiteCommand(command, _dbConnection);
            sqlitecommand.Parameters.AddWithValue("@StartDate", startDate);
            sqlitecommand.Parameters.AddWithValue("@EndDate", endDate);

            var reader = ExecuteSqLiteNonQuery(sqlitecommand);

            return CollectionConstructor(reader);
        }

        public IReadOnlyCollection<Error> ReadSpecificLevel(LoggingTypeModel.LogCategory category)
        {
            var command = "SELECT * FROM Error WHERE LoggingLevel = @LoggingLevel ORDER BY DateTimeUTC DESC";

            var sqlitecommand = new SQLiteCommand(command, _dbConnection);
            sqlitecommand.Parameters.AddWithValue("@LoggingLevel", category);

            var reader = ExecuteSqLiteNonQuery(sqlitecommand);

            return CollectionConstructor(reader);
        }

        private IReadOnlyCollection<Error> CollectionConstructor(SQLiteDataReader reader)
        {
            var errorCollection = new List<Error>();

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

        private SQLiteDataReader ExecuteSqLiteNonQuery(SQLiteCommand command)
        {
            try
            {
                _dbConnection.Open();

                var result = command.ExecuteReader();

                return result;

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

        }
    }
}