using Api.Interfaces;
using Core;
using Microsoft.AspNetCore.Mvc;
using System;

[ApiController]
[Route("api/City")]
public class CityController : ControllerBase
{
    private readonly ICityRepository _settingsRepository;

    public CityController(ICityRepository settingsRepository)
    {
        _settingsRepository = settingsRepository;
    }

    [HttpPost]
    [Route("create")]
    public ActionResult<City> CreateCity([FromBody] City city)
    {
        try
        {
            _settingsRepository.CreateCity(city);
            return Ok(city);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
    

    [HttpGet]
    [Route("getall")]
    public ActionResult<List<City>> GetAllCity()
    {
        var settingsFromDb = _settingsRepository.GetAllCity();
        if (settingsFromDb == null)
        {
            return NotFound();
        }
        return Ok(settingsFromDb);
    }

    [HttpGet]
    [Route("get/{cityId}")]
    public ActionResult<City> GetCityById(string cityId)
    {
        var settingsFromDb = _settingsRepository.GetCityById(cityId);
        if (settingsFromDb == null)
        {
            return NotFound();
        }
        return Ok(settingsFromDb);
    }

    [HttpPut]
    [Route("update/{settingsId}")]
    public ActionResult<City> UpdateCity(string settingsId, [FromBody] City city)
    {
        try
        {
            var existingSettings = _settingsRepository.GetCityById(settingsId);
            if (existingSettings == null)
            {
                Console.WriteLine($"System settings not found: {settingsId}");
                return NotFound();
            }

            // Ensure that the settings ID in the URL matches the settings ID in the request body
            if (city.Name != settingsId)
            {
                Console.WriteLine("System settings ID mismatch");
                return BadRequest("System settings ID mismatch");
            }

            // Update the system settings details
            existingSettings.Weeks = city.Weeks;
            existingSettings.OpenForRegistration = city.OpenForRegistration;

            _settingsRepository.UpdateCity(existingSettings);
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
    public ActionResult DeleteCity(string settingsId)
    {
        try
        {
            _settingsRepository.DeleteCity(settingsId);
            return Ok();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}
