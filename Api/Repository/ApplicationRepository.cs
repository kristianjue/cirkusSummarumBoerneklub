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
    
    public List<Application> GetApplicationsByfilter(string city, string period, string status)
    {
        var filterBuilder = Builders<Application>.Filter;
        var filters = new List<FilterDefinition<Application>>();

        if (!string.IsNullOrEmpty(city) && city != "all")
        {
            filters.Add(filterBuilder.Eq(application => application.City.Name, city));
        }

        if (!string.IsNullOrEmpty(period) && period != "all")
        {
            filters.Add(filterBuilder.Eq(application => application.Priority1, period));
        }

        if (!string.IsNullOrEmpty(status) && status != "all")
        {
            if (status == "Godkendt")
            {
                filters.Add(filterBuilder.Nin(application => application.Status, new[] { "Ny", "Venteliste", "Afvist" }));
            }
            else
            {
                filters.Add(filterBuilder.Eq(application => application.Status, status));
            }
        }

        var filter = filters.Count > 0 ? filterBuilder.And(filters) : FilterDefinition<Application>.Empty;
    
        return ApplicationCollection.Find(filter).ToList();
    }

    public void UpdateApplication(Application application)
    {
        var filter = Builders<Application>.Filter.Eq(a => a.Id, application.Id);
        var update = Builders<Application>.Update
            .Set(a => a.Status, application.Status)
            .Set(a => a.City, application.City)
            .Set(a => a.Priority1, application.Priority1)
            .Set(a => a.Priority2, application.Priority2)
            .Set(a => a.Volunteer, application.Volunteer);

        var result = ApplicationCollection.UpdateOne(filter, update);

        if (result.ModifiedCount == 0)
        {
            Console.WriteLine($"No documents were updated for application Id={application.Id}");
        }
        else
        {
            Console.WriteLine($"Successfully updated application Id={application.Id}");
        }
    }



    public void DeleteApplication(string id)
    {
        var filter = Builders<Application>.Filter.Eq(application => application.Id, id);
        ApplicationCollection.DeleteOne(filter);
    }
}