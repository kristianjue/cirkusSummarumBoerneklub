namespace Core
{
    public class Volunteer
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public int PhoneNumber { get; set; }
        public int KrævNumber { get; set; }
        public List<Child> Children { get; set; } // Antager at ChildModel klassen eksisterer

        // Hvis YoungVolunteer er en subklasse af Volunteer
        public YoungVolunteer YoungVolunteer { get; set; } // Antager at YoungVolunteerModel klassen eksisterer
    }
}
