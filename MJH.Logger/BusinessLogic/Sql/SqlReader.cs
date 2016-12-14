using MJH.Entities;
using MJH.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace MJH.BusinessLogic.Sql
{
    internal class SqlReader : ILogReader
    {
        public IReadOnlyCollection<Error> Read()
        {
            var context = new ErrorLoggerEntities(new SqlConnectionBuilder().ConnectionString().ToString());

            var logs = context.Errors.ToList();

            return logs;
        }
    }
}
