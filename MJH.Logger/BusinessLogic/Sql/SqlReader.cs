using System;
using MJH.Entities;
using MJH.Interfaces;
using System.Collections.Generic;
using System.Linq;
using MJH.Models;

namespace MJH.BusinessLogic.Sql
{
    internal class SqlReader : ILogReader, ILogReaderV2
    {
        private ErrorLoggerEntities _context;

        public SqlReader()
        {
            _context = new ErrorLoggerEntities(new SqlConnectionBuilder().ConnectionString().ToString());
        }

        public IReadOnlyCollection<Error> Read()
        {
            var logs = _context.Errors.ToList();

            return logs;
        }

        public IReadOnlyCollection<Error> ReadMaxRecordCount(int recordCount)
        {
            var logs = _context.Errors.Take(recordCount).OrderByDescending(e => e.DateTimeUTC).ToList();

            return logs;
        }

        public IReadOnlyCollection<Error> ReadBetweenDates(DateTime startDate, DateTime endDate)
        {
            var logs = from e in _context.Errors
                where e.DateTimeUTC >= startDate
                      && e.DateTimeUTC <= endDate
                      orderby e.DateTimeUTC descending 
                select e;

            return logs.ToList();
        }

        public IReadOnlyCollection<Error> ReadSpecificCategory(LoggingTypeModel.LogCategory category)
        {
            var logs = from e in _context.Errors
                where e.ErrorType == category.ToString()
                select e;

            return logs.ToList();
        }
    }
}
