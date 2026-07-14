using AILearning.Configuration;
using AILearning.Features.Embedding.Models;
using Microsoft.Extensions.Options;
using Pgvector;

namespace AILearning.Services.Embedings.Services;

public class OllmaEmbedingProvider : IEmbedingProvider
{
    private readonly HttpClient _httpClient;
    private readonly OLLMOptions _options;

    public OllmaEmbedingProvider(HttpClient httpClient, IOptions<OLLMOptions> options)
    {
        _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
        _options = options.Value ?? throw new ArgumentNullException(nameof(options));
    }

    public async Task<Vector> GenerateAsync(string text)
    {
        var request = new EmbedingRequest
        {
            Model = _options.EmbeddingModel,
            Input = text
        };

        var response = await _httpClient.PostAsJsonAsync("/api/embed", request);

        response.EnsureSuccessStatusCode();

        var result = await response.Content.ReadFromJsonAsync<EmbedingResponse>();

        if (result == null || result.Embeddings.Count == 0)
        {
            throw new InvalidOperationException("Embedding generation failed.");
        }

        return new Vector(result.Embeddings[0]);
    }
}