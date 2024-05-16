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
    
    
}