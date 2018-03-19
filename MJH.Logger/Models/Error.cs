using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MJH.Models
{
    public class Error
    {
        public string LoggingLevel { get; set; }

        public string ErrorType { get; set; }

        public string Message { get; set; }

        public DateTime DateTimeUTC { get; set; }
    }
}
