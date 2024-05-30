using MongoDB.Bson;
using System.ComponentModel.DataAnnotations;

namespace Core
{
    public class Application
    {
        public string Id { get; set; }
        public string Status { get; set; }
        
        public string? SelectedStatus { get; set; }
        
        public string Type { get; set; }
        
        public City City { get; set; }
        public string? SecondaryLocation { get; set; } 

        [Required(ErrorMessage = "The Priority1 field is required.")]
        public string Priority1 { get; set; }
        public string? Priority2 { get; set; }
        public Volunteer Volunteer { get; set; }
    }

 
}
