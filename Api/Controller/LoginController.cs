using Api.Interfaces;
using Api.Repositories;
using Core;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/login")]
// LoginController er en ControllerBase, der håndterer HTTP-anmodninger relateret til login og kontooprettelse.
public class LoginController : ControllerBase
{
    // Interface til LoginRepository for dataadgang.
    private readonly ILoginRepository _loginRepositories;

    // Constructor til at initialisere LoginController med LoginRepository.
    public LoginController(ILoginRepository loginRepositories)
    {
        _loginRepositories = loginRepositories;
    }

    // HTTP POST metode til at oprette en ny administrator konto.
    [HttpPost]
    public ActionResult<Administrator> CreateAccount([FromBody] Administrator administrator)
    {
        try
        {
            // Kalder CreateAccount metoden fra LoginRepository for at oprette en ny administrator.
            _loginRepositories.CreateAccount(administrator);
            // Returnerer 200 OK status med den oprettede administrator.
            return Ok(administrator);
        }
        catch (Exception ex)
        {
            // Returnerer 400 Bad Request status med fejlmeddelelsen, hvis der opstår en undtagelse.
            return BadRequest(ex.Message);
        }
    }

    // HTTP GET metode til at verificere loginoplysninger.
    [HttpGet]
    public ActionResult<bool> Login([FromQuery] string email, [FromQuery] string password)
    {
        // Kalder VerifyLogin metoden fra LoginRepository for at verificere loginoplysninger.
        var adminFromDb = _loginRepositories.VerifyLogin(email, password);
        // Hvis loginoplysningerne ikke matcher, returneres false.
        if (adminFromDb == null)
        {
            return Ok(false);
        }
        // Returnerer true, hvis login er succesfuld.
        return Ok(true);
    }

    // HTTP GET metode til at hente en bruger baseret på email.
    [HttpGet]
    [Route("getuser")]
    public ActionResult<Administrator> GetUser([FromQuery] string email)
    {
        // Kalder GetUserByEmail metoden fra LoginRepository for at hente en bruger baseret på email.
        var adminFromDb = _loginRepositories.GetUserByEmail(email);
        // Hvis brugeren ikke findes, returneres 404 Not Found status.
        if (adminFromDb == null)
        {
            return NotFound();
        }
        // Returnerer 200 OK status med den fundne bruger.
        return Ok(adminFromDb);
    }
}

