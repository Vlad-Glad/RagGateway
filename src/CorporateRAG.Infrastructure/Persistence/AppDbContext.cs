using CorporateRAG.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace CorporateRAG.Infrastructure.Persistence;

public class AppDbContext : DbContext
{
    public DbSet<Document> Documents => Set<Document>();
    public DbSet<DocumentChunk> DocumentChunks => Set<DocumentChunk>();

    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {

    }
}