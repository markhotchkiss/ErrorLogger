using MJH.Entities;
using MJH.Interfaces;
using MJH.Models;
using System;

namespace MJH.BusinessLogic.Sql
{
    internal class LoggingSql : ILoggingWriter
    {
        private readonly ErrorLoggerEntities _context;

        public LoggingSql()
        {
            _context = new Entities.ErrorLoggerEntities(new SqlConnectionBuilder().ConnectionString().ToString());
        }

        public bool Exists()
        {
            return _context.Database.Exists();
        }

        public void Create()
        {
            try
            {
                _context.Database.CreateIfNotExists();
                _context.SaveChanges();
            }
            catch (Exception exception)
            {

                throw;
            }

        }

        public void Write(string loggingLevel, LoggingTypeModel.LogCategory logCategory, string error, DateTime dateTime)
        {
            var sqlError = new Error
            {
                LoggingLevel = loggingLevel,
                DateTimeUTC = dateTime,
                ErrorType = logCategory.ToString(),
                Message = error
            };

            _context.Errors.Add(sqlError);
            _context.SaveChanges();
        }
    }
}