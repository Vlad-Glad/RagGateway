using CorporateRAG.Application.Abstractions.Documents;
using System.Net.WebSockets;

namespace CorporateRAG.Infrastructure.Documents;

public class SimpleTextChunker : ITextChunker
{
    private const int ChunkSize = 1024;
    private const int OverlapSize = 256;

    public IReadOnlyCollection<string> Chunk(string text)
    {
        if (string.IsNullOrWhiteSpace(text))
        {
            return Array.Empty<string>();
        }

        var chunks = new List<string>();
        var start = 0;

        while (start < text.Length)
        {
            var length = Math.Min(ChunkSize, text.Length - start);
            var chunk = text.Substring(start, length).Trim();

            if (!string.IsNullOrWhiteSpace(chunk))
            {
                chunks.Add(chunk);
            }

            if(start + length >= text.Length)
            {
                break;
            }

            start += ChunkSize - OverlapSize;
        }

        return chunks;
    }
}