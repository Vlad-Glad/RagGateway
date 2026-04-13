
namespace CorporateRAG.Application.Abstractions.Documents;

public interface ITextExtractor
{
    Task<string> ExtractTextAsync(
        string filePath,
        string contentType,
        CancellationToken cancellationToken = default);
}