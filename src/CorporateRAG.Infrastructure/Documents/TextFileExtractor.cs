using CorporateRAG.Application.Abstractions.Documents;

namespace CorporateRAG.Infrastructure.Documents;

public class TextFileExtractor : ITextExtractor
{
    public async Task<string> ExtractTextAsync(
        string filePath,
        string contentType,
        CancellationToken cancellationToken = default)
    {
        if(contentType != "text/plain")
        {
            throw new NotSupportedException($"Content type '{contentType}' is not supported yet.");
        }

        return await File.ReadAllTextAsync(filePath, cancellationToken);
    }
}
