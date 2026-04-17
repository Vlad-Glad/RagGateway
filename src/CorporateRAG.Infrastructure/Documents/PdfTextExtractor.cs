using CorporateRAG.Application.Abstractions.Documents;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using UglyToad.PdfPig;

namespace CorporateRAG.Infrastructure.Documents;

public class PdfTextExtractor : ITextExtractor
{
    public bool CanExtract(string contentType)
    {
        return contentType == "application/pdf";
    }

    public async Task<string> ExtractTextAsync(
        string filePath,
        string contentType,
        CancellationToken cancellationToken = default)
    {
        if (!CanExtract(contentType))
        {
            throw new NotSupportedException($"Content type '{contentType}' is not supported by PDF extractor.");
        }

        using var document = PdfDocument.Open(filePath);

        var textParts = new List<string>();

        foreach (var page in document.GetPages())
        {
            cancellationToken.ThrowIfCancellationRequested();
            
            textParts.Add(page.Text);
        }

        return await Task.FromResult(string.Join(Environment.NewLine, textParts));
    }
}