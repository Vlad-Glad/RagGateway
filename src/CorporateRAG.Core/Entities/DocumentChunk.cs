
using Microsoft.EntityFrameworkCore.Storage.Json;

namespace CorporateRAG.Core.Entities;


public class DocumentChunk
{
    public Guid Id { get; private set; }
    public Guid DocumentId { get; private set; }
    public int ChunkIndex { get; private set; }

    public string Text { get; private set; }

    public int? StartPageNumber { get; private set; }
    public int? EndPageNumber { get; private set; }
    
    private DocumentChunk() { }

    public DocumentChunk(
        Guid documentId,
        int chunkIndex,
        string text,
        int? startPageNumber,
        int? endPageNumber)
    {
        if (documentId == Guid.Empty)
        {
            throw new ArgumentException("Document id cannot be empty.", nameof(documentId));
        }
        
        if (chunkIndex < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(chunkIndex), "Chunk index cannot be negative.");
        }

        if (string.IsNullOrWhiteSpace(text))
        {
            throw new ArgumentException("Chunk text cannot be empty.", nameof(text));
        }

        if (startPageNumber.HasValue && startPageNumber <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(startPageNumber), "Start page number must be greated that zero.");
        }

        if (endPageNumber.HasValue && endPageNumber <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(endPageNumber), "Start page number must be greated that zero.");
        }

        if (startPageNumber.HasValue && endPageNumber.HasValue && endPageNumber < startPageNumber)
        {
            throw new ArgumentException("End page number cannot be less than start page number.");
        }

        Id = Guid.NewGuid();
        DocumentId = documentId;
        ChunkIndex = chunkIndex;
        Text = text;
        StartPageNumber = startPageNumber;
        EndPageNumber = endPageNumber;
    }

}