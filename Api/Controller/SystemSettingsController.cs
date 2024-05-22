using Api.Interfaces;
using Core;
using Microsoft.AspNetCore.Mvc;
using System;

[ApiController]
[Route("api/systemsettings")]
public class SystemSettingsController : ControllerBase
{
    private readonly ISystemSettingsRepository _settingsRepository;

    public SystemSettingsController(ISystemSettingsRepository settingsRepository)
    {
        _settingsRepository = settingsRepository;
    }

    [HttpPost]
    [Route("create")]
    public ActionResult<SystemSettings> CreateSystemSettings([FromBody] SystemSettings settings)
    {
        try
        {
            _settingsRepository.CreateSystemSettings(settings);
            return Ok(settings);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet]
    [Route("getall")]
    public ActionResult<List<SystemSettings>> GetAllSystemSettings()
    {
        var settingsFromDb = _settingsRepository.GetAllSystemSettings();
        if (settingsFromDb == null)
        {
            return NotFound();
        }
        return Ok(settingsFromDb);
    }

    [HttpGet]
    [Route("get/{settingsId}")]
    public ActionResult<SystemSettings> GetSystemSettingsById(string settingsId)
    {
        var settingsFromDb = _settingsRepository.GetSystemSettingsById(settingsId);
        if (settingsFromDb == null)
        {
            return NotFound();
        }
        return Ok(settingsFromDb);
    }

    [HttpPut]
    [Route("update/{settingsId}")]
    public ActionResult<SystemSettings> UpdateSystemSettings(string settingsId, [FromBody] SystemSettings settings)
    {
        try
        {
            var existingSettings = _settingsRepository.GetSystemSettingsById(settingsId);
            if (existingSettings == null)
            {
                Console.WriteLine($"System settings not found: {settingsId}");
                return NotFound();
            }

            // Ensure that the settings ID in the URL matches the settings ID in the request body
            if (settings.Id != settingsId)
            {
                Console.WriteLine("System settings ID mismatch");
                return BadRequest("System settings ID mismatch");
            }

            // Update the system settings details
            existingSettings.Locations = settings.Locations;
            existingSettings.OpenForRegistration = settings.OpenForRegistration;

            _settingsRepository.UpdateSystemSettings(existingSettings);
            Console.WriteLine($"Successfully updated system settings: {settingsId}");
            return Ok(existingSettings);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Exception updating system settings: {ex.Message}");
            return BadRequest(ex.Message);
        }
    }

    [HttpDelete]
    [Route("delete/{settingsId}")]
    public ActionResult DeleteSystemSettings(string settingsId)
    {
        try
        {
            _settingsRepository.DeleteSystemSettings(settingsId);
            return Ok();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}
