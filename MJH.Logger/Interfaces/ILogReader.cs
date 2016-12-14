using MJH.Entities;
using System.Collections.Generic;

namespace MJH.Interfaces
{
    public interface ILogReader
    {
        IReadOnlyCollection<Error> Read();
    }
}
