using MJH.BusinessLogic.Configuration;
using System.Data.Entity.Core.EntityClient;
using System.Data.SqlClient;

namespace MJH.Entities
{
    internal class SqlConnectionBuilder
    {
        public EntityConnectionStringBuilder ConnectionString()
        {
            var entityConf = new ConfigurationHandler().Read().Sql.ServerInformation;

            const string providerName = "System.Data.SqlClient";
            var serverName = entityConf.Server;
            var databaseName = entityConf.Database;
            var userName = entityConf.Username;
            var password = entityConf.Password;

            // Initialize the connection string builder for the
            // underlying provider.
            var sqlBuilder = new SqlConnectionStringBuilder
            {
                DataSource = serverName,
                InitialCatalog = databaseName,
                IntegratedSecurity = false,
                UserID = userName,
                Password = password
            };

            // Build the SqlConnection connection string.
            var providerString = sqlBuilder.ToString();

            // Initialize the EntityConnectionStringBuilder.
            var entityBuilder = new EntityConnectionStringBuilder
            {
                Provider = providerName,
                ProviderConnectionString = providerString,
                Metadata =
                    @"res://*/Entities.ErrorDb.csdl|res://*/Entities.ErrorDb.ssdl|res://*/Entities.ErrorDb.msl"
            };

            return entityBuilder;
        }
    }
}
