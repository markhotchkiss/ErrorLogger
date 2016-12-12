using System;

namespace MJH.Classes.Generic
{
    internal static class GenerateError
    {
        internal static string GetException(Exception exception)
        {
            var errorMessage = string.Empty;

            return exception.InnerException != null ?
                $"Exception: {exception.Message} - InnerException {exception.InnerException}" : 
                $"Exception: {exception.Message}";
        }
    }
}
