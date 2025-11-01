using Microsoft.EntityFrameworkCore;

namespace HRMS.API.Models
{
    [Keyless]
    public class UserMail
    {
        public string Email { get; set; }
    }
}
