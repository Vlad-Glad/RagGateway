using CorporateRAG.Application.Abstractions.Documents;

namespace CorporateRAG.Application.Abstractions.Documents;

public interface ITextChunker
{
    IReadOnlyCollection<TextChunk> Chunk(IReadOnlyCollection<ExtractedTextPage> pages);
}