namespace AILearning.Features.Embedding.Models;

public class Document
{
    public Guid Id { get; set; }
    public string FileName { get; set; } = "";
    public DateTime UploadedAt { get; set; }
    public int TotalChunks { get; set; }
    public ICollection<DocumentChunk> Chunks { get; set; } = new List<DocumentChunk>();
}