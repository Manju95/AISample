using AILearning.Features.UploadDocument.Services;
using Microsoft.AspNetCore.Mvc;

namespace AILearning.Features.UploadDocument.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DocumentController : ControllerBase
{
    private readonly IDataIngestionService _service;
    
    public DocumentController(IDataIngestionService service)
    {
        _service = service ?? throw new ArgumentNullException(nameof(service));
    }
    
    [HttpPost("upload")]
    public async Task<IActionResult> Upload(IFormFile file)
    {
        await _service.UploadAsync(file);
        return Ok();
    }
}