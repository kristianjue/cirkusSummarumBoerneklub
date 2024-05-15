using Api.Interfaces;
using Api.Repositories;
using Core;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/login")]
public class LoginController : ControllerBase
{
    private readonly ILoginRepository _loginRepositories;

    public LoginController(ILoginRepository loginRepositories)
    {
        _loginRepositories = loginRepositories;
    }

    [HttpPost]
    public ActionResult<Administrator> CreateAccount([FromBody] Administrator administrator)
    {
        try
        {
            _loginRepositories.CreateAccount(administrator);
            return Ok(administrator); // Returnerer den oprettede administrator
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message); // Returnerer fejlbeskeden hvis email allerede findes
        }
    }

    [HttpGet]
    public ActionResult<bool> Login([FromQuery] string email, [FromQuery] string password)
    {
        var adminFromDb = _loginRepositories.VerifyLogin(email, password);
        if (adminFromDb == null)
        {
            return Unauthorized(); // Returnerer 401 hvis login fejler
        }
        return Ok(true); // Returnerer 200 hvis login er succesfuld
    }

    [HttpGet]
    [Route("getuser")]
    public ActionResult<Administrator> GetUser([FromQuery] string email)
    {
        var adminFromDb = _loginRepositories.GetUserByEmail(email);
        if (adminFromDb == null)
        {
            return NotFound(); // Returnerer 404 hvis bruger ikke findes
        }
        return Ok(adminFromDb); // Returnerer brugeren hvis fundet
    }
}
