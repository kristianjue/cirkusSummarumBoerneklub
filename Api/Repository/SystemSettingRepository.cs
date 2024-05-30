using Core;
using MongoDB.Driver;
using System.Collections.Generic;
using Api.Interfaces;

namespace Api.Repository
{
    // CityRepository implementerer ICityRepository interface for MongoDB interaktioner.
    public class CityRepository : ICityRepository
    {
        // Private felter til at holde MongoDB database og samling.
        private readonly IMongoDatabase _database;
        private readonly IMongoCollection<City> _settingsCollection;

        // MongoDB forbindelsesstreng.
        private readonly string _connectionString = "mongodb+srv://system:system@cirkussummarum.to00ch9.mongodb.net/";

        // Constructor der initialiserer database og samling ved brug af en MongoClient.
        public CityRepository(IMongoClient mongoClient)
        {
            // Henter database "CirkusSummarum".
            _database = mongoClient.GetDatabase("CirkusSummarum");
            // Henter samlingen "SystemSettings".
            _settingsCollection = _database.GetCollection<City>("SystemSettings");
        }

        // Metode til at oprette en ny by.
        public void CreateCity(City settings)
        {
            // Indsætter en ny by i samlingen.
            _settingsCollection.InsertOne(settings);
        }

        // Metode til at hente alle byer.
        public List<City> GetAllCity()
        {
            // Finder alle dokumenter i samlingen.
            return _settingsCollection.Find(FilterDefinition<City>.Empty).ToList();
        }

        // Metode til at hente alle aktive byer (åben for registrering).
        public List<City> GetAllActiveCity()
        {
            // Opretter et filter for byer der er åbne for registrering.
            var filter = Builders<City>.Filter.Eq(settings => settings.OpenForRegistration, true);
            // Finder og returnerer alle aktive byer.
            return _settingsCollection.Find(filter).ToList();
        }

        // Metode til at hente en by baseret på navn.
        public City GetCityByName(string name)
        {
            // Opretter et filter for byer baseret på navn.
            var filter = Builders<City>.Filter.Eq(settings => settings.Name, name);
            // Finder og returnerer en enkelt by baseret på navnet.
            return _settingsCollection.Find(filter).SingleOrDefault();
        }

        // Metode til at opdatere en eksisterende by.
        public void UpdateCity(City settings)
        {
            // Opretter et filter for at finde byen baseret på navn.
            var filter = Builders<City>.Filter.Eq(s => s.Name, settings.Name);
            // Opretter opdateringsdefinitioner for byens felter.
            var update = Builders<City>.Update
                .Set(s => s.Name, settings.Name)
                .Set(s => s.OpenForRegistration, settings.OpenForRegistration)
                .Set(s => s.Weeks, settings.Weeks);

            // Udfører opdateringen og gemmer resultatet.
            var result = _settingsCollection.UpdateOne(filter, update);

            // Tjekker om nogen dokumenter blev opdateret.
            if (result.ModifiedCount == 0)
            {
                Console.WriteLine($"No documents were updated for city Name={settings.Name}");
            }
            else
            {
                Console.WriteLine($"Successfully updated city Name={settings.Name}");
            }
        }

        // Metode til at slette en by baseret på navn.
        public void DeleteCity(string name)
        {
            // Opretter et filter for at finde byen baseret på navn.
            var filter = Builders<City>.Filter.Eq(settings => settings.Name, name);
            // Sletter byen fra samlingen.
            _settingsCollection.DeleteOne(filter);
        }
    }

}
