This project can help you interact with ragflow(https://github.com/infiniflow/ragflow) using the API method. Tested with ragflow v0.19.0.

how to use it:
Install-Package RAGFlowClient

Sample code for console application:
```
var host = Host.CreateDefaultBuilder().ConfigureServices((context, services) =>
              {
                  services.AddRagFlowService(options =>
                  {
                      options.ApiKey = "ragflow-XXXX";
                      options.BaseUrl = "http://xxx.xxx.com:<port>";
                  });
              }).Build();
var ragFlowApi = host.Services.GetRequiredService<RagFlowApi>();
ragFlowApi.Dataset.CreateDataset("<datasetname>");

```

Exception: Will throw RagFlowException when the API call failed.

Currently, support the following methods:
1. Dataset Api
```
Task<DatasetDto?> CreateDataset(string datasetName);
Task<DatasetDto?> CreateDataset(DatasetRequest request);
Task<bool> DeleteDatasets(List<string> datasetIds);
Task<bool> UpdateDataset(string datasetId, DatasetRequest request);
Task<List<DatasetDto>?> ListDataset(string? id = null, string? name = null, RagFlowPagingRequest? pagingRequest = null);
```
2. Document Api
```
Task<List<DocumentSummaryDto>?> UploadDocuments(string datasetId, List<string> filePathList);
Task<bool> DownloadDocument(string datasetId, string documentId, string filePathNameForSave);
Task<byte[]?> DownloadDocument(string datasetId, string documentId);
Task<DocumentListResponse?> ListDocuments(string datasetId, string? keywords = null, string? id = null, string? name = null, RagFlowPagingRequest? pagingRequest = null);
Task<bool> DeleteDocuments(string datasetId, List<string> documentIdList);
Task<bool> ParseDocuments(string datasetId, List<string> documentIdList);
Task<bool> StopParsingDocuments(string datasetId, List<string> documentIdList);
```

3 . Chunk Api
```
Task<ChunkAddResult?> AddChunk(string datasetId, string documentId, ChunkAddRequest request);
Task<ChunkListResponse?> ListChunks(string datasetId, string documentId, string? keywords = null, string? chunkId = null, RagFlowPagingRequest? pagingRequest = null);
Task<bool> DeleteChunks(string datasetId, string documentId, List<string> chunkIdList);
Task<bool> UpdateChunk(string datasetId, string documentId, string chunkId, ChunkUpdateRequest request);
Task<RetrieveChunkResponse?> RetrieveChunk(RetrieveChunkRequest request);
```
