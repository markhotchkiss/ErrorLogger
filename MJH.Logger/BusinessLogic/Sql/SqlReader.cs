using System;
using MJH.Interfaces;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using MJH.BusinessLogic.Configuration;
using MJH.Models;

namespace MJH.BusinessLogic.Sql
{
    internal class SqlReader : ILogReader, ILogReaderV2
    {
        private readonly LoggerConfig _config;
        private SqlConnection _sqlConnection;

        public SqlReader()
        {
            var configHandler = new ConfigurationHandler();
            _config = configHandler.Read();

            BuildSqlConnection();
        }

        private void BuildSqlConnection()
        {
            _sqlConnection = new SqlConnectionCreator(_config).BuildSqlConnection();
        }

        public IReadOnlyCollection<Error> Read()
        {
            BuildSqlConnection();

            var errorLog = new List<Error>();

            var sqlCommand = new SqlCommand("SELECT * FROM dbo.Error", _sqlConnection);

            var reader = sqlCommand.ExecuteReader();

            while (reader.Read())
            {
                errorLog.Add(new Error
                {
                    LoggingLevel = reader["LoggingLevel"].ToString(),
                    Message = reader["Message"].ToString(),
                    DateTimeUTC = Convert.ToDateTime(reader["DateTimeUTC"]),
                    ErrorType = reader["ErrorType"].ToString()
                });
            }

            return errorLog;
        }

        public IReadOnlyCollection<Error> ReadMaxRecordCount(int recordCount)
        {
            BuildSqlConnection();

            var errorLog = new List<Error>();

            _sqlConnection.Open();

            var sqlCommand = new SqlCommand($"SELECT TOP {recordCount} * FROM dbo.Error ORDER BY DateTimeUTC DESC", _sqlConnection);

            var reader = sqlCommand.ExecuteReader();

            while (reader.Read())
            {
                errorLog.Add(new Error
                {
                    LoggingLevel = reader["LoggingLevel"].ToString(),
                    Message = reader["Message"].ToString(),
                    DateTimeUTC = Convert.ToDateTime(reader["DateTimeUTC"]),
                    ErrorType = reader["ErrorType"].ToString()
                });
            }

            _sqlConnection.Close();

            return errorLog;
        }

        public IReadOnlyCollection<Error> ReadBetweenDates(DateTime startDate, DateTime endDate)
        {
            BuildSqlConnection();

            var errorLog = new List<Error>();

            var sqlCommand = new SqlCommand("SELECT * FROM dbo.Error WHERE DateTimeUTC BETWEEN @StartDate AND @EndDate", _sqlConnection);
            sqlCommand.Parameters.AddWithValue("@StartDate", startDate);
            sqlCommand.Parameters.AddWithValue("@EndDate", endDate);

            var reader = sqlCommand.ExecuteReader();

            while (reader.Read())
            {
                errorLog.Add(new Error
                {
                    LoggingLevel = reader["LoggingLevel"].ToString(),
                    Message = reader["Message"].ToString(),
                    DateTimeUTC = Convert.ToDateTime(reader["DateTimeUTC"]),
                    ErrorType = reader["ErrorType"].ToString()
                });
            }

            return errorLog;
        }

        public IReadOnlyCollection<Error> ReadSpecificCategory(LoggingTypeModel.LogCategory category)
        {
            BuildSqlConnection();

            _sqlConnection.Open();

            var errorLog = new List<Error>();

            var sqlCommand = new SqlCommand("SELECT * FROM dbo.Error WHERE ErrorType = @category", _sqlConnection);
            sqlCommand.Parameters.AddWithValue("@category", category);

            var reader = sqlCommand.ExecuteReader();

            while (reader.Read())
            {
                errorLog.Add(new Error
                {
                    LoggingLevel = reader["LoggingLevel"].ToString(),
                    Message = reader["Message"].ToString(),
                    DateTimeUTC = Convert.ToDateTime(reader["DateTimeUTC"]),
                    ErrorType = reader["ErrorType"].ToString()
                });
            }

            _sqlConnection.Close();

            return errorLog;
        }
    }
}
