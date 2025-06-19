namespace RAGFlowClient.Dto.Chunk
{
    public sealed class ChunkDto
    {
        public bool Available { get; set; }
        public string Content { get; set; } = null!;
        public string? DocnmKwd { get; set; }
        public string DocumentId { get; set; } = null!;
        public string Id { get; set; } = null!;
        public string? ImageId { get; set; }
        public List<string>? ImportantKeywords { get; set; }
        public List<List<int>>? Positions { get; set; }
    }
}
