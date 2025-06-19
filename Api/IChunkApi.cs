using RAGFlowClient.Dto;
using RAGFlowClient.Dto.Chunk;

namespace RAGFlowClient.Api
{
    public interface IChunkApi
    {
        Task<ChunkAddResult?> AddChunk(string datasetId, string documentId, ChunkAddRequest request);
        Task<ChunkListResponse?> ListChunks(string datasetId, string documentId, string? keywords = null, string? chunkId = null, RagFlowPagingRequest? pagingRequest = null);
        Task<bool> DeleteChunks(string datasetId, string documentId, List<string> chunkIdList);
        Task<bool> UpdateChunk(string datasetId, string documentId, string chunkId, ChunkUpdateRequest request);
        Task<RetrieveChunkResponse?> RetrieveChunk(RetrieveChunkRequest request);
    }
}
