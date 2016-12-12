using MJH.Models;
using System;

namespace MJH.Interfaces
{
    public interface ILoggingWriter
    {
        bool Exists();

        void Create();

        void Write(string loggingLevel, LoggingTypeModel.LogCategory logCategory, string error,
            DateTime dateTime);
    }
}
