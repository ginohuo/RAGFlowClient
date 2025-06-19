using RAGFlowClient.Dto;
using RAGFlowClient.Dto.Document;

namespace RAGFlowClient.Api
{
    public interface IDocumentApi
    {
        Task<List<DocumentSummaryDto>?> UploadDocuments(string datasetId, List<string> filePathList);
        Task<bool> DownloadDocument(string datasetId, string documentId, string filePathNameForSave);
        Task<byte[]?> DownloadDocument(string datasetId, string documentId);
        Task<DocumentListResponse?> ListDocuments(string datasetId, string? keywords = null, string? id = null, string? name = null, RagFlowPagingRequest? pagingRequest = null);
        Task<bool> DeleteDocuments(string datasetId, List<string> documentIdList);
        Task<bool> ParseDocuments(string datasetId, List<string> documentIdList);
        Task<bool> StopParsingDocuments(string datasetId, List<string> documentIdList);
    }
}
