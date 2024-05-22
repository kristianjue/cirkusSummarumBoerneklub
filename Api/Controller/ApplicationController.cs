using Api.Interfaces;
using Api.Repositories;
using Api.Repository;
using Core;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/application")]

public class ApplicationController : ControllerBase
{
    private readonly IApplicationRepository _applicationRepository;
    
    public ApplicationController(IApplicationRepository applicationRepository)
    {
        _applicationRepository = applicationRepository;
    }
    
    [HttpPost]
    [Route("create")]
    public ActionResult<Application> CreateApplication([FromBody] Application application)
    {
        try
        {
            _applicationRepository.CreateApplication(application);
            new Api.Logic.Email().ApplicationSent(application);
            return Ok(application);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet]
    [Route("getall")]
    public ActionResult<Application> GetApplication()
    {
        var applicationFromDb = _applicationRepository.GetAllApplications();
        if (applicationFromDb == null)
        {
            return NotFound();
        }
        return Ok(applicationFromDb);
    }
    
    [HttpGet]
    [Route("get/{applicationId}")]
    public ActionResult<Application> GetApplicationById(string applicationId)
    {
        var applicationFromDb = _applicationRepository.GetApplicationById(applicationId);
        if (applicationFromDb == null)
        {
            return NotFound();
        }
        return Ok(applicationFromDb);
    }

    [HttpPut]
    [Route("update/{applicationId}")]
    public ActionResult<Application> UpdateApplication(string applicationId, [FromBody] Application application)
    {
        try
        {
            var existingApplication = _applicationRepository.GetApplicationById(applicationId);
            if (existingApplication == null)
            {
                Console.WriteLine($"Application not found: {applicationId}");
                return NotFound();
            }

            // Ensure that the application ID in the URL matches the application ID in the request body
            if (application.Id != applicationId)
            {
                Console.WriteLine("Application ID mismatch");
                return BadRequest("Application ID mismatch");
            }

            // Update the application details
            existingApplication.Status = application.Status;
            existingApplication.Location = application.Location;
            existingApplication.Priority1 = application.Priority1;
            existingApplication.Priority2 = application.Priority2;
            existingApplication.Volunteer = application.Volunteer;

            _applicationRepository.UpdateApplication(existingApplication);
            Console.WriteLine($"Successfully updated application: {applicationId}");
            return Ok(existingApplication);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Exception updating application: {ex.Message}");
            return BadRequest(ex.Message);
        }
    }



}