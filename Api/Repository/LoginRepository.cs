
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
            database = mongoClient.GetDatabase("GenbrugEaaa");
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
            // Husk at hashe password før sammenligning, hvis du bruger hashing i databasen.
            var filter = Builders<Administrator>.Filter.Eq(administrator => administrator.Email, email)
                & Builders<Administrator>.Filter.Eq(administrator => administrator.Password, password); // Du bør hashe password
            return AdministratorCollection.Find(filter).SingleOrDefault();

            Console.WriteLine("Login successful");
        }

        public Administrator GetUserByEmail(string email)
        {
            var filter = Builders<Administrator>.Filter.Eq(administrator => administrator.Email, email);
            return AdministratorCollection.Find(filter).SingleOrDefault();
        }


    }
}
