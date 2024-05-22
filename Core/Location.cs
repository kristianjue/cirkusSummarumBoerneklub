using MongoDB.Bson;
namespace Core;

public class SystemSettings
{
    public string Id { get; set; }

    public List<Location> Locations { get; set; }
    
    public bool OpenForRegistration { get; set; }
    
}
public class Location
{
    public string City { get; set; }
    
    public List<Period> Periods { get; set; }
    
}
public class Period
{
    public string PeriodName { get; set; }
    
    public DateOnly StartDate { get; set; }
    
    public DateOnly EndDate { get; set; }
    
    public int Capacity { get; set; }
    
}
