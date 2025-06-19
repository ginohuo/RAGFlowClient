namespace RAGFlowClient.Dto.Chunk
{
    public sealed class ChunkAddResult
    {
        public string Content { get; set; } = null!;
        public double CreateTimestamp { get; set; }
        public string CreateTime { get; set; } = null!;
        public string DatasetId { get; set; } = null!;
        public string DocumentId { get; set; } = null!;
        public string Id { get; set; } = null!;
        public List<string>? ImportantKeywords { get; set; }
        public List<string>? Questions { get; set; }
    }

    public sealed class ChunkAddResponse
    {
        public ChunkAddResult? Chunk { get; set; }
    }
}
