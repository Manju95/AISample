namespace AILearning.Features.Chunking;

public interface IChunkingService
{
    IReadOnlyList<string> Chunk(string text);
}