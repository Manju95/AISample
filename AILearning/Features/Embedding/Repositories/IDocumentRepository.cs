using AILearning.Features.Embedding.Models;

namespace AILearning.Features.Embedding.Repositories;

public interface IDocumentRepository
{
    Task SaveDocumentAsync(Document document);
}