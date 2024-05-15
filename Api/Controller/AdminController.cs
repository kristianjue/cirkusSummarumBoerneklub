using Api.Repository;
using Microsoft.AspNetCore.Mvc;
using Api.Interfaces;
using Core;

[ApiController] 
[Route("api/admin")]

public class AdminController : ControllerBase
{
    private readonly IAdminRepository _adminRepository;
    
    public AdminController(IAdminRepository adminRepository)
    {
        _adminRepository = adminRepository;
    }
    
    [HttpGet]
    [Route("get-all")]
    public ActionResult<List<Administrator>> GetAllAdmins()
    {
        return Ok(_adminRepository.GetAllAdmins());
    }
    
    [HttpPost]
    [Route("create")]
    public void CreateAdmin([FromBody] Administrator admin)
    {
        _adminRepository.CreateAdmin(admin); 
    }
    
    [HttpGet]
    [Route("get-by-email/{email}")]
    public ActionResult<Administrator> GetAdminByEmail(string email)
    {
        return Ok(_adminRepository.GetAdminByEmail(email));
    }
    
    [HttpPost]
    [Route("update/{email}")]
    public void UpdateAdmin(string email, [FromBody] Administrator admin)
    {
        _adminRepository.UpdateAdmin(email, admin);
    }
    
    [HttpDelete]
    [Route("delete/{email}")]
    public void DeleteAdmin(string email)
    {
        _adminRepository.DeleteAdmin(email);
    }
    
}