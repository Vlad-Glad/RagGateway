
using CorporateRAG.Core.Entities;
using CorporateRAG.Application.Abstractions.Persistence;
using System.Reflection.Metadata;

public interface IChunkRepository
{
    Task AddAsync(DocumentChunk chunk, CancellationToken cancellationToken = default);
    Task AddRangeAsync(IEnumerable<DocumentChunk> chunks, CancellationToken cancellationToken = default);

    Task<IReadOnlyCollection<DocumentChunk>> GetByDocumentIdAsync(
        Guid documentId,
        CancellationToken cancellationToken = default);

    Task SaveChangesAsync(CancellationToken cancellationToken = default);
}