using AILearning.Features.Chunking;
using AILearning.Features.DataExtractor;
using AILearning.Features.Embedding.Models;
using AILearning.Features.Embedding.Repositories;
using AILearning.Services.Embedings.Services;
using Pgvector;

namespace AILearning.Features.UploadDocument.Services;

public class DataIngestionService : IDataIngestionService
{
    private readonly IDataExtractor _dataExtractor;
    private readonly IDocumentRepository _repository;
    private readonly IChunkingService _chunkingService;
    private readonly IEmbedingProvider _embeddingProvider;
    
    public DataIngestionService(IDataExtractor dataExtractor, IChunkingService chunkingService, IDocumentRepository repository, IEmbedingProvider embeddingProvider)
    {
        _dataExtractor = dataExtractor ?? throw new ArgumentNullException(nameof(dataExtractor));
        _chunkingService = chunkingService ?? throw new ArgumentNullException(nameof(chunkingService));
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        _embeddingProvider = embeddingProvider ?? throw new ArgumentNullException(nameof(embeddingProvider));
    }
    
    public async Task UploadAsync(IFormFile pdf)
    {
        int chunkNumber = 0;
        var text = await _dataExtractor.ExtractTextAsync(pdf.OpenReadStream());

        var chunks = _chunkingService.Chunk(text);
        
        var document = new Document
        {
            Id = Guid.NewGuid(),
            FileName = pdf.FileName,
            UploadedAt = DateTime.UtcNow,
            TotalChunks = chunks.Count
        };
        
        foreach (var chunk in chunks)
        {
            var embedding = await _embeddingProvider.GenerateAsync(chunk);
            var chunkId = Guid.NewGuid();
            var entity = new DocumentChunk
            {
                Id = chunkId,
                DocumentId = document.Id,
                ChunkNumber = chunkNumber++,
                Content = chunk,
                Embedding = new ChunkEmbeding
                {
                    ChunkId = chunkId,
                    Embedding = embedding,
                    EmbeddingModel = "nomic-embed-text",
                    CreatedAt = DateTime.UtcNow
                }
            };

            document.Chunks.Add(entity);
        }
        
        await _repository.SaveDocumentAsync(document);
    }
}