using AILearning.Features.Chat.Models;
using AILearning.Features.VectorSearch.Models;
using AILearning.Models;

namespace AILearning.Features.Prompt.Services;

public interface IPromptBuilder
{
    List<ChatMessage> Build(string question, IEnumerable<SearchResult> chunks);
}