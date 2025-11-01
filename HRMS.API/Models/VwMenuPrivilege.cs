using Microsoft.EntityFrameworkCore;

namespace HRMS.API.Models
{
    [Keyless]
    public class VwMenuPrivilege
    {
        public int ParentId { get; set; }
        public string? MenuName { get; set; }
        public string? Icon { get; set; }



    }
}
