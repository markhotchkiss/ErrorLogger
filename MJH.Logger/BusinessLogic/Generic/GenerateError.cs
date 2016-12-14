using System;

namespace MJH.BusinessLogic.Generic
{
    internal static class GenerateError
    {
        internal static string GetException(Exception exception)
        {
            return exception.InnerException != null ?
                $"Exception: {exception.Message.Replace(',', ' ')} - InnerException {exception.InnerException.ToString().Replace(',', ' ')}" : 
                $"Exception: {exception.Message.Replace(',', ' ')}";
        }
    }
}
