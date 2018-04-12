using System.Data.SqlClient;
using MJH.Models;

namespace MJH.BusinessLogic.Sql
{
    public class SqlConnectionCreator
    {
        private readonly LoggerConfig _config;

        public SqlConnectionCreator(LoggerConfig config)
        {
            _config = config;
        }

        public SqlConnection BuildSqlConnection(Database database)
        {
            SqlConnectionStringBuilder sqlConnectionBuilder = null;
            SqlConnection sqlConnection;

            if (!IsWindowsAuth())
            {
                sqlConnectionBuilder = new SqlConnectionStringBuilder
                {
                    DataSource = _config.Sql.ServerInformation.Server,
                    InitialCatalog = DatabaseName(database),
                    UserID = _config.Sql.ServerInformation.Username,
                    Password = _config.Sql.ServerInformation.Password
                };

                sqlConnection = new SqlConnection(sqlConnectionBuilder.ConnectionString);

                return sqlConnection;
            }

            sqlConnectionBuilder = new SqlConnectionStringBuilder
            {
                DataSource = _config.Sql.ServerInformation.Server,
                InitialCatalog = DatabaseName(database),
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

        private string DatabaseName(Database database)
        {
            if (database == Database.DefaultErrorLogger)
            {
                return _config.Sql.ServerInformation.Database;
            }

            return "master";
        }
    }
}
