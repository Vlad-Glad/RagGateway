using CorporateRAG.Application.UseCases.Documents;
using Microsoft.AspNetCore.Mvc;

namespace CorporateRAG.Web.Controllers;

[ApiController]
[Route("api/documents")]
public class DocumentController : ControllerBase
{
    private readonly UploadDocumentService _uploadService;

    public DocumentController(UploadDocumentService uploadService)
    {
        _uploadService = uploadService;
    }

    [HttpPost("upload")]
    public async Task<IActionResult> Upload(IFormFile file)
    {
        if (file == null || file.Length == 0)
        {
            return BadRequest("File is empty");
        }

        using var stream = file.OpenReadStream();

        var documentId = await _uploadService.UploadAsync(
            stream,
            file.FileName,
            file.ContentType);

        return Ok(new { Id = documentId });

    }
}
