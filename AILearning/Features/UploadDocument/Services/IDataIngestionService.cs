namespace AILearning.Features.UploadDocument.Services;

public interface IDataIngestionService
{
    Task UploadAsync(IFormFile pdf);
}