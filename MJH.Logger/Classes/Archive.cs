using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MJH.Interfaces;
using MJH.Models;

namespace MJH.Classes
{
    class Archive : IArchive
    {
        public string LogOutputFileLocation { get; set; }
        public string LogOutputFileName { get; set; }
        public string ArchiveLocation { get; set; }
        public LoggingLevel LoggingLevel { get; set; }

        public float CheckLogFileSize()
        {
            throw new NotImplementedException();
        }

        public void ArchiveLogFile()
        {
            throw new NotImplementedException();
        }
    }
}
