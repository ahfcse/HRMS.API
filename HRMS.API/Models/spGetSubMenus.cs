using Microsoft.EntityFrameworkCore;

namespace HRMS.API.Models
{
    [Keyless]
    public class spGetSubMenus
    {
        public string? MenuName { get; set; }
        public string? RouterLink { get; set; }
        public string? Icon { get; set; }
        public bool? IsVisible { get; set; }
        public int? ParentId { get; set; }
    }
}
