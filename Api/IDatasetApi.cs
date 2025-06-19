using RAGFlowClient.Dto;
using RAGFlowClient.Dto.Dataset;

namespace RAGFlowClient.Api
{
    public interface IDatasetApi
    {
        Task<DatasetDto?> CreateDataset(string datasetName);
        Task<DatasetDto?> CreateDataset(DatasetRequest request);
        Task<bool> DeleteDatasets(List<string> datasetIds);
        Task<bool> UpdateDataset(string datasetId, DatasetRequest request);
        Task<List<DatasetDto>?> ListDataset(string? id = null, string? name = null, RagFlowPagingRequest? pagingRequest = null);
    }
}
