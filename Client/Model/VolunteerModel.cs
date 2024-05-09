namespace Client.Model
{
    public class VolunteerModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public int PhoneNumber { get; set; }
        public int KrævNumber { get; set; }
        public List<ChildModel> Children { get; set; } // Antager at ChildModel klassen eksisterer

        // Hvis YoungVolunteer er en subklasse af Volunteer
        public YoungVolunteerModel YoungVolunteer { get; set; } // Antager at YoungVolunteerModel klassen eksisterer
    }
}
