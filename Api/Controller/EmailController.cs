using System.Net;
using Microsoft.AspNetCore.Mvc;
using Api.Logic;
using Core;


[ApiController]
[Route("api/email")]
public class EmailController : ControllerBase // EmailController er en ControllerBase, der håndterer HTTP-anmodninger relateret til e-mails.
{
    private readonly Email _email; // initialiserer Email klassen

    public EmailController(Email email)
    {
        _email = email; 
    }
    
    
    [HttpPost]
    [Route("custom-email")]
    public async Task SendCustomEmail([FromBody] EmailRequest emailRequest) // metode til at sende en e-mail
    {
         await _email.SendCustomEmail(emailRequest); // kalder SendCustomEmail metoden fra Email klassen
    }
}