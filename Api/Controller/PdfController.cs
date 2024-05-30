using System.Net;
using System.Web;
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
    private readonly ISignatureRepository _signatureRepository;

    public PdfController(PdfForSignature pdfForSignature, IApplicationRepository applicationRepository, ISignatureRepository signatureRepository)
    {
        _pdfForSignature = pdfForSignature;
        _applicationRepository = applicationRepository;
        _signatureRepository = signatureRepository;
    }
    
    [HttpGet]
    [Route("get/{ApplicationId}")]
    public async Task<ActionResult> GeneratePdf(string ApplicationId)
    {
        
        // Henter en specifik ansøgning fra databasen ved hjælp af ApplicationId
        var application =  _applicationRepository.GetApplicationById(ApplicationId);
        
        // Opretter en PDF-fil ud fra ansøgningen
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
    [Route("get-signature/{SignatureId}")]
    public async Task<ActionResult> GenerateSignaturePdf(string SignatureId)
    {
        
        // Henter en specifik signatur fra databasen ved hjælp af SignatureId
        var signature = _signatureRepository.GetSignatureById(SignatureId);
        
        // Opretter en PDF-fil ud fra signaturen
        var document = _pdfForSignature.CreateDocumentForSpecifcSignature(signature);
        
        // Generer PDF-fil og returner den som et svar
        using (var stream = new MemoryStream())
        {
            document.GeneratePdf(stream);
            stream.Position = 0;
            return File(stream.ToArray(), "application/pdf", $"{SignatureId}.pdf");
        }
    }
    
    [HttpGet]
    [Route("get-all/applications/{city}/{period}/{status}")]
    
    public async Task<ActionResult> GenerateAll(string city, string period, string status)
    {
        // Dekoder URL-parametrene
        string encodedCity = city;
        city = HttpUtility.UrlDecode(encodedCity);
        
        string encodedPeriod = period;
        period = HttpUtility.UrlDecode(encodedPeriod);
        
        string encodedStatus = status;
        status = HttpUtility.UrlDecode(encodedStatus);
        
        // Henter alle ansøgninger fra databasen, som opfylder de givne filtre
        var allApplications = _applicationRepository.GetApplicationsByfilter(city, period, status);
        
        // Opretter en PDF-fil ud fra de fundne ansøgninger
        var document = _pdfForSignature.CreateDocumentForAllApplication(allApplications);
        
        
        // Generer PDF-fil og returner den som et svar
        using (var stream = new MemoryStream())
        {
            document.GeneratePdf(stream);
            stream.Position = 0;
            return File(stream.ToArray(), "application/pdf", $"filtered-applications-{DateTime.Now}.pdf");
        }
        
    }
    
    [HttpGet]
    [Route("get-all/signatures")]
    
    public async Task<ActionResult> GenerateAllSignatures()
    {
        // Henter alle signaturer fra databasen
        var allSignatures = _signatureRepository.GetAllSignatures();

        
        // Opretter en PDF-fil ud fra de fundne signaturer
        var document = _pdfForSignature.CreateDocumentForAllSignatures(allSignatures);
        
        
        // Generer PDF-fil og returner den som et svar
        using (var stream = new MemoryStream())
        {
            document.GeneratePdf(stream);
            stream.Position = 0;
            return File(stream.ToArray(), "application/pdf", $"all-signatures-{DateTime.Now}.pdf");
        }
        
    }
    
}