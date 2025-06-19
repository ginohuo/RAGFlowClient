namespace RAGFlowClient.Dto.Chunk
{
    public sealed class RetrieveChunkRequest
    {
        public string Question { get; set; } = null!;
        public List<string>? DatasetIds { get; set; }
        public List<string>? DocumentIds { get; set; }
        public int? Page { get; set; }
        public int? PageSize { get; set; }
        public double? SimilarityThreshold { get; set; }
        public double? VectorSimilarityWeight { get; set; }
        public int? TopK { get; set; }
        public string? RerankId { get; set; }
        public bool? Keyword { get; set; }
        public bool? Highlight { get; set; }
    }
}
