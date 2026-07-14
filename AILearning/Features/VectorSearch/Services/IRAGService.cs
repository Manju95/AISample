using AILearning.Features.VectorSearch.Models;

namespace AILearning.Features.VectorSearch.Services;

public interface IRAGService
{
    Task<List<SearchResult>> SearchAsync(string question);
}