using MJH.Entities;
using System.Collections.Generic;
using MJH.Models;

namespace MJH.Interfaces
{
    public interface ILogReader
    {
        IReadOnlyCollection<Error> Read();
    }
}
