namespace Core
{
    public class ApplicationModel
    {
        public string Id { get; set; }
        public string Status { get; set; }
        public string Location { get; set; }
        public DateTime FirstPriority { get; set; }
        public DateTime SecondPriority { get; set; }
        public VolunteerModel Volunteer { get; set; } // Antager at VolunteerModel klassen eksisterer 

    }

    public enum ApplicationStatuses
    {
        Applikation,
        Venteliste,
        Uge27,
        Uge28
    }
}
