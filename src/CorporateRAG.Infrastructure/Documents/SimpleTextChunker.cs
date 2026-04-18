using CorporateRAG.Application.Abstractions.Documents;
using System.Net.WebSockets;
using System.Text;

namespace CorporateRAG.Infrastructure.Documents;

public class SimpleTextChunker : ITextChunker
{
    private const int ChunkSize = 1024;
    private const int OverlapSize = 256;

    public IReadOnlyCollection<TextChunk> Chunk(IReadOnlyCollection<ExtractedTextPage> pages)
    {
        if (pages.Count == 0)
        {
            return Array.Empty<TextChunk>();
        }

        var fullTextBuilder = new StringBuilder();
        var pageRanges = new List<PageTextRange>();

        foreach (var page in pages)
        {
            if (string.IsNullOrWhiteSpace(page.text))
            {
                continue;
            }

            var startIndex = fullTextBuilder.Length;

            fullTextBuilder.Append(page.text.Trim());
            fullTextBuilder.Append(Environment.NewLine);

            var endIndexExclusive = fullTextBuilder.Length;

            if (page.PageNumber.HasValue)
            {
                pageRanges.Add(new PageTextRange(
                    startIndex,
                    endIndexExclusive,
                    page.PageNumber.Value));
            }
        }

        var fullText = fullTextBuilder.ToString();

        if (string.IsNullOrWhiteSpace(fullText))
        {
            return Array.Empty<TextChunk>();
        }

        var chunks = new List<TextChunk>();
        var start = 0;

        while (start < fullText.Length)
        {
            var length = Math.Min(ChunkSize, fullText.Length - start);
            var endExclusive = start + length;

            var chunkText = fullText.Substring(start, length).Trim();

            if (!string.IsNullOrWhiteSpace(chunkText))
            {
                var pagesForChunk = pageRanges
                    .Where(x => x.StartIndex < endExclusive && x.EndIndexExclusive > start)
                    .Select(x => x.PageNumber)
                    .Distinct()
                    .OrderBy(x => x)
                    .ToList();

                int? startPageNumber = pagesForChunk.Count > 0
                    ? pagesForChunk.First()
                    : null;

                int? endPageNumber = pagesForChunk.Count > 0
                    ? pagesForChunk.Last()
                    : null;

                chunks.Add(new TextChunk(
                    chunkText,
                    startPageNumber,
                    endPageNumber));
            }
            
            if (endExclusive >= fullText.Length)
            {
                break;
            }

            start += ChunkSize - OverlapSize;
        }

        return chunks;
    }

    private sealed record PageTextRange(
        int StartIndex,
        int EndIndexExclusive,
        int PageNumber);
}