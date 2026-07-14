using AILearning.Features.VectorSearch.Models;
using AILearning.Features.VectorSearch.Repository;
using AILearning.Features.VectorSearch.Services;
using AILearning.Services.Embedings.Services;

public class RAGService : IRAGService
{
    private readonly IEmbedingProvider _embeddingProvider;
    private readonly IVectorSearchRepository _repository;

    public RAGService(IEmbedingProvider embeddingProvider,IVectorSearchRepository repository)
    {
        _embeddingProvider = embeddingProvider;
        _repository = repository;
    }

    public async Task<List<SearchResult>> SearchAsync(string question)
    {
        var embedding = await _embeddingProvider.GenerateAsync(question);

        return await _repository.SearchAsync(embedding);
    }
}