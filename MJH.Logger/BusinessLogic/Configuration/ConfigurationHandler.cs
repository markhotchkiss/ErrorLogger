using MJH.Interfaces;
using MJH.Models;
using System;
using System.IO;
using System.Reflection;
using System.Xml;
using System.Xml.Serialization;

namespace MJH.BusinessLogic.Configuration
{
    public class ConfigurationHandler : IConfiguration, IConfigurationV2
    {
        public LoggerConfig Read()
        {
            var serializer = new XmlSerializer(typeof(LoggerConfig));

            var location = new FileInfo(AssemblyDirectory + "\\LoggerConfig.xml");

            var reader = new XmlTextReader(location.FullName);

            var config = (LoggerConfig) serializer.Deserialize(reader);

            reader.Close();

            return config;
        }

        internal static string AssemblyDirectory
        {
            get
            {
                var codeBase = Assembly.GetExecutingAssembly().CodeBase;
                var uri = new UriBuilder(codeBase);
                var path = Uri.UnescapeDataString(uri.Path);
                return Path.GetDirectoryName(path);
            }
        }

        public bool Write(LoggerConfig config)
        {
            var writer = new StreamWriter(AssemblyDirectory + "\\LoggerConfig.xml", false);

            var em = new XmlSerializer(config.GetType());

            em.Serialize(writer, config);
            writer.Close();

            return true;
        }
    }
}