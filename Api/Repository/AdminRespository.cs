using Core;
using MongoDB.Driver;

namespace Api.Repository;

// AdminRepository implementerer IAdminRepository interface for MongoDB interaktioner.
public class AdminRespository : IAdminRepository
{
    // Private felter til at holde MongoDB database og samling.
    private IMongoDatabase database;
    private IMongoCollection<Administrator> AdministratorCollection;

    // MongoDB forbindelsesstreng.
    private string connectionString = "mongodb+srv://system:system@cirkussummarum.to00ch9.mongodb.net/";

    // Constructor der initialiserer database og samling ved brug af en MongoClient.
    public AdminRespository(IMongoClient mongoClient)
    {
        // Henter database "CirkusSummarum".
        database = mongoClient.GetDatabase("CirkusSummarum");
        // Henter samlingen "Administrator".
        AdministratorCollection = database.GetCollection<Administrator>("Administrator");
    }

    // Metode til at hente alle administratorer.
    public List<Administrator> GetAllAdmins()
    {
        // Finder og returnerer alle dokumenter i samlingen.
        return AdministratorCollection.Find(FilterDefinition<Administrator>.Empty).ToList();
    }

    // Metode til at hente en administrator baseret på email.
    public Administrator GetAdminByEmail(string email)
    {
        // Opretter et filter for at matche email.
        var filter = Builders<Administrator>.Filter.Eq(administrator => administrator.Email, email);
        // Finder og returnerer administratoren, hvis email matcher.
        return AdministratorCollection.Find(filter).SingleOrDefault();
    }

    // Metode til at oprette en ny administrator.
    public void CreateAdmin(Administrator admin)
    {
        // Opretter et filter for at tjekke, om email allerede findes i samlingen.
        var filter = Builders<Administrator>.Filter.Eq(administrator => administrator.Email, admin.Email);
        if (AdministratorCollection.Find(filter).Any())
        {
            // Håndterer eksisterende e-mail fejl ved at kaste en exception med beskeden "Email already exists".
            throw new Exception("Email already exists");
        }
        else
        {
            // Indsætter en ny administrator i samlingen.
            AdministratorCollection.InsertOne(admin);
        }
    }

    // Metode til at opdatere en eksisterende administrator baseret på email.
    public void UpdateAdmin(string email, Administrator admin)
    {
        // Opretter et filter for at matche email.
        var filter = Builders<Administrator>.Filter.Eq(administrator => administrator.Email, email);
        // Opretter opdateringsdefinitioner for administratorens felter.
        var update = Builders<Administrator>.Update
            .Set(administrator => administrator.Name, admin.Name)
            .Set(administrator => administrator.Email, admin.Email)
            .Set(administrator => administrator.PhoneNumber, admin.PhoneNumber)
            .Set(administrator => administrator.Password, admin.Password)
            .Set(administrator => administrator.KrævNumber, admin.KrævNumber)
            .Set(administrator => administrator.Image, admin.Image)
            .Set(administrator => administrator.Role, admin.Role);
        // Udfører opdateringen.
        AdministratorCollection.UpdateOne(filter, update);
    }

    // Metode til at slette en administrator baseret på email.
    public void DeleteAdmin(string email)
    {
        // Opretter et filter for at matche email.
        var filter = Builders<Administrator>.Filter.Eq(administrator => administrator.Email, email);
        // Sletter administratoren fra samlingen.
        AdministratorCollection.DeleteOne(filter);
    }
}
