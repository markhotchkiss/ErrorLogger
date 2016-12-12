using MJH.Models;

namespace MJH.Classes.Generic
{
    public static class LoggingLevelEnabled
    {
        public static LoggingLevelModel Decide(LoggingLevel loggingLevel)
        {
            switch (loggingLevel)
            {
                case LoggingLevel.Info:
                    return new LoggingLevelModel
                    {
                        Debug = false,
                        Error = true,
                        Info = true
                    };
                case LoggingLevel.Error:
                    return new LoggingLevelModel
                    {
                        Debug = false,
                        Error = true,
                        Info = false
                    };
                case LoggingLevel.Debug:
                    return new LoggingLevelModel
                    {
                        Debug = true,
                        Error = true,
                        Info = true
                    };
                default:
                    return new LoggingLevelModel
                    {
                        Debug = false,
                        Error = false,
                        Info = false
                    };
            }
        }
    }
}
