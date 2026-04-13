
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace CorporateRAG.Application.Abstractions.Documents;

public interface ITextChunker
{
    IReadOnlyCollection<string> Chunk(string text);
}