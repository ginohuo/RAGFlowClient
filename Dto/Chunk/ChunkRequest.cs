namespace RAGFlowClient.Dto.Chunk
{
    public class ChunkAddRequest
    {
        public string Content { get; set; } = null!;

        public List<string>? ImportantKeywords { get; set; }

        public List<string>? Questions { get; set; }
    }

    public class ChunkUpdateRequest
    {
        public string? Content { get; set; }

        public ICollection<string>? ImportantKeywords { get; set; }

        public bool Available { get; set; }
    }
}
