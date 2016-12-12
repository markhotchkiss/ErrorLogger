using MJH.Models;
using System;
using System.IO;
using System.Reflection;
using System.Xml;
using System.Xml.Serialization;

namespace MJH.Classes
{
    internal class ConfigurationHandler
    {
        internal LoggerConfig Read()
        {
            var serializer = new XmlSerializer(typeof(LoggerConfig));

            var location = new FileInfo(AssemblyDirectory + "\\LoggerConfig.xml");

            var config = (LoggerConfig) serializer.Deserialize(new XmlTextReader(location.FullName));

            return config;
        }

        public static string AssemblyDirectory
        {
            get
            {
                var codeBase = Assembly.GetExecutingAssembly().CodeBase;
                var uri = new UriBuilder(codeBase);
                var path = Uri.UnescapeDataString(uri.Path);
                return Path.GetDirectoryName(path);
            }
        }
    }
}
