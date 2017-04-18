using System;
using MJH.Entities;
using System.Collections.Generic;
using MJH.Models;

namespace MJH.Interfaces
{
    public interface ILogReaderV2
    {
        IReadOnlyCollection<Error> ReadMaxRecordCount(int recordCount);

        IReadOnlyCollection<Error> ReadBetweenDates(DateTime startDate, DateTime endDate);

        IReadOnlyCollection<Error> ReadSpecificCategory(LoggingTypeModel.LogCategory category);
    }
}
