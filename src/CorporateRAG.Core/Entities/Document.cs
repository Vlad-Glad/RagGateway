using CorporateRAG.Core.Enums;

namespace CorporateRAG.Core.Entities;

public class Document
{
    public Guid Id { get; private set; }
    public string FileName { get; private set; }
    public string ContentType { get; private set; }
    public string StoragePath { get; private set; }
    public DateTime UploadedAtUtc { get; private set; }
    public DocumentStatus Status { get; private set; }

    public Document(
        string fileName,
        string contentType,
        string storagePath)
    {
        if (string.IsNullOrWhiteSpace(fileName))
        {
            throw new ArgumentException("File name cannot be empty.", nameof(fileName));
        }
        if (string.IsNullOrWhiteSpace(contentType))
        {
            throw new ArgumentException("Content type cannot be empty.", nameof(contentType));
        }
        if (string.IsNullOrWhiteSpace(storagePath))
        {
            throw new ArgumentException("Stirage path cannot be empty.", nameof(storagePath));
        }

        Id = Guid.NewGuid();
        FileName = fileName;
        ContentType = contentType;
        StoragePath = storagePath;
        UploadedAtUtc = DateTime.UtcNow;
        Status = DocumentStatus.Uploaded;
    }

    public void MarkAsIndexed()
    {
        Status = DocumentStatus.Indexed;
    }

    public void MarkAsFailed()
    {
        Status = DocumentStatus.Failed;
    }
}