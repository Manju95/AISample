namespace AILearning.Configuration;

public class OpenAIOptions
{
    public const string SectionName = "OpenAI";

    public string ApiKey { get; init; } = string.Empty;

    public string BaseUrl { get; init; } = string.Empty;

    public string ChatEndpoint { get; init; } = string.Empty;

    public string Model { get; init; } = string.Empty;

    public double Temperature { get; init; } = 0.2;
}