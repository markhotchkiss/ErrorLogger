using MJH.BusinessLogic.Configuration;
using MJH.Entities;
using MJH.Interfaces;
using MJH.Models;
using System;
using System.Linq;

namespace MJH.BusinessLogic.Sql
{
    internal class LoggingSql : ILoggingWriter, ILoggingPurge
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

        public void Purge()
        {
            var config = new ConfigurationHandler().Read().Sql;

            if (config.LoggerInformation.HistoryToKeep == 0)
            {
                return;
            }

            var calculatedPurgeDate = DateTime.Now.AddDays(-config.LoggerInformation.HistoryToKeep.Value);

            var recordsToPurge = _context.Errors.Where(dt => dt.DateTimeUTC < calculatedPurgeDate).ToList();

            _context.Errors.RemoveRange(recordsToPurge);
            _context.SaveChanges();
        }
    }
}