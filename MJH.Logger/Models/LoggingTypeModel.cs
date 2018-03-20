namespace MJH.Models
{
    public class LoggingTypeModel
    {
        public enum LogCategory
        {
            File,
            Process,
            Service,
            Api,
            Sdk,
            Http,
            Https,
            Sql,
            Email,
            Sms
        }

        public enum LogOutputType
        {
            TextFile,
            SQLite,
            SQL
        }
    }
}
