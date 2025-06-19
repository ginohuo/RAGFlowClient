namespace RAGFlowClient.Dto
{
    public sealed class RagFlowResponse<T>
    {
        public int Code { get; set; }

        public string? Message { get; set; }

        public T? Data { get; set; }
    }
}
