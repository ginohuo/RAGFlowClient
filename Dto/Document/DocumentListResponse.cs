namespace RAGFlowClient.Dto.Document
{
    public sealed class DocumentListResponse
    {
        public List<DocumentDto>? Docs { get; set; }

        public int Total { get; set; }
    }
}
