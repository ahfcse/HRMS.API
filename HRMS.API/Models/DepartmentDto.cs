using Microsoft.EntityFrameworkCore;

namespace HRMS.API.Models
{
    [Keyless]
    public class DepartmentDto
    {
        public int DepartmentId { get; set; }
        public string? DepartmentName { get; set; }
    }
}
