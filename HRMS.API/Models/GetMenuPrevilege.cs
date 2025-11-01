using Microsoft.EntityFrameworkCore;

namespace HRMS.API.Models
{
    [Keyless]
    public class GetMenuPrevilege
    {
        public string? MenuName { get; set; }
    }
}
