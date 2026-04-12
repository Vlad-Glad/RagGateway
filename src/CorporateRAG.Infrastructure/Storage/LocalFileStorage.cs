using CorporateRAG.Application.Abstractions.Storage;

namespace CorporateRAG.Infrastructure.Storage;

public class LocalFileStorage : IFileStorage
{
    private readonly string _basePath = Path.Combine(Directory.GetCurrentDirectory(), "storage");

    public async Task<string> SaveAsync(
        Stream content,
        string fileName,
        CancellationToken cancellationToken = default)
    {
        if (!Directory.Exists(_basePath))
        {
            Directory.CreateDirectory(_basePath);
        }

        var filePath = Path.Combine(_basePath, $"{Guid.NewGuid()}_{fileName}");

        using var fileStream = new FileStream(filePath, FileMode.Create);

        await content.CopyToAsync(fileStream, cancellationToken);

        return filePath;
    }
}