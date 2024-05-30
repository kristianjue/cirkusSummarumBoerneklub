using Core;
using MongoDB.Driver;

namespace Api.Repository;

// ApplicationRepository implementerer IApplicationRepository interface for MongoDB interaktioner.
public class ApplicationRepository : IApplicationRepository
{
    // Private felter til at holde MongoDB database og samling.
    private IMongoDatabase database;
    private IMongoCollection<Application> ApplicationCollection;

    // MongoDB forbindelsesstreng.
    private string connectionString = "mongodb+srv://system:system@cirkussummarum.to00ch9.mongodb.net/";

    // Constructor der initialiserer database og samling ved brug af en MongoClient.
    public ApplicationRepository(IMongoClient mongoClient)
    {
        // Henter database "CirkusSummarum".
        database = mongoClient.GetDatabase("CirkusSummarum");
        // Henter samlingen "Applications".
        ApplicationCollection = database.GetCollection<Application>("Applications");
    }

    // Metode til at oprette en ny ans�gning.
    public void CreateApplication(Application application)
    {
        // Inds�tter en ny ans�gning i samlingen.
        ApplicationCollection.InsertOne(application);
    }

    // Metode til at hente alle ans�gninger.
    public List<Application> GetAllApplications()
    {
        // Finder og returnerer alle dokumenter i samlingen.
        return ApplicationCollection.Find(FilterDefinition<Application>.Empty).ToList();
    }

    // Metode til at hente en ans�gning baseret p� ID.
    public Application GetApplicationById(string id)
    {
        // Opretter et filter for at matche ans�gnings-ID.
        var filter = Builders<Application>.Filter.Eq(application => application.Id, id);
        // Finder og returnerer ans�gningen, hvis ID matcher.
        return ApplicationCollection.Find(filter).SingleOrDefault();
    }

    // Metode til at hente ans�gninger baseret p� filterkriterier.
    public List<Application> GetApplicationsByfilter(string city, string period, string status)
    {
        // Opretter en filterbygger.
        var filterBuilder = Builders<Application>.Filter;
        // Liste til at holde filterdefinitioner.
        var filters = new List<FilterDefinition<Application>>();

        // Tilf�jer filter for by, hvis den ikke er null eller "all".
        if (!string.IsNullOrEmpty(city) && city != "all")
        {
            filters.Add(filterBuilder.Eq(application => application.City.Name, city));
        }

        // Tilf�jer filter for periode, hvis den ikke er null eller "all".
        if (!string.IsNullOrEmpty(period) && period != "all")
        {
            filters.Add(filterBuilder.Eq(application => application.Priority1, period));
        }

        // Tilf�jer filter for status, hvis den ikke er null eller "all".
        if (!string.IsNullOrEmpty(status) && status != "all")
        {
            // Tilf�jer filter for "Godkendt" status.
            if (status == "Godkendt")
            {
                filters.Add(filterBuilder.Nin(application => application.Status, new[] { "Ny", "Venteliste", "Afvist" }));
            }
            else
            {
                filters.Add(filterBuilder.Eq(application => application.Status, status));
            }
        }

        // Kombinerer alle filterdefinitioner med "And" operatoren, eller bruger en tom filterdefinition.
        var filter = filters.Count > 0 ? filterBuilder.And(filters) : FilterDefinition<Application>.Empty;

        // Finder og returnerer ans�gninger, der matcher filtrene.
        return ApplicationCollection.Find(filter).ToList();
    }

    // Metode til at opdatere en eksisterende ans�gning.
    public void UpdateApplication(Application application)
    {
        // Opretter et filter for at matche ans�gnings-ID.
        var filter = Builders<Application>.Filter.Eq(a => a.Id, application.Id);
        // Opretter opdateringsdefinitioner for ans�gningens felter.
        var update = Builders<Application>.Update
            .Set(a => a.Status, application.Status)
            .Set(a => a.City, application.City)
            .Set(a => a.Priority1, application.Priority1)
            .Set(a => a.Priority2, application.Priority2)
            .Set(a => a.Volunteer, application.Volunteer);

        // Udf�rer opdateringen og gemmer resultatet.
        var result = ApplicationCollection.UpdateOne(filter, update);

        // Tjekker om nogen dokumenter blev opdateret.
        if (result.ModifiedCount == 0)
        {
            Console.WriteLine($"No documents were updated for application Id={application.Id}");
        }
        else
        {
            Console.WriteLine($"Successfully updated application Id={application.Id}");
        }
    }

    // Metode til at slette en ans�gning baseret p� ID.
    public void DeleteApplication(string id)
    {
        // Opretter et filter for at matche ans�gnings-ID.
        var filter = Builders<Application>.Filter.Eq(application => application.Id, id);
        // Sletter ans�gningen fra samlingen.
        ApplicationCollection.DeleteOne(filter);
    }
}
