using Microsoft.Extensions.Options;
using RAGFlowClient.Dto;
using RAGFlowClient.Dto.Chunk;
using System.Dynamic;

namespace RAGFlowClient.Api
{
    public sealed class ChunkApi : RagFlowBaseApi, IChunkApi
    {
        public ChunkApi(IOptions<RagFlowOptions> options, IHttpClientFactory httpClientFactory) : base(options, httpClientFactory)
        {
        }

        public async Task<ChunkAddResult?> AddChunk(string datasetId, string documentId, ChunkAddRequest request)
        {
            var url = BuildRequestUrl($"datasets/{datasetId}/documents/{documentId}/chunks");
            var result = await PostJson<ChunkAddResponse?>(url, request);
            return result?.Chunk;
        }

        public async Task<ChunkListResponse?> ListChunks(string datasetId, string documentId, string? keywords = null, string? chunkId = null, RagFlowPagingRequest? pagingRequest = null)
        {
            var url = BuildRequestUrl($"datasets/{datasetId}/documents/{documentId}/chunks", new Dictionary<string, string?>
            {
                { "keywords", keywords }, { "id", chunkId }
            }, pagingRequest);
            return await GetJson<ChunkListResponse?>(url);
        }

        public async Task<bool> DeleteChunks(string datasetId, string documentId, List<string> chunkIdList)
        {
            var url = BuildRequestUrl($"datasets/{datasetId}/documents/{documentId}/chunks");
            dynamic request = new ExpandoObject();
            request.chunk_ids = chunkIdList;
            await DeleteJson<RagFlowResponse<string?>?>(url, request);
            return true;
        }

        public async Task<bool> UpdateChunk(string datasetId, string documentId, string chunkId, ChunkUpdateRequest request)
        {
            var url = BuildRequestUrl($"datasets/{datasetId}/documents/{documentId}/chunks/{chunkId}");
            await PutJson<RagFlowResponse<string?>?>(url, request);
            return true;
        }

        public async Task<RetrieveChunkResponse?> RetrieveChunk(RetrieveChunkRequest request)
        {
            var url = BuildRequestUrl("retrieval");
            return await PostJson<RetrieveChunkResponse?>(url, request);
        }

    }
}
