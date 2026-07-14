using AILearning.Features.VectorSearch.Models;
using Dapper;
using Npgsql;
using Pgvector;

namespace AILearning.Features.VectorSearch.Repository;

public class VectorSearchRepository : IVectorSearchRepository
{
    private readonly NpgsqlConnection _connection;

    public VectorSearchRepository(NpgsqlConnection connection)
    {
        _connection = connection;
    }

    public async Task<List<SearchResult>> SearchAsync(Vector embedding,int topK = 5)
    {
        const string sql = """
        SELECT
            dc.content,
            ce.embedding <=> @Embedding AS score
        FROM chunk_embeddings ce
        JOIN document_chunks dc
            ON dc.id = ce.chunk_id
        ORDER BY ce.embedding <=> @Embedding
        LIMIT @TopK;
        """;

        var parameters = new DynamicParameters();
        parameters.Add("Embedding", embedding, dbType: null);
        parameters.Add("TopK", topK);

        var result = await _connection.QueryAsync<SearchResult>(sql, parameters);
        return result.ToList();
    }
}