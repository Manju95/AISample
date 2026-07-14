using AILearning.Features.VectorSearch.Models;
using Pgvector;

namespace AILearning.Features.VectorSearch.Repository;

public interface IVectorSearchRepository
{
    Task<List<SearchResult>> SearchAsync(Vector embedding,int topK = 5);
}