using CorporateRAG.Application.Abstractions.Documents;

namespace CorporateRAG.Infrastructure.Documents;

public class TextFileExtractor : IDocumentTextExtractor
{
    public bool CanExtract(string contentType)
    {
        return contentType == "text/plain";
    }
    public async Task<IReadOnlyCollection<ExtractedTextPage>> ExtractTextAsync(
        string filePath,
        string contentType,
        CancellationToken cancellationToken = default)
    {
        if(!CanExtract(contentType))
        {
            throw new NotSupportedException($"Content type '{contentType}' is not supported yet.");
        }

        var text = await File.ReadAllTextAsync(filePath, cancellationToken);

        return new List<ExtractedTextPage>
        {
            new ExtractedTextPage(null, text)
        };
    }
}
