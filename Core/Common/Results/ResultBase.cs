namespace UrbanEngine.Core.Common.Results
{
    public abstract class ResultBase
    {
        public bool Success { get; protected set; }
        public string Message { get; protected set; }
    }
}
