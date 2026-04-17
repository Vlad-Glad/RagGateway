using CorporateRAG.Application.Abstractions.Documents;

namespace CorporateRAG.Infrastructure.Documents;

public class CompositeTextExtractor : ITextExtractor
{
    public readonly IEnumerable<ITextExtractor> _extractors;

    public CompositeTextExtractor(IEnumerable<ITextExtractor> extractors)
    {
        _extractors = extractors;
    }

    public bool CanExtract(string contentType)
    {
        return _extractors.Any(x => x.CanExtract(contentType));
    }

    public async Task<string> ExtractTextAsync(
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