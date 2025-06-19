namespace RAGFlowClient.Dto.Dataset
{
    public class DatasetDto
    {
        public string Id { get; set; } = null!;

        public string Name { get; set; } = null!;

        public string? Description { get; set; }

        public string? Avatar { get; set; } 

        public int ChunkCount { get; set; }

        public int DocumentCount { get; set; }

        public string EmbeddingModel { get; set; } = null!;

        public string? Language { get; set; }

        public string ChunkMethod { get; set; } = null!;

        public int Pagerank { get; set; }

        public ParserConfig ParserConfig { get; set; } = new ();

        public string Permission { get; set; } = null!;

        public double SimilarityThreshold { get; set; }

        public string Status { get; set; } = null!;

        public string? TenantId { get; set; }

        public string CreatedBy { get; set; } = null!;

        public long CreateTime { get; set; }

        public int TokenNum { get; set; }

        public string CreateDate { get; set; } = null!;

        public string UpdateDate { get; set; } = null!;

        public long UpdateTime { get; set; }

        public double VectorSimilarityWeight { get; set; }
    }

    public class ParserConfig
    {
        public int ChunkTokenNum { get; set; }

        public string? Delimiter { get; set; }

        public List<string>? EntityTypes { get; set; }
    }
}