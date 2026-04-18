using CorporateRAG.Application.Abstractions.Documents;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using UglyToad.PdfPig;

namespace CorporateRAG.Infrastructure.Documents;

public class PdfTextExtractor : IDocumentTextExtractor
{
    public bool CanExtract(string contentType)
    {
        return contentType == "application/pdf";
    }

    public async Task<IReadOnlyCollection<ExtractedTextPage>> ExtractTextAsync(
        string filePath,
        string contentType,
        CancellationToken cancellationToken = default)
    {
        if (!CanExtract(contentType))
        {
            throw new NotSupportedException($"Content type '{contentType}' is not supported by PDF extractor.");
        }

        using var document = PdfDocument.Open(filePath);

        var pages = new List<ExtractedTextPage>();

        foreach (var page in document.GetPages())
        {
            cancellationToken.ThrowIfCancellationRequested();

            pages.Add(new ExtractedTextPage(page.Number,
                page.Text));
        }

        return await Task.FromResult(pages);
    }
}