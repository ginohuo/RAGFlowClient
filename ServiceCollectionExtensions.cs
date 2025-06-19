using Microsoft.Extensions.DependencyInjection;
using RAGFlowClient;
using RAGFlowClient.Api;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddRagFlowService(this IServiceCollection services, Action<RagFlowOptions> configureOptions)
    {
        services.Configure(configureOptions);
        services.AddHttpClient();
        services.AddSingleton<IDatasetApi, DatasetApi>();
        services.AddSingleton<IDocumentApi, DocumentApi>();
        services.AddSingleton<IChunkApi, ChunkApi>();
        services.AddSingleton<RagFlowApi>();
        return services;
    }
}