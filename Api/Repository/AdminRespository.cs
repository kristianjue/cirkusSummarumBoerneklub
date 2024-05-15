using Core;
using MongoDB.Driver;

namespace Api.Repository;

public class AdminRespository : IAdminRepository
{
    private IMongoDatabase database;
    private IMongoCollection<Administrator> AdministratorCollection;
    private string connectionString = "mongodb+srv://system:system@cirkussummarum.to00ch9.mongodb.net/";

    public AdminRespository(IMongoClient mongoClient)
    {
        database = mongoClient.GetDatabase("CirkusSummarum");
        AdministratorCollection = database.GetCollection<Administrator>("Administrator");
    }

    
    public List<Administrator> GetAllAdmins()
    {
        return AdministratorCollection.Find(FilterDefinition<Administrator>.Empty).ToList();
    }

    public Administrator GetAdminByEmail(string email)
    {
        var filter = Builders<Administrator>.Filter.Eq(administrator => administrator.Email, email);
        return AdministratorCollection.Find(filter).SingleOrDefault();
    }

    public void CreateAdmin(Administrator admin)
    {
        var filter = Builders<Administrator>.Filter.Eq(administrator => administrator.Email, admin.Email);
        if (AdministratorCollection.Find(filter).Any())
        {
            throw new Exception("Email already exists");
        }
        else
        {
            AdministratorCollection.InsertOne(admin);
        }
    }

    public void UpdateAdmin(string email, Administrator admin)
    {
        var filter = Builders<Administrator>.Filter.Eq(administrator => administrator.Email, email);
        var update = Builders<Administrator>.Update
            .Set(administrator => administrator.Name, admin.Name)
            .Set(administrator => administrator.Email, admin.Email)
            .Set(administrator => administrator.PhoneNumber, admin.PhoneNumber)
            .Set(administrator => administrator.Password, admin.Password)
            .Set(administrator => administrator.KrævNumber, admin.KrævNumber);

        AdministratorCollection.UpdateOne(filter, update);
    }

    public void DeleteAdmin(string email)
    {
        var filter = Builders<Administrator>.Filter.Eq(administrator => administrator.Email, email);
        AdministratorCollection.DeleteOne(filter);
    }
}