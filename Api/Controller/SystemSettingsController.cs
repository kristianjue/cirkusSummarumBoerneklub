using Api.Interfaces;
using Core;
using Microsoft.AspNetCore.Mvc;
using System;

[ApiController]
[Route("api/City")]
// CityController er en ControllerBase, der håndterer HTTP-anmodninger relateret til City.
public class CityController : ControllerBase
{
    // Interface til CityRepository for dataadgang.
    private readonly ICityRepository _settingsRepository;

    // Constructor til at initialisere CityController med CityRepository.
    public CityController(ICityRepository settingsRepository)
    {
        _settingsRepository = settingsRepository;
    }

    // HTTP POST metode til at oprette en ny by.
    [HttpPost]
    [Route("create")]
    public ActionResult<City> CreateCity([FromBody] City city)
    {
        try
        {
            // Kalder CreateCity metoden fra CityRepository for at indsætte en ny by.
            _settingsRepository.CreateCity(city);
            // Returnerer 200 OK status med den oprettede by.
            return Ok(city);
        }
        catch (Exception ex)
        {
            // Returnerer 400 Bad Request status med fejlmeddelelsen, hvis der opstår en undtagelse.
            return BadRequest(ex.Message);
        }
    }

    // HTTP GET metode til at hente alle byer.
    [HttpGet]
    [Route("getall")]
    public ActionResult<List<City>> GetAllCity()
    {
        // Henter alle byer fra CityRepository.
        var settingsFromDb = _settingsRepository.GetAllCity();
        // Hvis ingen byer findes, returneres 404 Not Found status.
        if (settingsFromDb == null)
        {
            return NotFound();
        }
        // Returnerer 200 OK status med listen over byer.
        return Ok(settingsFromDb);
    }

    // HTTP GET metode til at hente alle aktive byer (åben for registrering).
    [HttpGet]
    [Route("getallactive")]
    public ActionResult<List<City>> GetAllActiveCity()
    {
        // Henter alle aktive byer fra CityRepository.
        var settingsFromDb = _settingsRepository.GetAllActiveCity();
        // Hvis ingen aktive byer findes, returneres 404 Not Found status.
        if (settingsFromDb == null)
        {
            return NotFound();
        }
        // Returnerer 200 OK status med listen over aktive byer.
        return Ok(settingsFromDb);
    }

    // HTTP GET metode til at hente en by baseret på navn.
    [HttpGet]
    [Route("get/{cityName}")]
    public ActionResult<City> GetCityByName(string cityName)
    {
        // Henter en by baseret på navn fra CityRepository.
        var settingsFromDb = _settingsRepository.GetCityByName(cityName);
        // Hvis byen ikke findes, returneres 404 Not Found status.
        if (settingsFromDb == null)
        {
            return NotFound();
        }
        // Returnerer 200 OK status med den fundne by.
        return Ok(settingsFromDb);
    }

    // HTTP PUT metode til at opdatere en eksisterende by.
    [HttpPut]
    [Route("update/{cityName}")]
    public ActionResult<City> UpdateCity(string cityName, [FromBody] City city)
    {
        try
        {
            // Henter den eksisterende by baseret på navn fra CityRepository.
            var existingCity = _settingsRepository.GetCityByName(cityName);
            // Hvis byen ikke findes, returneres 404 Not Found status.
            if (existingCity == null)
            {
                return NotFound();
            }

            // Opdaterer byens detaljer.
            existingCity.Name = city.Name;
            existingCity.Weeks = city.Weeks;
            existingCity.OpenForRegistration = city.OpenForRegistration;

            // Kalder UpdateCity metoden fra CityRepository for at gemme ændringerne.
            _settingsRepository.UpdateCity(existingCity);
            // Returnerer 200 OK status med den opdaterede by.
            return Ok(existingCity);
        }
        catch (Exception ex)
        {
            // Returnerer 400 Bad Request status med fejlmeddelelsen, hvis der opstår en undtagelse.
            return BadRequest(ex.Message);
        }
    }

    // HTTP DELETE metode til at slette en by baseret på navn.
    [HttpDelete]
    [Route("delete/{cityName}")]
    public ActionResult DeleteCity(string cityName)
    {
        try
        {
            // Kalder DeleteCity metoden fra CityRepository for at slette byen.
            _settingsRepository.DeleteCity(cityName);
            // Returnerer 200 OK status ved succesfuld sletning.
            return Ok();
        }
        catch (Exception ex)
        {
            // Returnerer 400 Bad Request status med fejlmeddelelsen, hvis der opstår en undtagelse.
            return BadRequest(ex.Message);
        }
    }
}

