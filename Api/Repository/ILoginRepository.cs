using Core;
using MongoDB.Driver;

namespace Api.Interfaces
{
    public interface ILoginRepository
    {
        void CreateAccount(Administrator customer);
        Administrator VerifyLogin(string username, string password);
        Administrator GetUserByUserName(string username);
    }
}