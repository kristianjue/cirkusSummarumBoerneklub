using Microsoft.AspNetCore.Mvc;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;


[ApiController]
[Route("api/pdf")]
public class PdfController : ControllerBase
{
    [HttpGet]
    [Route("create")]
    public IResult GeneratePdf()
    {
        // use any method to create a document, e.g.: injected service
        var document = CreateDocument();
        
        // generate PDF file and return it as a response
        var pdf = document.GeneratePdf();
        return Results.File(pdf, "application/pdf", "hello-world.pdf");
    }

    QuestPDF.Infrastructure.IDocument CreateDocument()
    {
        return Document.Create(container =>
        {
            container.Page(page =>
            {
                page.Size(PageSizes.A4);
                page.Margin(2, Unit.Centimetre);
                page.PageColor(Colors.White);
                page.DefaultTextStyle(x => x.FontSize(20));

                page.Header()
                    .Text("Cirkus Summarum - Børneklub")
                    .SemiBold().FontSize(28);

                page.Content()
                    .PaddingVertical(1, Unit.Centimetre)
                    .Column(x =>
                    {
                        x.Spacing(20);

                        x.Item().Text("Barn: ").Bold().FontSize(20);
                        
                        x.Item().Image(Placeholders.Image(200, 100));
                    });

                page.Footer()
                    .AlignCenter()
                    .Text(x =>
                    {
                        x.Span("Page ");
                        x.CurrentPageNumber();
                    });
            });
        });
    }
}