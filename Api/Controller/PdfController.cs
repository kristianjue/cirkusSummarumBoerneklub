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
    [Route("get-signature/{SignatureId}")]
    public async Task<ActionResult> GenerateSignaturePdf(string SignatureId)
    {
        
        // Fetch or create the Signature object using the ApplicationId
        var signature = _signatureRepository.GetSignatureById(SignatureId);
        
        // Use the injected service to create a document
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
        string encodedCity = city;
        city = HttpUtility.UrlDecode(encodedCity);
        
        string encodedPeriod = period;
        period = HttpUtility.UrlDecode(encodedPeriod);
        
        string encodedStatus = status;
        status = HttpUtility.UrlDecode(encodedStatus);
        
        var allApplications = _applicationRepository.GetApplicationsByfilter(city, period, status);
        
        var document = _pdfForSignature.CreateDocumentForAllApplication(allApplications);
        
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
        var allSignatures = _signatureRepository.GetAllSignatures();

        var document = _pdfForSignature.CreateDocumentForAllSignatures(allSignatures);
        
        using (var stream = new MemoryStream())
        {
            document.GeneratePdf(stream);
            stream.Position = 0;
            return File(stream.ToArray(), "application/pdf", $"all-signatures-{DateTime.Now}.pdf");
        }
        
    }
    
}