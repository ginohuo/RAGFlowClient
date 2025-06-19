using System.Dynamic;
using Microsoft.Extensions.Options;
using RAGFlowClient.Dto;
using RAGFlowClient.Dto.Document;

namespace RAGFlowClient.Api
{
    public sealed class DocumentApi : RagFlowBaseApi, IDocumentApi
    {
        public DocumentApi(IOptions<RagFlowOptions> options, IHttpClientFactory httpClientFactory) : base(options, httpClientFactory)
        {
        }

        public async Task<List<DocumentSummaryDto>?> UploadDocuments(string datasetId, List<string> filePathList)
        {
            var url = BuildRequestUrl($"datasets/{datasetId}/documents");
            return await PostFiles<List<DocumentSummaryDto>?>(url, filePathList);
        }

        public async Task<bool> DownloadDocument(string datasetId, string documentId, string filePathNameForSave)
        {
            var fileBytes = await DownloadDocument(datasetId, documentId);
            if (fileBytes == null)
                return false;
            var fileSize = fileBytes.Length;
            using (var fs = File.OpenWrite(filePathNameForSave))
            {
                fs.Write(fileBytes, 0, fileSize);
            }
            return true;
        }

        public async Task<byte[]?> DownloadDocument(string datasetId, string documentId)
        {
            var url = BuildRequestUrl($"datasets/{datasetId}/documents/{documentId}");
            return await DownloadFile(url);
        }

        public async Task<DocumentListResponse?> ListDocuments(string datasetId, string? keywords = null, string? id = null, string? name = null, RagFlowPagingRequest? pagingRequest = null)
        {
            var url = BuildRequestUrl($"datasets/{datasetId}/documents", new Dictionary<string, string?>
            {
                { "keywords", keywords }, { "id", id }, { "name", name }
            }, pagingRequest);
            return await GetJson<DocumentListResponse?>(url);
        }

        public async Task<bool> DeleteDocuments(string datasetId, List<string> documentIdList)
        {
            var url = BuildRequestUrl($"datasets/{datasetId}/documents");
            dynamic request = new ExpandoObject();
            request.ids = documentIdList;
            await DeleteJson<RagFlowResponse<string?>?>(url, request);
            return true;
        }

        public async Task<bool> ParseDocuments(string datasetId, List<string> documentIdList)
        {
            var url = BuildRequestUrl($"datasets/{datasetId}/chunks");
            dynamic request = new ExpandoObject();
            request.document_ids = documentIdList;
            await PostJson<RagFlowResponse<string?>?>(url, request);
            return true;
        }

        public async Task<bool> StopParsingDocuments(string datasetId, List<string> documentIdList)
        {
            var url = BuildRequestUrl($"datasets/{datasetId}/chunks");
            dynamic request = new ExpandoObject();
            request.document_ids = documentIdList;
            await DeleteJson<RagFlowResponse<string?>?>(url, request);
            return true;
        }
    }
}
