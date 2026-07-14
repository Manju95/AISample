using AILearning.Features.VectorSearch.Models;
using AILearning.Features.VectorSearch.Services;
using Microsoft.AspNetCore.Mvc;

namespace AILearning.Features.VectorSearch.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RAGController : ControllerBase
{
    private readonly IRAGService _ragService;

    public RAGController(IRAGService ragService)
    {
        _ragService = ragService ?? throw new ArgumentNullException(nameof(ragService));
    }

    [HttpPost("ask")]
    public async Task<IActionResult> Ask(AskRequest request)
    {
        var chunks = await _ragService.SearchAsync(request.Question);

        return Ok(chunks);
    }
}