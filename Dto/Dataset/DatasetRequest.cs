namespace RAGFlowClient.Dto.Dataset
{
    public sealed class DatasetRequest
    {
        /// <summary>
        /// The unique name of the dataset to create.
        /// </summary>
        public string Name { get; set; } = null!;

        /// <summary>
        /// Base64 encoding of the avatar.
        /// </summary>
        public string? Avatar { get; set; }

        public string? Description { get; set; }

        /// <summary>
        /// Must follow model_name@model_factory format
        /// </summary>
        public string? EmbeddingModel { get; set; }

        /// <summary>
        /// "me" or "team"
        /// </summary>
        public string? Permission { get; set; } = "me";

        /// <summary>
        /// naive/book/email/...
        /// </summary>
        public string? ChunkMethod { get; set; } = "naive";

        public int Pagerank { get; set; }

        public DatasetParserConfig? ParserConfig { get; set; }
    }

    public class DatasetParserConfig
    {
        public int? AutoKeywords { get; set; }

        public int? AutoQuestions { get; set; }

        public int? ChunkTokenNum { get; set; }

        public string? Delimiter { get; set; }

        public bool? Html4Excel { get; set; }

        public string? LayoutRecognize { get; set; }

        public List<string>? TagKbIds { get; set; }

        public int? TaskPageSize { get; set; }

        public DatasetRaptorConfig? Raptor { get; set; }

        public DatasetGraphRagConfig? GraphRag { get; set; }
    }
 
    public class DatasetGraphRagConfig
    {
        public bool UseGraphRag { get; set; }
    }

    public class DatasetRaptorConfig
    {
        public bool UseRaptor { get; set; }
    }
}
