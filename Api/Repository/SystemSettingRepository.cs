using Core;
using MongoDB.Driver;
using System.Collections.Generic;
using Api.Interfaces;

namespace Api.Repository
{
    public class CityRepository : ICityRepository
    {
        private readonly IMongoDatabase _database;
        private readonly IMongoCollection<City> _settingsCollection;
        private readonly string _connectionString = "mongodb+srv://system:system@cirkussummarum.to00ch9.mongodb.net/";

        public CityRepository(IMongoClient mongoClient)
        {
            _database = mongoClient.GetDatabase("CirkusSummarum");
            _settingsCollection = _database.GetCollection<City>("SystemSettings");
        }

        public void CreateCity(City settings)
        {
            _settingsCollection.InsertOne(settings);
        }

        public List<City> GetAllCity()
        {
            return _settingsCollection.Find(FilterDefinition<City>.Empty).ToList();
        }
        
        public List<City> GetAllActiveCity()
        {
            var filter = Builders<City>.Filter.Eq(settings => settings.OpenForRegistration, true);
            return _settingsCollection.Find(filter).ToList();
        }

        public City GetCityByName(string name)
        {
            var filter = Builders<City>.Filter.Eq(settings => settings.Name, name);
            return _settingsCollection.Find(filter).SingleOrDefault();
        }

        public void UpdateCity(City settings)
        {
            var filter = Builders<City>.Filter.Eq(s => s.Name, settings.Name);
            var update = Builders<City>.Update
                .Set(s => s.Name, settings.Name)
                .Set(s => s.OpenForRegistration, settings.OpenForRegistration)
                .Set(s => s.Weeks, settings.Weeks);

            var result = _settingsCollection.UpdateOne(filter, update);

            if (result.ModifiedCount == 0)
            {
                Console.WriteLine($"No documents were updated for city Name={settings.Name}");
            }
            else
            {
                Console.WriteLine($"Successfully updated city Name={settings.Name}");
            }
        }

        public void DeleteCity(string name)
        {
            var filter = Builders<City>.Filter.Eq(settings => settings.Name, name);
            _settingsCollection.DeleteOne(filter);
        }
    }
}
