using Pgvector.EntityFrameworkCore;
using AILearning.Features.Embedding.Models;
using AILearning.Models;
using Microsoft.EntityFrameworkCore;
using Pgvector;

namespace AILearning.Context;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }
    
    public DbSet<Test> Test { get; set; }
    
    public DbSet<Document> Documents {get; set;}
    public DbSet<DocumentChunk> DocumentChunks {get; set;}
    public DbSet<ChunkEmbeding> ChunkEmbeddings {get; set;}
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasPostgresExtension("vector");
        
        modelBuilder.Entity<Test>().ToTable("test");

        modelBuilder.Entity<Test>().Property(x => x.Id).HasColumnName("id");
        modelBuilder.Entity<Test>().Property(x => x.Name).HasColumnName("name");
        
        
        modelBuilder.Entity<Document>(entity =>
        {
            entity.ToTable("documents");
            entity.HasKey(x => x.Id);
            entity.HasMany(x => x.Chunks)
                .WithOne(x => x.Document)
                .HasForeignKey(x => x.DocumentId);

            entity.Property(x => x.Id)
            .HasColumnName("id");

            entity.Property(x => x.FileName)
                .HasColumnName("file_name");

            entity.Property(x => x.UploadedAt)
                .HasColumnName("uploaded_at");

            entity.Property(x => x.TotalChunks)
                .HasColumnName("total_chunks");
        });
        
        modelBuilder.Entity<DocumentChunk>(entity =>
        {
            entity.ToTable("document_chunks");
            entity.HasKey(x => x.Id);
            entity.HasOne(x => x.Embedding)
                .WithOne(x => x.Chunk)
                .HasForeignKey<ChunkEmbeding>(x => x.ChunkId);

            entity.Property(x => x.Id)
                .HasColumnName("id");

            entity.Property(x => x.DocumentId)
                .HasColumnName("document_id");

            entity.Property(x => x.ChunkNumber)
                .HasColumnName("chunk_number");

            entity.Property(x => x.Content)
                .HasColumnName("content");
        });

        modelBuilder.Entity<ChunkEmbeding>(entity =>
        {
            entity.ToTable("chunk_embeddings");
            entity.HasKey(x => x.ChunkId);
            entity.Property(x => x.Embedding)
                .HasColumnType("vector(768)");

            entity.Property(x => x.ChunkId)
                .HasColumnName("chunk_id");

            entity.Property(x => x.Embedding)
                .HasColumnName("embedding");

            entity.Property(x => x.CreatedAt)
                .HasColumnName("created_at");

            entity.Property(x => x.EmbeddingModel)
                .HasColumnName("embedding_model");
        });
    }
}