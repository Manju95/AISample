using AILearning.Configuration;
using AILearning.Features.Chat.Models;
using AILearning.Models;
using Microsoft.Extensions.Options;

namespace AILearning.Features.Chat.Providers;

public class OLLMAProvider : IChatProvider
{
    private readonly HttpClient _httpClient;
    private readonly OLLMOptions _options;

    public OLLMAProvider(HttpClient httpClient, IOptions<OLLMOptions> options)
    {
        _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
        _options = options.Value ?? throw new ArgumentNullException(nameof(options));
    }
    
    public async Task<ChatResponse> GetAIResponseAsync(ChatRequest request)
    {
        var modelRequest = request with { Model = _options.ChatModel, Stream = false};
        
        var response = await _httpClient.PostAsJsonAsync(_options.ChatEndpoint, modelRequest);
        
        response.EnsureSuccessStatusCode();
        
        Console.WriteLine(await response.Content.ReadAsStringAsync());
        
        return await response.Content.ReadFromJsonAsync<ChatResponse>();
    }
}