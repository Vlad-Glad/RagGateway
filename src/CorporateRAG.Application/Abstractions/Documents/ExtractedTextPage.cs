namespace CorporateRAG.Application.Abstractions.Documents;

public sealed record ExtractedTextPage(
    int? PageNumber,
    string text);