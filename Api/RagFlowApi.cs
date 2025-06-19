namespace RAGFlowClient.Api
{
    public sealed class RagFlowApi
    {
        public IDatasetApi Dataset { get; } 
        public IDocumentApi Document { get; }
        public IChunkApi Chunk { get; }
        public RagFlowApi(IDatasetApi dataset, IDocumentApi document, IChunkApi chunk)
        {
            Dataset = dataset;
            Document = document;
            Chunk = chunk;
        }
    }
}
