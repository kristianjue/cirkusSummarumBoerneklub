using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    public class Signature
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public int PhoneNumber { get; set; }
        public int? KrævNumber { get; set; }
        public Volunteer? Volunteer { get; set; }
        public YoungVolunteer YoungVolunteer { get; set; }
    }
}
