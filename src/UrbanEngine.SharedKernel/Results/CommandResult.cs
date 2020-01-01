namespace UrbanEngine.SharedKernel.Results
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

    public class CommandResultWithData : CommandResult
    {
        public object Data { get; private set; }

        public CommandResultWithData(object data, string message, int? statusCode, bool? success)
            : base(message, statusCode, success)
        {
            Data = data;
        }
    }

    public class CommandResultWithData<T> : CommandResultWithData
    {
        public CommandResultWithData(T data, string message, int? statusCode, bool? success)
            : base(data, message, statusCode, success) { }
    }
}
