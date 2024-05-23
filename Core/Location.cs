namespace Core;


public class City
{
    public string Id { get; set; }
    public string Name { get; set; }  // Renamed City property to Name for clarity.

    public List<Week> Weeks { get; set; } // A city has multiple weeks.

    public bool OpenForRegistration { get; set; }
}

public class Week
{
    public string WeekName { get; set; } // Optional: To identify the week.

    public List<Period> Periods { get; set; } // A week contains multiple periods.
}

public class Period
{
    public string PeriodName { get; set; }

    public DateTime StartDate { get; set; }

    public DateTime EndDate { get; set; }

    public int Capacity { get; set; }
}