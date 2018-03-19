using System;
using MJH.Interfaces;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using MJH.Models;

namespace MJH.BusinessLogic.Sql
{
    internal class SqlReader : ILogReader, ILogReaderV2
    {
        public SqlReader()
        {
            
        }

        public IReadOnlyCollection<Error> Read()
        {
            return new ObservableCollection<Error>();
        }

        public IReadOnlyCollection<Error> ReadMaxRecordCount(int recordCount)
        {
            return new ObservableCollection<Error>();
        }

        public IReadOnlyCollection<Error> ReadBetweenDates(DateTime startDate, DateTime endDate)
        {
            return new ObservableCollection<Error>();
        }

        public IReadOnlyCollection<Error> ReadSpecificCategory(LoggingTypeModel.LogCategory category)
        {
            return new ObservableCollection<Error>();
        }
    }
}
