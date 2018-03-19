using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MJH.Interfaces
{
    public interface ITransaction
    {
        void LogTransaction(DateTime logDateTime, string sourceId, string logMessage);
    }
}
