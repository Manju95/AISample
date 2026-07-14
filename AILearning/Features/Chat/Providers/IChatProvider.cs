using AILearning.Features.Chat.Models;
using AILearning.Models;

namespace AILearning.Features.Chat.Providers;

public interface IChatProvider
{
    Task<ChatResponse> GetAIResponseAsync(ChatRequest request);
}