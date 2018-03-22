using System.Data.SqlClient;
using MJH.Models;

namespace MJH.BusinessLogic.Sql
{
    internal class SqlConnectionCreator
    {
        private readonly LoggerConfig _config;

        public SqlConnectionCreator(LoggerConfig config)
        {
            _config = config;
        }

        internal SqlConnection BuildSqlConnection()
        {
            SqlConnectionStringBuilder sqlConnectionBuilder = null;
            SqlConnection sqlConnection;

            if (!IsWindowsAuth())
            {
                sqlConnectionBuilder = new SqlConnectionStringBuilder
                {
                    DataSource = _config.Sql.ServerInformation.Server,
                    InitialCatalog = _config.Sql.ServerInformation.Database,
                    UserID = _config.Sql.ServerInformation.Username,
                    Password = _config.Sql.ServerInformation.Password
                };

                sqlConnection = new SqlConnection(sqlConnectionBuilder.ConnectionString);

                return sqlConnection;
            }

            sqlConnectionBuilder = new SqlConnectionStringBuilder
            {
                DataSource = _config.Sql.ServerInformation.Server,
                InitialCatalog = _config.Sql.ServerInformation.Database,
                IntegratedSecurity = true
            };

            sqlConnection = new SqlConnection(sqlConnectionBuilder.ConnectionString);

            return sqlConnection;
        }

        private bool IsWindowsAuth()
        {
            if (_config.Sql.ServerInformation.Username == "" && _config.Sql.ServerInformation.Password == "")
            {
                return true;
            }

            return false;
        }
    }
}
