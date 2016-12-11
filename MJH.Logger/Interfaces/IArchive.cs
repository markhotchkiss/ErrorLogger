using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Build.Framework;
using MJH.Models;

namespace MJH.Interfaces
{
    public interface IArchive
    {
        string LogOutputFileLocation { get; set; }
        string LogOutputFileName { get; set; }
        string ArchiveLocation { get; set; }

        float CheckLogFileSize();

        void ArchiveLogFile();
    }
}