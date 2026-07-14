namespace AILearning.Configuration;

public class OLLMOptions
{
    public const string SectionName = "OLLMA";

    public string BaseUrl { get; init; } = string.Empty;

    public string ChatEndpoint { get; init; } = string.Empty;

    public string ChatModel { get; init; } = string.Empty;
    public string EmbeddingModel { get; init; } = string.Empty;

    public double Temperature { get; init; } = 0.2;
}