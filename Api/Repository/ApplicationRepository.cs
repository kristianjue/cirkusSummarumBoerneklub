using Core;
using MongoDB.Driver;

namespace Api.Repository;

public class ApplicationRepository : IApplicationRepository
{
    
    private IMongoDatabase database;
    private IMongoCollection<Application> ApplicationCollection;
    private string connectionString = "mongodb+srv://system:system@cirkussummarum.to00ch9.mongodb.net/";

    public ApplicationRepository(IMongoClient mongoClient)
    {
        database = mongoClient.GetDatabase("CirkusSummarum");
        ApplicationCollection = database.GetCollection<Application>("Applications");
    }

    public void CreateApplication(Application application)
    {
        ApplicationCollection.InsertOne(application);
    }

    public List<Application> GetAllApplications()
    {
        return ApplicationCollection.Find(FilterDefinition<Application>.Empty).ToList();
    }

    public Application GetApplicationById(string id)
    {
        var filter = Builders<Application>.Filter.Eq(application => application.Id, id);
        return ApplicationCollection.Find(filter).SingleOrDefault();
    }

    public void UpdateApplication(Application application)
    {
        var filter = Builders<Application>.Filter.Eq(a => a.Id, application.Id);
        var update = Builders<Application>.Update
            .Set(a => a.Status, application.Status)
            .Set(a => a.Location, application.Location)
            .Set(a => a.Priority1, application.Priority1)
            .Set(a => a.Priority2, application.Priority2)
            .Set(a => a.Volunteer, application.Volunteer);

        ApplicationCollection.UpdateOne(filter, update);
    }


    public void DeleteApplication(string id)
    {
        var filter = Builders<Application>.Filter.Eq(application => application.Id, id);
        ApplicationCollection.DeleteOne(filter);
    }
}