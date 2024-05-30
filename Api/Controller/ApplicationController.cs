using Api.Interfaces;
using Api.Repositories;
using Api.Repository;
using Core;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/application")]

// ApplicationController er en ControllerBase, der h�ndterer HTTP-anmodninger relateret til Application.
public class ApplicationController : ControllerBase
{
    // Interface til ApplicationRepository for dataadgang.
    private readonly IApplicationRepository _applicationRepository;

    // Constructor til at initialisere ApplicationController med ApplicationRepository.
    public ApplicationController(IApplicationRepository applicationRepository)
    {
        _applicationRepository = applicationRepository;
    }

    // HTTP POST metode til at oprette en ny ans�gning.
    [HttpPost]
    [Route("create")]
    public ActionResult<Application> CreateApplication([FromBody] Application application)
    {
        try
        {
            // Kalder CreateApplication metoden fra ApplicationRepository for at oprette en ny ans�gning.
            _applicationRepository.CreateApplication(application);
            // Kalder ApplicationSent metoden fra Api.Logic.Email for at sende en e-mail notifikation.
            Api.Logic.Email.ApplicationSent(application);
            // Returnerer 200 OK status med den oprettede ans�gning.
            return Ok(application);
        }
        catch (Exception ex)
        {
            // Returnerer 400 Bad Request status med fejlmeddelelsen, hvis der opst�r en undtagelse.
            return BadRequest(ex.Message);
        }
    }

    // HTTP GET metode til at hente alle ans�gninger.
    [HttpGet]
    [Route("getall")]
    public ActionResult<Application> GetApplication()
    {
        // Henter alle ans�gninger fra ApplicationRepository.
        var applicationFromDb = _applicationRepository.GetAllApplications();
        // Hvis ingen ans�gninger findes, returneres 404 Not Found status.
        if (applicationFromDb == null)
        {
            return NotFound();
        }
        // Returnerer 200 OK status med listen over ans�gninger.
        return Ok(applicationFromDb);
    }

    // HTTP GET metode til at hente en ans�gning baseret p� ans�gnings-ID.
    [HttpGet]
    [Route("get/{applicationId}")]
    public ActionResult<Application> GetApplicationById(string applicationId)
    {
        // Henter en ans�gning baseret p� ans�gnings-ID fra ApplicationRepository.
        var applicationFromDb = _applicationRepository.GetApplicationById(applicationId);
        // Hvis ans�gningen ikke findes, returneres 404 Not Found status.
        if (applicationFromDb == null)
        {
            return NotFound();
        }
        // Returnerer 200 OK status med den fundne ans�gning.
        return Ok(applicationFromDb);
    }

    // HTTP PUT metode til at opdatere en eksisterende ans�gning.
    [HttpPut]
    [Route("update/{applicationId}")]
    public ActionResult<Application> UpdateApplication(string applicationId, [FromBody] Application application)
    {
        try
        {
            // Henter den eksisterende ans�gning baseret p� ans�gnings-ID fra ApplicationRepository.
            var existingApplication = _applicationRepository.GetApplicationById(applicationId);
            // Hvis ans�gningen ikke findes, returneres 404 Not Found status.
            if (existingApplication == null)
            {
                Console.WriteLine($"Application not found: {applicationId}");
                return NotFound();
            }

            // Sikrer at ans�gnings-ID'et i URL'en matcher ans�gnings-ID'et i foresp�rgselsbody'en.
            if (application.Id != applicationId)
            {
                Console.WriteLine("Application ID mismatch");
                return BadRequest("Application ID mismatch");
            }

            // Opdaterer ans�gningsdetaljerne.
            existingApplication.Status = application.Status;
            existingApplication.City = application.City;
            existingApplication.Priority1 = application.Priority1;
            existingApplication.Priority2 = application.Priority2;
            existingApplication.Volunteer = application.Volunteer;

            // Kalder UpdateApplication metoden fra ApplicationRepository for at gemme �ndringerne.
            _applicationRepository.UpdateApplication(existingApplication);
            Console.WriteLine($"Successfully updated application: {applicationId}");
            // Returnerer 200 OK status med den opdaterede ans�gning.
            return Ok(existingApplication);
        }
        catch (Exception ex)
        {
            // Returnerer 400 Bad Request status med fejlmeddelelsen, hvis der opst�r en undtagelse.
            Console.WriteLine($"Exception updating application: {ex.Message}");
            return BadRequest(ex.Message);
        }
    }
}
