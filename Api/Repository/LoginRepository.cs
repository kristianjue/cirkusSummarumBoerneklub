
using Api.Interfaces;
using Core;
using MongoDB.Driver;

namespace Api.Repositories
{
    public class LoginRepository : ILoginRepository
    {
        private IMongoDatabase database;
        private IMongoCollection<Administrator> AdministratorCollection;
        private string connectionString = "mongodb+srv://system:system@cirkussummarum.to00ch9.mongodb.net/";

        public LoginRepository(IMongoClient mongoClient)
        {
            database = mongoClient.GetDatabase("CirkusSummarum");
            AdministratorCollection = database.GetCollection<Administrator>("Administrator");
        }


        public void CreateAccount(Administrator administrator)
        {
            var filter = Builders<Administrator>.Filter.Eq(administrator => administrator.Email, administrator.Email);
            if (AdministratorCollection.Find(filter).Any())
            {
                // Håndter eksisterende e-mail fejlfrit, måske returnere en specifik fejl eller resultat.
                throw new Exception("Email already exists");
            }
            else
            {
                AdministratorCollection.InsertOne(administrator);
            }
        }

        public Administrator VerifyLogin(string email, string password)
        {
            var filter = Builders<Administrator>.Filter.Eq(Administrator => Administrator.Email, email) &
                         Builders<Administrator>.Filter.Eq(Administrator => Administrator.Password, password);
            var admin = AdministratorCollection.Find(filter).SingleOrDefault();
            return admin;
        }

        public Administrator GetUserByEmail(string email)
        {
            var filter = Builders<Administrator>.Filter.Eq(Administrator => Administrator.Email, email);
            return AdministratorCollection.Find(filter).SingleOrDefault();
        }


    }
}
