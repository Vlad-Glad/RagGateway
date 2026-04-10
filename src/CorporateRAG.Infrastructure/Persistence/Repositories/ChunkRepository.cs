using CorporateRAG.Application.Abstractions.Persistence;
using CorporateRAG.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace CorporateRAG.Infrastructure.Persistence.Repositories;

public class ChunkRepository : IChunkRepository
{
    private readonly AppDbContext _dbContext;

    public ChunkRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task AddAsync(DocumentChunk chunk, CancellationToken cancellationToken = default)
    {
        await _dbContext.DocumentChunks.AddAsync(chunk, cancellationToken);
    }

    public async Task AddRangeAsync(IEnumerable<DocumentChunk> chunks, CancellationToken cancellationToken = default)
    {
        await _dbContext.DocumentChunks.AddRangeAsync(chunks, cancellationToken);
    }

    public async Task<IReadOnlyCollection<DocumentChunk>> GetByDocumentIdAsync(
        Guid documentId,
        CancellationToken cancellationToken = default)
    {
        return await _dbContext.DocumentChunks
            .Where(x => x.DocumentId == documentId)
            .OrderBy(x => x.ChunkIndex)
            .ToListAsync(cancellationToken);
    }

    public async Task SaveChangesAsync(CancellationToken cancellationToken  = default)
    {
        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}