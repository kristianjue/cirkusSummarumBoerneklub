using Core;
using MongoDB.Driver;

namespace Api.Repository;

public class SystemSettingRepository
{
    private IMongoDatabase database;
    private IMongoCollection<SystemSettings> SystemSettingCollection;
    private string connectionString = "mongodb+srv://system:system@cirkussummarum.to00ch9.mongodb.net/";
}

public SystemSettingRepository(IMongoClient mongoClient)
{
    database = mongoClient.GetDatabase("CirkusSummarum");
    SystemSettingCollection = database.GetCollection<SystemSettings>("SystemSettings");
}

public void CreateSystemSetting(SystemSettings systemSetting)
{
    SystemSettingCollection.InsertOne(systemSetting);
}

public List<SystemSettings> GetAllSystemSettings()
{
    return SystemSettingCollection.Find(FilterDefinition<SystemSettings>.Empty).ToList();
}

public SystemSettings GetSystemSettingById(string id)
{
    var filter = Builders<SystemSettings>.Filter.Eq(systemSetting => systemSetting.Id, id);
    return SystemSettingCollection.Find(filter).SingleOrDefault();
}

public void UpdateSystemSetting(SystemSettings systemSetting)
{
    var filter = Builders<SystemSettings>.Filter.Eq(a => a.Id, systemSetting.Id);
    var update = Builders<SystemSettings>.Update
        .Set(a => a.Status, systemSetting.Status)
        .Set(a => a.Location, systemSetting.Location)
        .Set(a => a.Priority1, systemSetting.Priority1)
        .Set(a => a.Priority2, systemSetting.Priority2)
        .Set(a => a.Volunteer, systemSetting.Volunteer);

    var result = SystemSettingCollection.UpdateOne(filter, update);

    if (result.ModifiedCount == 0)
    {
        Console.WriteLine($"No documents were updated for systemSetting Id={systemSetting.Id}");
    }
    else
    {
        Console.WriteLine($"Successfully updated systemSetting Id={systemSetting.Id}");
    }
}

public void DeleteSystemSetting(string id)
{
    var filter = Builders<SystemSettings>.Filter.Eq(systemSetting => systemSetting.Id, id);
    SystemSettingCollection.DeleteOne(filter);
}