using System;

namespace UrbanEngine.Core.Common.Results
{
    public class FailureResult : ResultBase
    {
        public FailureResult(Exception ex)
        {
            Message = GetMessageToShow(ex);
            Success = false;
        }

        public FailureResult(string message)
        {
            Success = false;
            Message = message;
        }

        public static string GetMessageToShow(Exception ex)
        {
            if (ex == null)
                return "An error occurred";
            if (ex is ArgumentException || ex is ArgumentNullException)
                return "Invalid arguments were specified, please check your request and try again";
            else if (ex is NotImplementedException)
                return "The requested operation is not supported at this time";
            else
                return "An error occurred trying to process your request, please try again";
        }
    }
}
