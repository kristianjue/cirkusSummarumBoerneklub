using Api.Repository;
using Microsoft.AspNetCore.Mvc;
using Api.Interfaces;
using Core;

[ApiController] 
[Route("api/admin")]

// AdminController er en ControllerBase, der håndterer HTTP-anmodninger relateret til administratorer.
public class AdminController : ControllerBase
{
    // Interface til AdminRepository for dataadgang.
    private readonly IAdminRepository _adminRepository;

    // Constructor til at initialisere AdminController med AdminRepository.
    public AdminController(IAdminRepository adminRepository)
    {
        _adminRepository = adminRepository;
    }

    // HTTP GET metode til at hente alle administratorer.
    [HttpGet]
    [Route("get-all")]
    public ActionResult<List<Administrator>> GetAllAdmins()
    {
        // Returnerer 200 OK status med listen over administratorer.
        return Ok(_adminRepository.GetAllAdmins());
    }

    // HTTP POST metode til at oprette en ny administrator.
    [HttpPost]
    [Route("create")]
    public void CreateAdmin([FromBody] Administrator admin)
    {
        // Kalder CreateAdmin metoden fra AdminRepository for at oprette en ny administrator.
        _adminRepository.CreateAdmin(admin);
    }

    // HTTP GET metode til at hente en administrator baseret på email.
    [HttpGet]
    [Route("get-by-email/{email}")]
    public ActionResult<Administrator> GetAdminByEmail(string email)
    {
        // Returnerer 200 OK status med den fundne administrator.
        return Ok(_adminRepository.GetAdminByEmail(email));
    }

    // HTTP POST metode til at opdatere en eksisterende administrator baseret på email.
    [HttpPost]
    [Route("update/{email}")]
    public void UpdateAdmin(string email, [FromBody] Administrator admin)
    {
        // Kalder UpdateAdmin metoden fra AdminRepository for at opdatere administratoren.
        _adminRepository.UpdateAdmin(email, admin);
    }

    // HTTP DELETE metode til at slette en administrator baseret på email.
    [HttpDelete]
    [Route("delete/{email}")]
    public void DeleteAdmin(string email)
    {
        // Kalder DeleteAdmin metoden fra AdminRepository for at slette administratoren.
        _adminRepository.DeleteAdmin(email);
    }
}
