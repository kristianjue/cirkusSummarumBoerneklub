using Core;
using MongoDB.Driver;
using System.Collections.Generic;
using Api.Interfaces;

namespace Api.Repository
{
    public class SystemSettingsRepository : ISystemSettingsRepository
    {
        private readonly IMongoDatabase _database;
        private readonly IMongoCollection<SystemSettings> _settingsCollection;
        private readonly string _connectionString = "mongodb+srv://system:system@cirkussummarum.to00ch9.mongodb.net/";

        public SystemSettingsRepository(IMongoClient mongoClient)
        {
            _database = mongoClient.GetDatabase("CirkusSummarum");
            _settingsCollection = _database.GetCollection<SystemSettings>("SystemSettings");
        }

        public void CreateSystemSettings(SystemSettings settings)
        {
            _settingsCollection.InsertOne(settings);
        }

        public List<SystemSettings> GetAllSystemSettings()
        {
            return _settingsCollection.Find(FilterDefinition<SystemSettings>.Empty).ToList();
        }

        public SystemSettings GetSystemSettingsById(string id)
        {
            var filter = Builders<SystemSettings>.Filter.Eq(settings => settings.Id, id);
            return _settingsCollection.Find(filter).SingleOrDefault();
        }

        public void UpdateSystemSettings(SystemSettings settings)
        {
            var filter = Builders<SystemSettings>.Filter.Eq(s => s.Id, settings.Id);
            var update = Builders<SystemSettings>.Update
                .Set(s => s.Locations, settings.Locations)
                .Set(s => s.OpenForRegistration, settings.OpenForRegistration);

            var result = _settingsCollection.UpdateOne(filter, update);

            if (result.ModifiedCount == 0)
            {
                Console.WriteLine($"No documents were updated for settings Id={settings.Id}");
            }
            else
            {
                Console.WriteLine($"Successfully updated settings Id={settings.Id}");
            }
        }

        public void DeleteSystemSettings(string id)
        {
            var filter = Builders<SystemSettings>.Filter.Eq(settings => settings.Id, id);
            _settingsCollection.DeleteOne(filter);
        }
    }
}
