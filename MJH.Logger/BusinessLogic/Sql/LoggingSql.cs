using MJH.BusinessLogic.Configuration;
using MJH.Interfaces;
using MJH.Models;
using System;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Diagnostics;
using System.Linq;

namespace MJH.BusinessLogic.Sql
{
    internal class LoggingSql : ILoggingWriter, ILoggingPurge
    {
        private SqlConnection _sqlConnection;
        private readonly LoggerConfig _config;

        public LoggingSql()
        {
            var handler = new ConfigurationHandler();
            _config = handler.Read();

            BuildSqlConnection();
        }

        public bool Exists()
        {
            if (!DatabaseExists())
                return false;

            if (!ErrorTableExists())
                return false;

            if (!TransactionTableExists())
                return false;


            return true;
        }

        private void BuildSqlConnection()
        {
            var sqlConnectionBuilder = new SqlConnectionStringBuilder
            {
                DataSource = _config.Sql.ServerInformation.Server,
                InitialCatalog = _config.Sql.ServerInformation.Database,
                UserID = _config.Sql.ServerInformation.Username,
                Password = _config.Sql.ServerInformation.Password
            };

            var sqlConnection = new SqlConnection(sqlConnectionBuilder.ConnectionString);

            _sqlConnection = sqlConnection;
        }

        private void BuildMasterSqlConnection()
        {
            var sqlConnectionBuilder = new SqlConnectionStringBuilder
            {
                DataSource = _config.Sql.ServerInformation.Server,
                InitialCatalog = "master",
                UserID = _config.Sql.ServerInformation.Username,
                Password = _config.Sql.ServerInformation.Password
            };

            var sqlConnection = new SqlConnection(sqlConnectionBuilder.ConnectionString);

            _sqlConnection = sqlConnection;
        }

        public void Create()
        {
            if (!DatabaseExists())
            {
                using (_sqlConnection)
                {
                    BuildMasterSqlConnection();

                    _sqlConnection.Open();

                    var sqlCommand = new SqlCommand($"CREATE DATABASE {_config.Sql.ServerInformation.Database}", _sqlConnection);

                    sqlCommand.ExecuteNonQuery();

                    _sqlConnection.Close();
                }
            }

            if (!ErrorTableExists())
            {
                using (_sqlConnection)
                {
                    BuildSqlConnection();

                    _sqlConnection.Open();

                    var createSqlCommand = new SqlCommand(
                        "CREATE TABLE [dbo].[Error] ([Id] int IDENTITY(1, 1) NOT NULL, [LoggingLevel] nvarchar(10)  NULL, [ErrorType] nvarchar(200)  NULL, [Message] nvarchar(4000)  NULL, [DateTimeUTC] datetime  NULL); ",
                        _sqlConnection);

                    createSqlCommand.ExecuteNonQuery();

                    createSqlCommand =
                        new SqlCommand(
                            "ALTER TABLE [dbo].[Error] ADD CONSTRAINT [PK_Errors] PRIMARY KEY CLUSTERED([Id] ASC); ",
                            _sqlConnection);

                    createSqlCommand.ExecuteNonQuery();

                    _sqlConnection.Close();
                }
               
            }

            if (!TransactionTableExists())
            {
                using (_sqlConnection)
                {
                    BuildSqlConnection();

                    _sqlConnection.Open();

                    var sqlCommand =
                        new SqlCommand(
                            "CREATE TABLE [dbo].[Transaction] ([Id] int IDENTITY(1, 1) NOT NULL, [DateTime] datetime  NULL, [SourceId] nvarchar(max)  NULL, [Message] nvarchar(max)  NULL); ",
                            _sqlConnection);

                    sqlCommand.ExecuteNonQuery();

                    sqlCommand =
                        new SqlCommand(
                            "ALTER TABLE [dbo].[Transaction] ADD CONSTRAINT[PK_Transaction] PRIMARY KEY CLUSTERED([Id] ASC); ",
                            _sqlConnection);
                    sqlCommand.ExecuteNonQuery();

                    _sqlConnection.Close();
                }
            }

            if(_sqlConnection.State == ConnectionState.Open)
                _sqlConnection.Close();
        }

        private bool DatabaseExists()
        {
            BuildMasterSqlConnection();
            var sqlCommand = new SqlCommand("if db_id('ErrorLogger') is not null SELECT 'true' as Result ELSE SELECT 'false' as Result",
                _sqlConnection);

            using (_sqlConnection)
            {
                _sqlConnection.Open();

                using (var dbExists = sqlCommand.ExecuteReader())
                {
                    while (dbExists.Read())
                    {
                        if (!Convert.ToBoolean(dbExists["Result"]))
                        {
                            return false;
                        }
                    }
                }

                _sqlConnection.Close();
            }

            return true;
        }

        private bool ErrorTableExists()
        {
            BuildSqlConnection();
            using (_sqlConnection)
            {
                var sqlCommand = new SqlCommand("IF object_id('dbo.Error') is not null SELECT 'true' as Result ELSE SELECT 'false' as Result",
                    _sqlConnection);

                _sqlConnection.Open();

                using (var errorTableExists = sqlCommand.ExecuteReader())
                {
                    while (errorTableExists.Read())
                    {
                        if (!Convert.ToBoolean(errorTableExists["Result"]))
                        {
                            return false;
                        }
                    }
                }

                _sqlConnection.Close();
            }

            return true;
        }

        private bool TransactionTableExists()
        {
            BuildSqlConnection();
            using (_sqlConnection)
            {
                _sqlConnection.Open();

                var sqlCommand = new SqlCommand("IF object_id('dbo.Transaction') is not null SELECT 'true' as Result ELSE SELECT 'false' as Result",
                    _sqlConnection);

                using (var transactionTableExists = sqlCommand.ExecuteReader())
                {
                    while (transactionTableExists.Read())
                    {
                        if (!Convert.ToBoolean(transactionTableExists["Result"]))
                        {
                            return false;
                        }
                    }
                }

                _sqlConnection.Close();
            }

            return true;
        }

        public void Write(string loggingLevel, LoggingTypeModel.LogCategory logCategory, string error, DateTime dateTime)
        {
            var sqlError = new Error
            {
                LoggingLevel = loggingLevel,
                DateTimeUTC = dateTime,
                ErrorType = logCategory.ToString(),
                Message = error
            };

            BuildSqlConnection();

            var sqlCommand =
                new SqlCommand("INSERT INTO dbo.Error VALUES (@LoggingLevel, @ErrorType, @Message, @DateTimeUTC)",
                    _sqlConnection);
            sqlCommand.Parameters.AddWithValue("@LoggingLevel", sqlError.LoggingLevel);
            sqlCommand.Parameters.AddWithValue("@ErrorType", sqlError.ErrorType);
            sqlCommand.Parameters.AddWithValue("@Message", sqlError.Message);
            sqlCommand.Parameters.AddWithValue("@DateTimeUTC", sqlError.DateTimeUTC.ToUniversalTime());

            using (_sqlConnection)
            {
                _sqlConnection.Open();
                sqlCommand.ExecuteNonQuery();
                _sqlConnection.Close();
            }
        }

        public void Purge()
        {
            var config = new ConfigurationHandler().Read().Sql;

            if (config.LoggerInformation.HistoryToKeep == 0)
            {
                return;
            }

            var calculatedPurgeDate = DateTime.Now.AddDays(-config.LoggerInformation.HistoryToKeep.Value);

            var sqlCommand = new SqlCommand("DELETE FROM dbo.Error WHERE [DateTimeUTC] < @history");
            sqlCommand.Parameters.AddWithValue("@history", calculatedPurgeDate);

            BuildSqlConnection();

            _sqlConnection.Open();
            sqlCommand.ExecuteNonQuery();
            _sqlConnection.Close();
        }

        public void Write(DateTime logDateTime, string sourceId, string logMessage)
        {
            BuildSqlConnection();

            using (_sqlConnection)
            {
                _sqlConnection.Open();

                var sqlCommand = new SqlCommand("INSERT INTO dbo.[Transaction] VALUES (@DateTime, @SourceId, @Message)", _sqlConnection);
                sqlCommand.Parameters.AddWithValue("@DateTime", DateTime.Now.ToUniversalTime());
                sqlCommand.Parameters.AddWithValue("@SourceId", sourceId);
                sqlCommand.Parameters.AddWithValue("@Message", logMessage);

                sqlCommand.ExecuteNonQuery();

                _sqlConnection.Close();
            }
        }
    }
}