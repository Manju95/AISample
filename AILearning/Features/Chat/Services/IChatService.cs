using AILearning.Features.Chat.Models;
using AILearning.Models;

namespace AILearning.Features.Chat.Services;

public interface IChatService
{
    Task<ChatResponse> AskQuestionAsync(ChatRequest request);
    Task<IEnumerable<Test>> GetTestDBData();
}