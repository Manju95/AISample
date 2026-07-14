using Pgvector;

namespace AILearning.Services.Embedings.Services;

public interface IEmbedingProvider
{
    Task<Vector> GenerateAsync(string text);
}