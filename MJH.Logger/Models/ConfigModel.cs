namespace MJH.Models
{

    public class LoggerConfig
    {
        /// <remarks/>
        public LoggingTypeModel.LogOutputType LoggerType { get; set; }

        public LoggingLevel LoggingLevel { get; set; }

        public LoggerConfigSql Sql { get; set; }

        public LoggerConfigSQLite SQLite { get; set; }

        public LoggerConfigText Text { get; set; }
    }

    public class LoggerConfigSql
    {
        public LoggerConfigSqlServerInformation ServerInformation { get; set; }

        public LoggerConfigSqlLoggerInformation LoggerInformation { get; set; }
    }

    public class LoggerConfigSqlServerInformation
    {
        public string Server { get; set; }

        public string Database { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }
    }

    public class LoggerConfigSqlLoggerInformation
    {
        public int? HistoryToKeep { get; set; }
    }

    public class LoggerConfigSQLite
    {
        public LoggerConfigSQLiteServerInformation ServerInformation { get; set; }

        public LoggerConfigSQLiteLoggerInformation LoggerInformation { get; set; }
    }

    public class LoggerConfigSQLiteServerInformation
    {
        public string Server { get; set; }

        public string Database { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }
    }

    public class LoggerConfigSQLiteLoggerInformation
    {
        public int? HistoryToKeep { get; set; }
    }

    public class LoggerConfigText
    {
        public LoggerConfigTextFileInformation FileInformation { get; set; }

        public LoggerConfigTextLoggerInformation LoggerInformation { get; set; }
    }

    public class LoggerConfigTextFileInformation
    {
        public string LogFileName { get; set; }

        public string LogFileLocation { get; set; }

        public string ArchiveDirectory { get; set; }
    }

    public class LoggerConfigTextLoggerInformation
    {
        public int? FileHistoryToKeep { get; set; }

        public float MaxFileSize { get; set; }
    }


}
