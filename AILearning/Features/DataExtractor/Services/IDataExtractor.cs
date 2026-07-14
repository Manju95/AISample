namespace AILearning.Features.DataExtractor;

public interface IDataExtractor
{
    Task<string> ExtractTextAsync(Stream pdfStream);
}