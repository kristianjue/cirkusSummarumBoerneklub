using Core;
using MongoDB.Driver;

namespace Api.Interfaces
{
    public interface ILoginRepository
    {
        void CreateAccount(Administrator administrator);
        Administrator VerifyLogin(string email, string password);
        Administrator GetUserByEmail(string email);
    }
}