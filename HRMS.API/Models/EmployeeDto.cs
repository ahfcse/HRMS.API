using Microsoft.EntityFrameworkCore;

namespace HRMS.API.Models
{
    [Keyless]
    public class EmployeeDto
    {
        public int EmployeeId { get; set; }
        public string? EmpName { get; set; }
    }
}
