using System.Net;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.Extensions.Options;
using RAGFlowClient.Dto;

namespace RAGFlowClient.Api
{
    public abstract class RagFlowBaseApi
    {
        private readonly RagFlowOptions _options;
        private readonly JsonSerializerOptions _jsonSerializerOptions;
        private readonly IHttpClientFactory _httpClientFactory;

        protected RagFlowBaseApi(IOptions<RagFlowOptions> options, IHttpClientFactory httpClientFactory)
        {
            _options = options.Value;
            _httpClientFactory = httpClientFactory;
            _jsonSerializerOptions = BuildJsonSerializerOptions();
        }

        private JsonSerializerOptions BuildJsonSerializerOptions()
        {
            var rgCaseNamingPolicy = new RgCaseLowerNamingPolicy();

            var options = new JsonSerializerOptions
            {
                PropertyNamingPolicy = rgCaseNamingPolicy,
                DictionaryKeyPolicy = JsonNamingPolicy.CamelCase,
                DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
            };
            options.Converters.Add(new JsonStringEnumConverter(rgCaseNamingPolicy));
            return options;
        }

        private HttpClient BuildHttpClient(bool json)
        {
            var httpClient = _httpClientFactory.CreateClient();
            httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {_options.ApiKey}");
            var contentType = json ? "application/json" : "multipart/form-data";
            httpClient.DefaultRequestHeaders.TryAddWithoutValidation("content-Type", contentType);
            return httpClient;
        }

        protected string BuildRequestUrl(string subPath)
        {
            return $"{_options.BaseUrl}/api/v1/{subPath}";
        }

        protected string BuildRequestUrl(string subPath, Dictionary<string, string?>? conditions, RagFlowPagingRequest? pagingRequest)
        {
            var queryQueue = new Queue<string>();
            if (conditions != null)
            {
                foreach (var condition in conditions)
                {
                    if (string.IsNullOrWhiteSpace(condition.Value))
                        continue;
                    queryQueue.Enqueue($"{condition.Key}={WebUtility.UrlEncode(condition.Value)}");
                }
            }
            if (pagingRequest != null)
            {
                queryQueue.Enqueue($"page={pagingRequest.Page}");
                queryQueue.Enqueue($"page_size={pagingRequest.PageSize}");
                if (!string.IsNullOrWhiteSpace(pagingRequest.OrderBy))
                    queryQueue.Enqueue($"orderby={pagingRequest.OrderBy}");
                if (pagingRequest.Desc.HasValue)
                    queryQueue.Enqueue($"desc={pagingRequest.Desc.Value.ToString().ToLower()}");
            }
            var url = BuildRequestUrl(subPath);
            if (!queryQueue.Any())
                return url;
            var sb = new StringBuilder(url);
            var item1 = queryQueue.Dequeue();
            sb.Append($"?{item1}");
            while (queryQueue.Count > 0)
            {
                sb.Append($"&{queryQueue.Dequeue()}");
            }
            return sb.ToString();
        }

        private T? BuildResponse<T>(string jsonResponse)
        {
            var result = JsonSerializer.Deserialize<RagFlowResponse<T>>(jsonResponse, _jsonSerializerOptions);
            if (result == null)
                return default(T);
            if (result.Code != 0)
                throw new RagFlowException(result.Code, result.Message);
            return result.Data;
        }

        protected async Task<T?> GetJson<T>(string url)
        {
            var jsonResponse = await BuildHttpClient(true).GetStringAsync(url);
            return BuildResponse<T>(jsonResponse);
        }

        protected async Task<T?> PostJson<T>(string url, object requestObject)
        {
            var jsonRequest = JsonSerializer.Serialize(requestObject, _jsonSerializerOptions);
            var requestContent = new StringContent(jsonRequest, Encoding.UTF8, "application/json");
            var response = await BuildHttpClient(true).PostAsync(url, requestContent);
            var jsonResponse = await response.Content.ReadAsStringAsync();
            return BuildResponse<T>(jsonResponse);
        }

        private async Task<T?> DeleteOrPutJson<T>(string url, object requestObject, bool delete)
        {
            var jsonRequest = JsonSerializer.Serialize(requestObject, _jsonSerializerOptions);
            var requestContent = new StringContent(jsonRequest, Encoding.UTF8, "application/json");
            var method = delete ? HttpMethod.Delete : HttpMethod.Put;
            var request = new HttpRequestMessage(method, url)
            {
                Content = requestContent
            };
            var response = await BuildHttpClient(true).SendAsync(request);
            var jsonResponse = await response.Content.ReadAsStringAsync();
            return BuildResponse<T>(jsonResponse);
        }

        protected async Task<T?> DeleteJson<T>(string url, object requestObject)
        {
            return await DeleteOrPutJson<T>(url, requestObject, true);
        }

        protected async Task<T?> PutJson<T>(string url, object requestObject)
        {
            return await DeleteOrPutJson<T>(url, requestObject, false);
        }

        protected async Task<T?> PostFiles<T>(string url, List<string> filePathList)
        {
            using (var formData = new MultipartFormDataContent())
            {
                foreach (var filePath in filePathList)
                {
                    var stream = new FileStream(filePath, FileMode.Open, FileAccess.Read);
                    var content = new StreamContent(stream);
                    formData.Add(content, "file", Path.GetFileName(filePath));
                }
                var response = await BuildHttpClient(false).PostAsync(url, formData);
                var jsonResponse = await response.Content.ReadAsStringAsync();
                return BuildResponse<T>(jsonResponse);
            }
        }

        protected async Task<byte[]?> DownloadFile(string url)
        {
            var response = await BuildHttpClient(true).GetAsync(url);
            response.EnsureSuccessStatusCode();
            var contentTypeItem = response.Content.Headers.FirstOrDefault(t => t.Key.ToLower() == "content-type");
            //return result = json error
            if (contentTypeItem.Value.Any() && contentTypeItem.Value.Any(t => t.ToLower().Contains("application/json")))
            {
                var jsonResponse = await response.Content.ReadAsStringAsync();
                BuildResponse<string>(jsonResponse);
                return null;
            }
            const string contentDispositionFlag = "content-disposition";
            //no file return
            if (response.Content.Headers.All(t => t.Key.ToLower() != contentDispositionFlag))
                return null;
            var contentDispositionItem = response.Content.Headers.FirstOrDefault(t => t.Key.ToLower() == contentDispositionFlag);
            if (!contentDispositionItem.Value.Any(t => t.Contains("filename=")))
                return null;
            return await response.Content.ReadAsByteArrayAsync();
        }
    }
}
