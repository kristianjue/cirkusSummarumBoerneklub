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
    [Route("getallactive")]
    public ActionResult<List<City>> GetAllActiveCity()
    {
        var settingsFromDb = _settingsRepository.GetAllActiveCity();
        if (settingsFromDb == null)
        {
            return NotFound();
        }
        return Ok(settingsFromDb);
    }

    [HttpGet]
    [Route("get/{cityName}")]
    public ActionResult<City> GetCityByName(string cityName)
    {
        var settingsFromDb = _settingsRepository.GetCityByName(cityName);
        if (settingsFromDb == null)
        {
            return NotFound();
        }
        return Ok(settingsFromDb);
    }

    [HttpPut]
    [Route("update/{cityName}")]
    public ActionResult<City> UpdateCity(string cityName, [FromBody] City city)
    {
        try
        {
            var existingCity = _settingsRepository.GetCityByName(cityName);
            if (existingCity == null)
            {
                return NotFound();
            }

            // Update the city details
            existingCity.Name = city.Name;
            existingCity.Weeks = city.Weeks;
            existingCity.OpenForRegistration = city.OpenForRegistration;

            _settingsRepository.UpdateCity(existingCity);
            return Ok(existingCity);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpDelete]
    [Route("delete/{cityName}")]
    public ActionResult DeleteCity(string cityName)
    {
        try
        {
            _settingsRepository.DeleteCity(cityName);
            return Ok();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}
