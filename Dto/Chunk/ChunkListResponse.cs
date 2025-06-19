using RAGFlowClient.Dto.Document;

namespace RAGFlowClient.Dto.Chunk
{
    public sealed class ChunkListResponse
    {
        public ICollection<ChunkDto>? Chunks { get; set; }

        public DocumentDto? Doc { get; set; }

        public int Total { get; set; }
    }
}
