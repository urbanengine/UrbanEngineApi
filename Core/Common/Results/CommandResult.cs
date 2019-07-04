namespace UrbanEngine.Core.Common.Results
{
    public class CommandResult : ResultBase
    {
        public override string ResultType => "Command";

        public CommandResult(string message, int? statusCode, bool? success) 
        {
            Message = message;
            StatusCode = statusCode;
            Success = success;
        }
    }
}
