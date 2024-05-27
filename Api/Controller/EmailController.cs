using System.Net;
using Microsoft.AspNetCore.Mvc;
using Api.Logic;
using Core;


[ApiController]
[Route("api/email")]
public class EmailController : ControllerBase
{
    private readonly Email _email;

    public EmailController(Email email)
    {
        _email = email;
    }
    
    
    [HttpPost]
    [Route("custom-email")]
    public async Task SendCustomEmail([FromBody] EmailRequest emailRequest)
    {
         await _email.SendCustomEmail(emailRequest);
    }
}