using System.ComponentModel.DataAnnotations;

namespace Core
{
    public class Volunteer
    {
        public string Id { get; set; }
        [Required] public string Name { get; set; }
        [Required] public string Email { get; set; }
        [Required] public int PhoneNumber { get; set; }
        [Required] public int KrævNumber { get; set; }
        [Required] public List<Child>? Children { get; set; }
        
    }
}
