using System.Data;
using AILearning.Context;
using AILearning.Features.Embedding.Models;
using AILearning.Models;
using Dapper;

namespace AILearning.Features.Embedding.Repositories;

public class DocumentRepository : IDocumentRepository
{
    private readonly AppDbContext _context;
    
    public DocumentRepository(AppDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }
    
    public async Task SaveDocumentAsync(Document document)
    {
        _context.Add(document);
        await _context.SaveChangesAsync();
    }
}