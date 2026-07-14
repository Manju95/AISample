using System.Text;
using UglyToad.PdfPig;

namespace AILearning.Features.DataExtractor;

public class PDFDataExtractor : IDataExtractor
{
    public Task<string> ExtractTextAsync(Stream pdfStream)
    {
        
        using var document = PdfDocument.Open(pdfStream);
        var builder = new StringBuilder();
        foreach (var page in document.GetPages())
        {
            builder.AppendLine(page.Text);
        }
        return Task.FromResult(builder.ToString());
    }
}