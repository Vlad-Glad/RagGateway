using CorporateRAG.Application.Abstractions.Persistence;
using CorporateRAG.Application.Abstractions.Storage;
using CorporateRAG.Core.Entities;

namespace CorporateRAG.Application.UseCases.Documents;

public class UploadDocumentService
{
    private readonly IFileStorage _fileStorage;
    private readonly IDocumentRepository _documentRepository;

    public UploadDocumentService(
        IFileStorage fileStorage,
        IDocumentRepository documentRepository)
    {
        _fileStorage = fileStorage;
        _documentRepository = documentRepository;
    }

    public async Task<Guid> UploadAsync(
        Stream fileStream,
        string fileName,
        string contentType,
        CancellationToken cancellationToken = default)
    {
        var storagePath = await _fileStorage.SaveAsync(
            fileStream,
            fileName,
            cancellationToken);

        var document = new Document(
            fileName,
            contentType,
            storagePath);

        await _documentRepository.AddAsync(document, cancellationToken);
        await _documentRepository.SaveChangesAsync(cancellationToken);

        return document.Id;
    }
}