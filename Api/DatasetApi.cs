using Microsoft.Extensions.Options;
using RAGFlowClient.Dto.Dataset;
using RAGFlowClient.Dto;
using System.Dynamic;

namespace RAGFlowClient.Api
{
    public sealed class DatasetApi : RagFlowBaseApi, IDatasetApi
    {
        public DatasetApi(IOptions<RagFlowOptions> options, IHttpClientFactory httpClientFactory) : base(options, httpClientFactory)
        {
        }

        public async Task<DatasetDto?> CreateDataset(string datasetName)
        {
            return await CreateDataset(new DatasetRequest { Name = datasetName });
        }

        public async Task<DatasetDto?> CreateDataset(DatasetRequest request)
        {
            var url = BuildRequestUrl("datasets");
            return await PostJson<DatasetDto?>(url, request);
        }

        public async Task<bool> DeleteDatasets(List<string> datasetIds)
        {
            var url = BuildRequestUrl("datasets");
            dynamic request = new ExpandoObject();
            request.ids = datasetIds;
            await DeleteJson<RagFlowResponse<string?>?>(url, request);
            return true;
        }

        public async Task<bool> UpdateDataset(string datasetId, DatasetRequest request)
        {
            var url = BuildRequestUrl($"datasets/{datasetId}");
            await PutJson<RagFlowResponse<string?>?>(url, request);
            return true;
        }

        public async Task<List<DatasetDto>?> ListDataset(string? id = null, string? name = null, RagFlowPagingRequest? pagingRequest = null)
        {
            var url = BuildRequestUrl("datasets", new Dictionary<string, string?>
            {
                { "id", id }, { "name", name }
            }, pagingRequest);
            return await GetJson<List<DatasetDto>?>(url);
        }
    }
}
