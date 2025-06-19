namespace RAGFlowClient.Dto.Document
{
    public class DocumentSummaryDto
    {
        public string Id { get; set; } = null!;

        public string Name { get; set; } = null!;

        public string? Location { get; set; }

        public long Size { get; set; }

        public string DatasetId { get; set; } = null!;

        public string? CreatedBy { get; set; }

        public string ChunkMethod { get; set; } = null!;

        public string? Thumbnail { get; set; }

        public string? Type { get; set; }

        public string? Run { get; set; }

        public ParserConfig? ParserConfig { get; set; }
    }

    public sealed class DocumentDto : DocumentSummaryDto
    {
        public string? Status { get; set; }

        public string? CreateDate { get; set; }

        public long CreateTime { get; set; }

        public string? UpdateDate { get; set; }

        public long UpdateTime { get; set; }

        public string? SourceType { get; set; }

        public int ChunkCount { get; set; }

        public int TokenCount { get; set; }

        public double Progress { get; set; }

        public string? ProgressMsg { get; set; }

        public string? ProcessBeginAt { get; set; }

        public double ProcessDuration { get; set; }

    }

    public sealed class ParserConfig
    {
        public int ChunkTokenNum { get; set; }

        public string? LayoutRecognize { get; set; }

        public bool Html4Excel { get; set; }

        public string? Delimiter { get; set; }

        public RaptorConfig? Raptor { get; set; }
    }

    public sealed class RaptorConfig
    {
        public bool UseRaptor { get; set; }
    }
}