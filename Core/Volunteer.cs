namespace Core
{
    public class Volunteer
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public int PhoneNumber { get; set; }
        public int KrævNumber { get; set; }
        public List<Child>? Children { get; set; }

        public YoungVolunteer? YoungVolunteer { get; set; }
       
    }
}
