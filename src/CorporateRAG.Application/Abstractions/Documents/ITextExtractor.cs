
namespace CorporateRAG.Application.Abstractions.Documents;

public interface ITextExtractor
{
    bool CanExtract(string contentType);

    Task<string> ExtractTextAsync(
        string filePath,
        string contentType,
        CancellationToken cancellationToken = default);
}