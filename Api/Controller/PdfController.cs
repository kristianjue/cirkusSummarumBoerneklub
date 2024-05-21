using System.Net;
using Microsoft.AspNetCore.Mvc;
using Api.Logic;
using Api.Repository;
using Core;
using QuestPDF.Fluent;


[ApiController]
[Route("api/pdf")]
public class PdfController : ControllerBase
{
    private readonly PdfForSignature _pdfForSignature;
    private readonly IApplicationRepository _applicationRepository;

    public PdfController(PdfForSignature pdfForSignature, IApplicationRepository applicationRepository)
    {
        _pdfForSignature = pdfForSignature;
        _applicationRepository = applicationRepository;
    }
    
    [HttpGet]
    [Route("get/{ApplicationId}")]
    public async Task<ActionResult> GeneratePdf(string ApplicationId)
    {
        // Fetch or create the Signature object using the ApplicationId
        var application =  _applicationRepository.GetApplicationById(ApplicationId);
        
        // Use the injected service to create a document
        var document = _pdfForSignature.CreateDocumentForSpecifcApplication(application);
        
        // Generer PDF-fil og returner den som et svar
        using (var stream = new MemoryStream())
        {
            document.GeneratePdf(stream);
            stream.Position = 0;
            return File(stream.ToArray(), "application/pdf", $"{ApplicationId}.pdf");
        }
    }
    
    [HttpGet]
    [Route("get-all")]
    public async Task<ActionResult> GenerateAll()
    {
        // Fetch or create the Signature object using the ApplicationId
        List<Application> applications = _applicationRepository.GetAllApplications();
        
        while (applications.Count == 0)
        {
            await Task.Delay(10);
        }
        
        // Use the injected service to create a document
        var document = _pdfForSignature.CreateDocumentForAllApplication(applications);

        // Generate PDF file and return it as a response
        using (var stream = new MemoryStream())
        {
            document.GeneratePdf(stream);
            stream.Position = 0;
            return File(stream.ToArray(), "application/pdf", "all-applications.pdf");
        }
    }
}