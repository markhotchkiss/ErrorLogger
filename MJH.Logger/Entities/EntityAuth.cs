using System.Data.Entity;

namespace MJH.Entities
{
    public partial class ErrorLoggerEntities : DbContext
    {
        public ErrorLoggerEntities(string connectionString)
            : base(connectionString)
        {

        }
    }
}
