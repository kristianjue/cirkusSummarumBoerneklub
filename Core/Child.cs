using System.ComponentModel.DataAnnotations;

namespace Core
{
    public class Child
    {
        [Required] public string Name { get; set; }
        [Required]   public int Age { get; set; }
        [Required]  public string Size { get; set; }
        [Required]  public string Signature { get; set; }
        [Required]  public string beenThereBefore { get; set; }
        
        public string comment { get; set; }
        
        public string interests { get; set; }
    }
}
