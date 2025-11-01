using Microsoft.EntityFrameworkCore;

namespace HRMS.API.Models
{
    [Keyless]
    public class VwUser
    {
      
        public string? Name { get; set; }
        public byte[]? Image { get; set; }
        public string? Email { get; set; }
        public string? ContactNo { get; set; }
        public string? IntercomNo { get; set; }
        public string? BloodGroup { get; set; }
   
    }
}
