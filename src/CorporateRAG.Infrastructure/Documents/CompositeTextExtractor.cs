using CorporateRAG.Application.Abstractions.Documents;

namespace CorporateRAG.Infrastructure.Documents;

public class CompositeTextExtractor : ITextExtractor
{
    public readonly IEnumerable<IDocumentTextExtractor> _extractors;

    public CompositeTextExtractor(IEnumerable<IDocumentTextExtractor> extractors)
    {
        _extractors = extractors;
    }

    public async Task<IReadOnlyCollection<ExtractedTextPage>> ExtractTextAsync(
        string filePath,
        string contentType,
        CancellationToken cancellationToken = default)
    {
        var extractor = _extractors.FirstOrDefault(x => x.CanExtract(contentType));

        if (extractor is null)
        {
            throw new NotSupportedException($"Content type '{contentType}' is not supported");
        }

        return await extractor.ExtractTextAsync(
            filePath,
            contentType,
            cancellationToken);
    }
}