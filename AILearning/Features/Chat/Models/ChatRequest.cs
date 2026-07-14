namespace AILearning.Features.Chat.Models;

public record ChatRequest
{
    public string? Model { get; set; }
    public List<ChatMessage> Messages { get; set; } = [];
    public bool Stream { get; set; }
    public bool IsPersonal { get; set; } = false;
}