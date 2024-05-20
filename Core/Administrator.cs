using System.Globalization;
using MongoDB.Bson;

namespace Core
{
    public class Administrator
    {
        public ObjectId Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public int PhoneNumber { get; set;}
        public string Password { get; set; }
        public int KrævNumber { get; set; }
        
        public string Image { get; set; }
    }
}
