namespace Core;

public class System
{
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
    
    public int Capacity { get; set; }
    
}
