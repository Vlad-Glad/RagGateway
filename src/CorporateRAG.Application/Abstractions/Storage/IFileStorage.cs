using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace CorporateRAG.Application.Abstractions.Storage;

public interface IFileStorage
{
    Task<string> SaveAsync(
        Stream content,
        string fileName,
        CancellationToken cancellationToken = default);
}