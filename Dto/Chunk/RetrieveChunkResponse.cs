namespace RAGFlowClient.Dto.Chunk
{
    public sealed class RetrievedChunk
    {
        public string Content { get; set; } = null!;
        public string? ContentLtks { get; set; }
        public string DocumentId { get; set; } = null!;
        public string? DocumentKeyword { get; set; }
        public string? Highlight { get; set; }
        public string Id { get; set; } = null!;
        public string? ImageId { get; set; }
        public List<string>? ImportantKeywords { get; set; }
        public string? KbId { get; set; }
        public List<List<int>>? Positions { get; set; }
        public double? Similarity { get; set; }
        public double? TermSimilarity { get; set; }
        public double? VectorSimilarity { get; set; }
    }

    public sealed class RetrievedChunkDocAgg
    {
        public int Count { get; set; }
        public string DocId { get; set; } = null!;
        public string DocName { get; set; } = null!;
    }

    public sealed class RetrieveChunkResponse
    {
        public List<RetrievedChunk>? Chunks { get; set; }
        public List<RetrievedChunkDocAgg>? DocAggs { get; set; }
        public int Total { get; set; }
    }
}
