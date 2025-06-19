namespace RAGFlowClient
{
    public class RagFlowException : Exception
    {
        public int Code { get; }
        public new string? Message { get; }

        public RagFlowException(int code, string? message) : base(message)
        {
            Code = code;
            Message = message;
        }

        public override string ToString()
        {
            return $"RagFlowException {{ Code = {Code}, Message = \"{Message}\" }}";
        }
    }
}
