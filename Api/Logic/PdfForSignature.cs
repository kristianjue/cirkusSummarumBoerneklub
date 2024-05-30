using Core;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace Api.Logic
{
    public class PdfForSignature
    {
        /// <summary>
        /// Ved at bruge QuestPDF biblioteket generer vi en PDF-fil for alle børn på en specifik ansøgnng.
        /// </summary>
        /// <param name="Application"></param>
        /// <returns></returns>
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
        
        /// <summary>
        /// Ved at bruge QuestPDF biblioteket generer vi en PDF-fil for en specifik signatur til frivillige.
        /// </summary>
        /// <param name="Signature"></param>
        /// <returns></returns>
        public IDocument CreateDocumentForSpecifcSignature(Signature signature)
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
                            .Text("Cirkus Summarum")
                            .SemiBold().FontSize(28);

                        page.Content()
                            .PaddingVertical(1, Unit.Centimetre)
                            .Column(x =>
                            {
                                x.Spacing(20);

                                x.Item().Text(text =>
                                {
                                    text.Span("Frivilliges navn:").Bold().FontSize(16);
                                    text.Span($" {signature.YoungVolunteer.Name}").FontSize(16);
                                });
                                
                                x.Item().Text(text =>
                                {
                                    text.Span("Frivilliges KrævNummer:").Bold().FontSize(16);
                                    text.Span($" {signature.YoungVolunteer.KrævNumber}").FontSize(16);
                                });


                                x.Item().LineVertical(3);

                                x.Item().Text(text =>
                                {
                                    text.Span("Forældre:").Bold().FontSize(16);
                                    text.Span($" {signature.Name}").FontSize(16);
                                });
                                x.Item().Text(text =>
                                {
                                    text.Span("Email:").Bold().FontSize(16);
                                    text.Span($" {signature.Email}").FontSize(16);
                                });
                                x.Item().Text(text =>
                                {
                                    text.Span("Telefonnummer:").Bold().FontSize(16);
                                    text.Span($" {signature.PhoneNumber}").FontSize(16);
                                });

                                x.Item().Text(text =>
                                {
                                    text.Span("Kræw nummer på værge:").Bold().FontSize(16);
                                    text.Span($" {signature.KrævNumber ?? signature.Volunteer.KrævNumber}").FontSize(16);
                                });

                                x.Item().LineVertical(6);

                                x.Item().Text(
                                        "Jeg giver hermed tilladelse til, at mit barn må deltage som frivillig på Cirkus Summarum.")
                                    .FontSize(16);

                                string signatureData = signature.YoungVolunteer.Signature.Replace("data:image/png;base64,", "");
                                byte[] image = Convert.FromBase64String(signatureData);
                                x.Item().Image(image);
                            });
                    });
            });
        }
        
        /// <summary>
        /// Ved at bruge QuestPDF biblioteket generer vi en PDF-fil for alle ansøgninger. Dette gør vi ved at bruge en container, som indeholder en side for hver signatur, og for hvert barn.
        /// </summary>
        /// <param name="List<Application>"></param>
        /// <returns></returns>
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
        
        /// <summary>
        /// Ved at bruge QuestPDF biblioteket generer vi en PDF-fil for alle signaturer. Dette gør vi ved at bruge en container, som indeholder en side for hver signatur.
        /// </summary>
        /// <param name="signatures"></param>
        /// <returns></returns>
        public IDocument CreateDocumentForAllSignatures(List<Signature> signatures)
        {
            return Document.Create(container =>
            {
                foreach (Signature signature in signatures)
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
                                    text.Span("Frivilliges navn:").Bold().FontSize(16);
                                    text.Span($" {signature.YoungVolunteer.Name}").FontSize(16);
                                });
                                
                                x.Item().Text(text =>
                                {
                                    text.Span("Frivilliges KrævNummer:").Bold().FontSize(16);
                                    text.Span($" {signature.YoungVolunteer.KrævNumber}").FontSize(16);
                                });


                                x.Item().LineVertical(3);

                                x.Item().Text(text =>
                                {
                                    text.Span("Forældre:").Bold().FontSize(16);
                                    text.Span($" {signature.Name}").FontSize(16);
                                });
                                x.Item().Text(text =>
                                {
                                    text.Span("Email:").Bold().FontSize(16);
                                    text.Span($" {signature.Email}").FontSize(16);
                                });
                                x.Item().Text(text =>
                                {
                                    text.Span("Telefonnummer:").Bold().FontSize(16);
                                    text.Span($" {signature.PhoneNumber}").FontSize(16);
                                });

                                x.Item().Text(text =>
                                {
                                    text.Span("Kræw nummer på værge:").Bold().FontSize(16);
                                    text.Span($" {signature.KrævNumber ?? signature.Volunteer.KrævNumber}").FontSize(16);
                                });

                                x.Item().LineVertical(6);

                                x.Item().Text(
                                        "Jeg giver hermed tilladelse til, at mit barn må deltage som frivillig på Cirkus Summarum.")
                                    .FontSize(16);

                                string signatureData = signature.YoungVolunteer.Signature.Replace("data:image/png;base64,", "");
                                byte[] image = Convert.FromBase64String(signatureData);
                                x.Item().Image(image);
                            });
                        
                    });
                }
            });
                
        }
    }
}
