using AILearning.Features.Chat.Models;
using AILearning.Features.Chat.Services;
using Microsoft.AspNetCore.Mvc;

namespace AILearning.Features.Chat.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ChatController : ControllerBase
{
    private readonly IChatService _chatService;
    
    public ChatController(IChatService chatService)
    {
        _chatService = chatService ?? throw new ArgumentNullException(nameof(chatService));
    }
    
    [HttpPost]
    public async Task<IActionResult> Chat(ChatRequest request)
    {
        var answer = await _chatService.AskQuestionAsync(request);
        
        return Ok(answer.Message);
    }

    [HttpGet("db-test-data")]
    public async Task<IActionResult> GetTestDataFromDB()
    {
        var response = await _chatService.GetTestDBData();
        return Ok(response);
    }
}