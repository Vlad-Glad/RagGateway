
namespace CorporateRAG.Core.Entities;


public class DocumentChunk
{
    public Guid Id { get; private set; }
    public Guid DocumentId { get; private set; }
    public int ChunkIndex { get; private set; }

    public string Text { get; private set; }

    public int? PageNumber { get; private set; }

    public DocumentChunk(
        Guid documentId,
        int chunkIndex,
        string text,
        int? pageNumber)
    {
        if (documentId == Guid.Empty)
        {
            throw new ArgumentException("Document id cannot be empty.", nameof(documentId));
        }
        
        if (ChunkIndex < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(chunkIndex), "Chunk index cannot be negative.");
        }

        if (pageNumber.HasValue && pageNumber <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(pageNumber), "Page number be greater than zero.");
        }

        Id = Guid.NewGuid();
        DocumentId = documentId;
        ChunkIndex = chunkIndex;
        Text = text;
        PageNumber = pageNumber;

    }

}