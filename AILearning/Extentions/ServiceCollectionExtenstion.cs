using AILearning.Configuration;
using AILearning.Context;
using AILearning.Features.Chat.Providers;
using AILearning.Features.Chat.Services;
using AILearning.Features.Chunking;
using AILearning.Features.DataExtractor;
using AILearning.Features.Embedding.Repositories;
using AILearning.Features.Prompt.Services;
using AILearning.Features.UploadDocument.Services;
using AILearning.Features.VectorSearch.Repository;
using AILearning.Features.VectorSearch.Services;
using AILearning.Models;
using AILearning.Services.Embedings.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Npgsql;

namespace AILearning.Extentions;

public static class ServiceCollectionExtenstion
{
    public static IServiceCollection AddServiceExtenstions(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection");
        var dataSourceBuilder = new NpgsqlDataSourceBuilder(connectionString);
        dataSourceBuilder.UseVector();
        var dataSource = dataSourceBuilder.Build();
        services.AddSingleton(dataSource);

        services.AddDbContext<AppDbContext>(options =>
        {
            options.UseNpgsql(dataSource, o => o.UseVector());
        });

        services.AddScoped<NpgsqlConnection>(sp =>
            sp.GetRequiredService<NpgsqlDataSource>().CreateConnection());

        services.Configure<OLLMOptions>(configuration.GetSection("OLLMA"));

        services.AddControllers();
        services.AddScoped<IChatService, ChatService>();
        services.AddScoped<IChunkingService, ChunkingService>();
        services.AddScoped<IDataExtractor, PDFDataExtractor>();
        services.AddScoped<IDocumentRepository, DocumentRepository>();
        services.AddScoped<IDataIngestionService, DataIngestionService>();
        services.AddScoped<IRAGService, RAGService>();
        services.AddScoped<IVectorSearchRepository, VectorSearchRepository>();
        services.AddScoped<IPromptBuilder, PromptBuilder>();

        services.AddHttpClient<IChatProvider, OLLMAProvider>((provider, client) =>
        {
            var options = provider.GetRequiredService<IOptions<OLLMOptions>>().Value;
            client.BaseAddress = new Uri(options.BaseUrl);
        });

        services.AddHttpClient<IEmbedingProvider, OllmaEmbedingProvider>((provider, client) =>
        {
            var options = provider.GetRequiredService<IOptions<OLLMOptions>>().Value;
            client.BaseAddress = new Uri(options.BaseUrl);
        });
        
        return services;
    }
}