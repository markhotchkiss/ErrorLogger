namespace MJH.Models
{
    public class LoggingLevelModel
    {
        public bool Debug { get; set; }
        public bool Info { get; set; }
        public bool Error { get; set; }
    }

    public enum LoggingLevel
    {
        Info,
        Debug,
        Error
    }
}