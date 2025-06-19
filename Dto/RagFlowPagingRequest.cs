namespace RAGFlowClient.Dto
{
    public sealed class RagFlowPagingRequest
    {
        public int Page { get; set; }
        public int PageSize { get; set; }
        public bool? Desc { get; set; }
        public string? OrderBy { get; set; }
    }
}
