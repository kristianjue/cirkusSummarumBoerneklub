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
var filter = Builders<Application>.Filter.Eq(application => application.Id, application.Id);
        var update = Builders<Application>.Update
            .Set(application => application.Status, application.Status)
            .Set(application => application.Location, application.Location)
            .Set(application => application.FirstPriority, application.FirstPriority)
            .Set(application => application.SecondPriority, application.SecondPriority)
            .Set(application => application.Volunteer, application.Volunteer);


        ApplicationCollection.UpdateOne(filter, update);
    }

    public void DeleteApplication(string id)
    {
        var filter = Builders<Application>.Filter.Eq(application => application.Id, id);
        ApplicationCollection.DeleteOne(filter);
    }
}