using System;

namespace MJH.BusinessLogic.Generic
{
    internal static class GenerateError
    {
        internal static string GetException(Exception exception)
        {
            return exception.InnerException != null ?
                $"Exception: {exception.Message} - InnerException {exception.InnerException}" : 
                $"Exception: {exception.Message}";
        }
    }
}
