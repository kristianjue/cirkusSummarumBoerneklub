using Core;

namespace Api.Repository;

public interface IAdminRepository
{
    List<Administrator> GetAllAdmins();
    
    Administrator GetAdminByEmail(string email);
    
    void CreateAdmin(Administrator admin);
    
    void UpdateAdmin(string email, Administrator admin);
    
    void DeleteAdmin(string email);
}