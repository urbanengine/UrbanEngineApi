namespace UrbanEngine.SharedKernel.Results
{
    public abstract class ResultBase
    {
        public bool? Success { get; protected set; }
        public string Message { get; protected set; }
        public int? StatusCode { get; protected set; }
        public abstract string ResultType { get; }

        protected ResultBase() { }

        protected ResultBase(string message, int statusCode = 200, bool success = true)
        {
            Success = success;
            StatusCode = statusCode;
            Message = message;
        }
    }
}
