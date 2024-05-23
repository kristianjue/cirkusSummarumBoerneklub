using MongoDB.Bson;

namespace Core
{
    public class Application
    {
        public string Id { get; set; }
        public string Status { get; set; }
        public string Location { get; set; }
        public string SecondaryLocation { get; set; } // Tilføj denne linje
        public string Priority1 { get; set; }
        public string Priority2 { get; set; }
        public Volunteer Volunteer { get; set; } // Antager at VolunteerModel klassen eksisterer 
    }

    public enum ApplicationStatuses
    {
        Applikation,
        Venteliste,
        Uge27,
        Uge28
    }
}
