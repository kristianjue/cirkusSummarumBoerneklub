using Core;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace Api.Logic
{
    public class PdfForSignature
    {
        public IDocument CreateDocumentForSpecifcApplication(Application application)
        {
            return Document.Create(container =>
            {
                foreach (Child child in application.Volunteer.Children)
                {
                    container.Page(page =>
                    {
                        page.Size(PageSizes.A4);
                        page.Margin(2, Unit.Centimetre);
                        page.PageColor(Colors.White);
                        page.DefaultTextStyle(x => x.FontSize(20));

                        page.Header()
                            .Text("Cirkus Summarum")
                            .SemiBold().FontSize(28);

                        page.Content()
                            .PaddingVertical(1, Unit.Centimetre)
                            .Column(x =>
                            {
                                x.Spacing(20);

                                x.Item().Text(text =>
                                {
                                    text.Span("Barn:").Bold().FontSize(16);
                                    text.Span($" {child.Name}").FontSize(16);
                                });


                                x.Item().LineVertical(3);

                                x.Item().Text(text =>
                                {
                                    text.Span("Forældre / værge:").Bold().FontSize(16);
                                    text.Span($" {application.Volunteer.Name}").FontSize(16);
                                });
                                x.Item().Text(text =>
                                {
                                    text.Span("Email:").Bold().FontSize(16);
                                    text.Span($" {application.Volunteer.Email}").FontSize(16);
                                });
                                x.Item().Text(text =>
                                {
                                    text.Span("Telefonnummer:").Bold().FontSize(16);
                                    text.Span($" {application.Volunteer.PhoneNumber}").FontSize(16);
                                });
                                x.Item().Text(text =>
                                {
                                    text.Span("Kræw nummer:").Bold().FontSize(16);
                                    text.Span($" {application.Volunteer.KrævNumber}").FontSize(16);
                                });

                                x.Item().LineVertical(6);

                                x.Item().Text(
                                        "Jeg giver hermed tilladelse til, at mit barn må deltage som frivillig på Cirkus Summarum.")
                                    .FontSize(16);

                                string signatureData = child.Signature.Replace("data:image/png;base64,", "");
                                byte[] image = Convert.FromBase64String(signatureData);
                                x.Item().Image(image);
                            });
                    });
                }
            });
        }
        
        public IDocument CreateDocumentForAllApplication(List<Application> allApplications)
        {
            return Document.Create(container =>
            {
                foreach (Application application in allApplications)
                {
                        foreach (Child child in application.Volunteer.Children)
                        { container.Page(page =>
                        {
                            page.Size(PageSizes.A4);
                            page.Margin(2, Unit.Centimetre);
                            page.PageColor(Colors.White);
                            page.DefaultTextStyle(x => x.FontSize(20));

                            page.Header()
                                .Text("Cirkus Summarum")
                                .SemiBold().FontSize(28);

                            page.Content()
                                .PaddingVertical(1, Unit.Centimetre)
                                .Column(x =>
                                {
                                    x.Spacing(20);

                                    x.Item().Text(text =>
                                    {
                                        text.Span("Barn:").Bold().FontSize(16);
                                        text.Span($" {child.Name}").FontSize(16);
                                    });


                                    x.Item().LineVertical(3);

                                    x.Item().Text(text =>
                                    {
                                        text.Span("Forældre / værge:").Bold().FontSize(16);
                                        text.Span($" {application.Volunteer.Name}").FontSize(16);
                                    });
                                    x.Item().Text(text =>
                                    {
                                        text.Span("Email:").Bold().FontSize(16);
                                        text.Span($" {application.Volunteer.Email}").FontSize(16);
                                    });
                                    x.Item().Text(text =>
                                    {
                                        text.Span("Telefonnummer:").Bold().FontSize(16);
                                        text.Span($" {application.Volunteer.PhoneNumber}").FontSize(16);
                                    });
                                    x.Item().Text(text =>
                                    {
                                        text.Span("Kræw nummer:").Bold().FontSize(16);
                                        text.Span($" {application.Volunteer.KrævNumber}").FontSize(16);
                                    });

                                    x.Item().LineVertical(6);

                                    x.Item().Text(
                                            "Jeg giver hermed tilladelse til, at mit barn må deltage som frivillig på Cirkus Summarum.")
                                        .FontSize(16);

                                    string signatureData = child.Signature.Replace("data:image/png;base64,", "");
                                    byte[] image = Convert.FromBase64String(signatureData);
                                    x.Item().Image(image);
                                });
                        });
                    }
                }
            });
        }
    }
}
