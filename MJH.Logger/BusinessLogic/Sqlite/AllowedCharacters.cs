using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MJH.BusinessLogic.Sqlite
{
    public static class AllowedCharacters
    {
        public static string Replace(string message)
        {
            return message.Replace("'", "''");
        }
    }
}
