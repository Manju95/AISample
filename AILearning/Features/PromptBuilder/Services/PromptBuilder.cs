using System.Text;
using AILearning.Features.Chat.Models;
using AILearning.Features.VectorSearch.Models;
using AILearning.Models;

namespace AILearning.Features.Prompt.Services;

public class PromptBuilder : IPromptBuilder
{
    public List<ChatMessage> Build(string question, IEnumerable<SearchResult> chunks)
    {
        var messages = new List<ChatMessage>();
        var sb = new StringBuilder();
        var modelMessage = new ChatMessage
        {
            Role = "system",
            Content = @"You are an AI assistant.mAnswer ONLY from the provided context.
                        If the answer isn't present, say:
                        I couldn't find that information."
        };

        foreach (var chunk in chunks)
        {
            sb.Append(chunk.Content);
        }
        var contextMessage = new ChatMessage
        {
            Role = "user",
            Content = @$"Context: {sb.ToString()}\n Question: {question}"
        };
        messages.Add(modelMessage);
        messages.Add(contextMessage);

        return messages;
    }
}