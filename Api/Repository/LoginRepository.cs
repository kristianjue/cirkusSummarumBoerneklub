
using Api.Interfaces;
using Core;
using MongoDB.Driver;

namespace Api.Repositories
{
    // LoginRepository implementerer ILoginRepository interface for MongoDB interaktioner.
    public class LoginRepository : ILoginRepository
    {
        // Private felter til at holde MongoDB database og samling.
        private IMongoDatabase database;
        private IMongoCollection<Administrator> AdministratorCollection;

        // MongoDB forbindelsesstreng.
        private string connectionString = "mongodb+srv://system:system@cirkussummarum.to00ch9.mongodb.net/";

        // Constructor der initialiserer database og samling ved brug af en MongoClient.
        public LoginRepository(IMongoClient mongoClient)
        {
            // Henter database "CirkusSummarum".
            database = mongoClient.GetDatabase("CirkusSummarum");
            // Henter samlingen "Administrator".
            AdministratorCollection = database.GetCollection<Administrator>("Administrator");
        }

        // Metode til at oprette en ny administrator konto.
        public void CreateAccount(Administrator administrator)
        {
            // Opretter et filter for at tjekke, om email allerede findes i samlingen.
            var filter = Builders<Administrator>.Filter.Eq(administrator => administrator.Email, administrator.Email);
            if (AdministratorCollection.Find(filter).Any())
            {
                // Håndterer eksisterende e-mail fejl ved at kaste en exception med beskeden "Email already exists".
                throw new Exception("Email already exists");
            }
            else
            {
                // Indsætter en ny administrator i samlingen.
                AdministratorCollection.InsertOne(administrator);
            }
        }

        // Metode til at verificere loginoplysninger.
        public Administrator VerifyLogin(string email, string password)
        {
            // Opretter et filter for at matche email og password.
            var filter = Builders<Administrator>.Filter.Eq(Administrator => Administrator.Email, email) &
                         Builders<Administrator>.Filter.Eq(Administrator => Administrator.Password, password);
            // Finder og returnerer administratoren, hvis loginoplysningerne matcher.
            var admin = AdministratorCollection.Find(filter).SingleOrDefault();
            return admin;
        }

        // Metode til at hente en administrator baseret på email.
        public Administrator GetUserByEmail(string email)
        {
            // Opretter et filter for at matche email.
            var filter = Builders<Administrator>.Filter.Eq(Administrator => Administrator.Email, email);
            // Finder og returnerer administratoren, hvis email matcher.
            return AdministratorCollection.Find(filter).SingleOrDefault();
        }
    }

}
