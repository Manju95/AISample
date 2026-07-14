namespace AILearning.Features.Chunking;

public class ChunkingService : IChunkingService
{
    private const int ChunkSize = 500;

    private const int Overlap = 100;

    public IReadOnlyList<string> Chunk(string text)
    {
        var chunks = new List<string>();
        var start = 0;
        while (start < text.Length)
        {
            var length = Math.Min(ChunkSize, text.Length - start);
            chunks.Add(text.Substring(start, length));
            start += ChunkSize - Overlap;
        }

        return chunks;
    }
}