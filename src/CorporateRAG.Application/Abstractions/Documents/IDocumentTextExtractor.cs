namespace CorporateRAG.Application.Abstractions.Documents;

public interface IDocumentTextExtractor
{
    bool CanExtract(string contentTyp);

    Task<IReadOnlyCollection<ExtractedTextPage>> ExtractTextAsync(
        string filePath,
        string contentType,
        CancellationToken cancellationToken = default);
}