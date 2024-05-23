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

    public List<Week> Weeks { get; set; }

    public Location()
    {
        Weeks = new List<Week>();
    }

}

public class Week
{
    public int WeekNumber { get; set; }

    public List<Period> Periods { get; set; }

    public Week()
    {
        Periods = new List<Period>();
    }
}

public class Period
{
    public string PeriodName { get; set; }

    public DateOnly StartDate { get; set; }

    public DateOnly EndDate { get; set; }

    public int Capacity { get; set; }

}
