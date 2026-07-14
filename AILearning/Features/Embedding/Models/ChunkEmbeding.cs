using System.ComponentModel.DataAnnotations.Schema;
using Pgvector;

namespace AILearning.Features.Embedding.Models;

public class ChunkEmbeding
{
    public Guid ChunkId { get; set; }
    public Vector Embedding { get; set; } = default!;
    public string EmbeddingModel { get; set; } = "";
    public DateTime CreatedAt { get; set; }
    public DocumentChunk Chunk { get; set; } = default!;
}