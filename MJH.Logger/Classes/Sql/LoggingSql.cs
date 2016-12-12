using MJH.Interfaces;
using MJH.Models;
using System;

namespace MJH.Classes.Sql
{
    internal class LoggingSql : ILoggingWriter
    {
        public bool Exists()
        {
            throw new NotImplementedException();
        }

        public void Create()
        {
            throw new NotImplementedException();
        }

        public void Write(string loggingLevel, LoggingTypeModel.LogCategory logCategory, string error, DateTime dateTime)
        {
            throw new NotImplementedException();
        }
    }
}
