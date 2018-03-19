using MJH.BusinessLogic.Configuration;
using MJH.Entities;
using MJH.Interfaces;
using MJH.Models;
using System;
using System.Data.SqlClient;
using System.Linq;

namespace MJH.BusinessLogic.Sql
{
    internal class LoggingSql : ILoggingWriter, ILoggingPurge
    {
        private SqlConnection _sqlConnection;
        private LoggerConfig _config;

        public LoggingSql()
        {
            var handler = new ConfigurationHandler();
            _config = handler.Read();

            CreateSqlConnection();
        }

        public bool Exists()
        {
            _config = new ConfigurationHandler().Read();

            _sqlConnection = BuildSqlConnection();

            var sqlCommand = new SqlCommand("if db_id('ErrorLogger') is not null SELECT 'true' ELSE SELECT 'false'",
                _sqlConnection);

            var dbExists = sqlCommand.ExecuteReader();

            if (!Convert.ToBoolean(dbExists[0]))
            {
                return false;
            }

            sqlCommand = new SqlCommand("IF object_id('dbo.Error') is not null SELECT 'true' ELSE SELECT 'false'",
                _sqlConnection);

            var errorTableExists = sqlCommand.ExecuteReader();

            if (!Convert.ToBoolean(errorTableExists[0]))
            {
                return false;
            }

            sqlCommand = new SqlCommand("IF object_id('dbo.Transaction') is not null SELECT 'true' ELSE SELECT 'false'",
                _sqlConnection);

            var transactionTableExists = sqlCommand.ExecuteReader();

            if (!Convert.ToBoolean(transactionTableExists[0]))
            {
                return false;
            }

            return true;
        }

        private SqlConnection BuildSqlConnection()
        {
            var sqlConnectionBuilder = new SqlConnectionStringBuilder
            {
                DataSource = _config.Sql.ServerInformation.Server,
                InitialCatalog = _config.Sql.ServerInformation.Database,
                UserID = _config.Sql.ServerInformation.Username,
                Password = _config.Sql.ServerInformation.Password
            };

            var sqlConnection = new SqlConnection(sqlConnectionBuilder.ConnectionString);

            return sqlConnection;
        }

        public void Create()
        {
            _config = new ConfigurationHandler().Read();

            _sqlConnection = BuildSqlConnection();

            var sqlCommand = new SqlCommand("if db_id('ErrorLogger') is not null SELECT 'true' ELSE SELECT 'false'",
                _sqlConnection);

            using (_sqlConnection)
            {
                _sqlConnection.Open();

                var dbExists = sqlCommand.ExecuteReader();

                if (!Convert.ToBoolean(dbExists[0]))
                {
                    sqlCommand = new SqlCommand("CREATE DATABASE @DatabaseName", _sqlConnection);
                    sqlCommand.Parameters.AddWithValue("@DatabaseName", _config.Sql.ServerInformation.Database);

                    sqlCommand.ExecuteNonQuery();
                }

                sqlCommand = new SqlCommand("IF object_id('dbo.Error') is not null SELECT 'true' ELSE SELECT 'false'",
                    _sqlConnection);

                var errorTableExists = sqlCommand.ExecuteReader();

                if (!Convert.ToBoolean(errorTableExists[0]))
                {
                    sqlCommand = new SqlCommand("CREATE TABLE [dbo].[Errors] ([Id] int IDENTITY(1, 1) NOT NULL, [LoggingLevel] nvarchar(10)  NULL, [ErrorType] nvarchar(200)  NULL, [Message] nvarchar(4000)  NULL, [DateTimeUTC] datetime  NULL); ", _sqlConnection);

                    sqlCommand.ExecuteNonQuery();

                    sqlCommand = new SqlCommand("ALTER TABLE [dbo].[Errors] ADD CONSTRAINT [PK_Errors] PRIMARY KEY CLUSTERED([Id] ASC); ", _sqlConnection);

                    sqlCommand.ExecuteNonQuery();

                }

                sqlCommand = new SqlCommand("IF object_id('dbo.Transaction') is not null SELECT 'true' ELSE SELECT 'false'",
                    _sqlConnection);

                var transactionTableExists = sqlCommand.ExecuteReader();

                if (!Convert.ToBoolean(transactionTableExists[0]))
                {
                    sqlCommand = new SqlCommand("CREATE TABLE [dbo].[Transactions] ([Id] int IDENTITY(1, 1) NOT NULL, [DateTime] datetime  NULL, [SourceId] nvarchar(max)  NULL, [Message] nvarchar(max)  NULL); ", _sqlConnection);

                    sqlCommand.ExecuteNonQuery();

                    sqlCommand = new SqlCommand("ALTER TABLE [dbo].[Transactions] ADD CONSTRAINT[PK_Transactions] PRIMARY KEY CLUSTERED([Id] ASC); ", _sqlConnection);
                    sqlCommand.ExecuteNonQuery();
                }

                _sqlConnection.Close();
            }
        }

        private void CreateSqlConnection()
        {
            var sqlConnectionStringBuilder = new SqlConnectionStringBuilder
            {
                DataSource = _config.Sql.ServerInformation.Server,
                InitialCatalog = _config.Sql.ServerInformation.Database,
                UserID = _config.Sql.ServerInformation.Username,
                Password = _config.Sql.ServerInformation.Password
            };

            var sqlConnection = new SqlConnection(sqlConnectionStringBuilder.ConnectionString);

            _sqlConnection = sqlConnection;
        }

        public void Write(string loggingLevel, LoggingTypeModel.LogCategory logCategory, string error,
            DateTime dateTime)
        {
            var sqlError = new Error
            {
                LoggingLevel = loggingLevel,
                DateTimeUTC = dateTime,
                ErrorType = logCategory.ToString(),
                Message = error
            };

            var sqlCommand =
                new SqlCommand("INSERT INTO dbo.Error VALUES (@LoggingLevel, @ErrorType, @Message, @DateTimeUTC)",
                    _sqlConnection);
            sqlCommand.Parameters.AddWithValue("@LoggingLevel", sqlError.LoggingLevel);
            sqlCommand.Parameters.AddWithValue("@ErrorType", sqlError.ErrorType);
            sqlCommand.Parameters.AddWithValue("@Message", sqlError.Message);
            sqlCommand.Parameters.AddWithValue("@DateTimeUTC", sqlError.DateTimeUTC);

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

            _sqlConnection.Open();
            sqlCommand.ExecuteNonQuery();
            _sqlConnection.Close();
        }

        public void Write(DateTime logDateTime, string sourceId, string logMessage)
        {
            using (_sqlConnection)
            {
                _sqlConnection.Open();

                var sqlCommand = new SqlCommand("INSERT INTO dbo.Transaction VALUES (GETDATE(), @SourceId, @Message)");
                sqlCommand.Parameters.AddWithValue("@SourceId", sourceId);
                sqlCommand.Parameters.AddWithValue("@Message", logMessage);

                sqlCommand.ExecuteNonQuery();

                _sqlConnection.Close();
            }
        }
    }
}