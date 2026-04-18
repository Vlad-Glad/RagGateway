namespace CorporateRAG.Application.Abstractions.Documents;

public sealed record TextChunk(
    string Text,
    int? StartPageNumber,
    int? EndPageNumber);