using CorporateRAG.Application.Abstractions.Documents;
using CorporateRAG.Application.Abstractions.Persistence;
using CorporateRAG.Core.Entities;

namespace CorporateRAG.Application.UseCases.Documents;

public class IndexDocumentService
{
    private readonly IDocumentRepository _documentRepository;
    private readonly IChunkRepository _chunkRepository;
    private readonly ITextExtractor _textExtractor;
    private readonly ITextChunker _textChunker;

    public IndexDocumentService(
        IDocumentRepository documentRepository,
        IChunkRepository chunkRepository,
        ITextExtractor textExtractor,
        ITextChunker textChunker)
    {
        _documentRepository = documentRepository;
        _chunkRepository = chunkRepository;
        _textExtractor = textExtractor;
        _textChunker = textChunker;
    }

    public async Task IndexAsync(Guid documentId, CancellationToken cancellationToken = default)
    {
        var document = await _documentRepository.GetByIdAsync(documentId, cancellationToken);

        if (document is null)
        {
            throw new InvalidOperationException("Document not found.");
        }

        var text = await _textExtractor.ExtractTextAsync(
            document.StoragePath,
            document.ContentType,
            cancellationToken);

        var chunks = _textChunker.Chunk(text);

        var documentChunks = chunks
            .Select((chunkText, index) => new DocumentChunk(
                document.Id,
                index,
                chunkText,
                pageNumber: null))
            .ToList();

        await _chunkRepository.AddRangeAsync(documentChunks, cancellationToken);
        await _chunkRepository.SaveChangesAsync(cancellationToken);

        document.MarkAsIndexed();
        await _documentRepository.SaveChangesAsync(cancellationToken);
    }


}
