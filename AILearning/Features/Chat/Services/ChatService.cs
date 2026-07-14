using AILearning.Context;
using AILearning.Features.Chat.Models;
using AILearning.Models;
using AILearning.Features.Chat.Providers;
using AILearning.Features.Prompt.Services;
using AILearning.Features.VectorSearch.Services;
using Microsoft.EntityFrameworkCore;

namespace AILearning.Features.Chat.Services;

public class ChatService : IChatService
{
    private readonly IChatProvider _chatProvider;
    private readonly AppDbContext _context;
    private readonly IRAGService _ragService;
    private readonly IPromptBuilder _promptBuilder;

    public ChatService(IChatProvider chatProvider, AppDbContext context, IRAGService ragService, IPromptBuilder promptBuilder)
    {
        _chatProvider = chatProvider ?? throw new ArgumentNullException(nameof(chatProvider));
        _context = context ?? throw new ArgumentNullException(nameof(context));
        _ragService = ragService ??  throw new ArgumentNullException(nameof(ragService));
        _promptBuilder = promptBuilder ?? throw new ArgumentNullException(nameof(promptBuilder));
    }

    public async Task<ChatResponse> AskQuestionAsync(ChatRequest request)
    {
        ChatResponse response = null;

        if (!request.IsPersonal)
            response = await _chatProvider.GetAIResponseAsync(request);
        else
        {
            var question = request.Messages.FirstOrDefault().Content;
            var ragData = await _ragService.SearchAsync(question);
            var messages = _promptBuilder.Build(question, ragData);

            var modelRequest = new ChatRequest
            {
                Messages = messages,
                Stream = false
            };
            response = await _chatProvider.GetAIResponseAsync(modelRequest);
        }
        
        return response;
    }

    public async Task<IEnumerable<Test>> GetTestDBData()
    {
        return await _context.Test.ToListAsync();
    }
}