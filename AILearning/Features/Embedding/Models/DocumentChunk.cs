namespace AILearning.Features.Embedding.Models;

public class DocumentChunk
{
    public Guid Id { get; set; }
    public Guid DocumentId { get; set; }
    public int ChunkNumber { get; set; }
    public string Content { get; set; } = "";
    public Document Document { get; set; } = default!;
    public ChunkEmbeding Embedding { get; set; } = default!;
}