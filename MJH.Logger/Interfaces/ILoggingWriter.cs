using MJH.Models;
using System;

namespace MJH.Interfaces
{
    public interface ILoggingWriter
    {
        bool Exists();

        void Create();

        void WriteToErrorLog(string loggingLevel, LoggingTypeModel.LogCategory logCategory, string error,
            DateTime dateTime);
    }
}
