using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MJH.Interfaces
{
    public interface ITransactionWriter
    {
        void Write(DateTime logDateTime, string sourceId, string logMessage);
    }
}
