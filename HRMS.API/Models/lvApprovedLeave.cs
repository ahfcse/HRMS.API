using Microsoft.EntityFrameworkCore;

namespace HRMS.API.Models
{
    [Keyless]
    public class lvApprovedLeave
    {
      
        public int LeaveId { get; set; }
        public DateTime? LeaveDate { get; set; }
        public string? EmpName { get; set; }
        
    }
}
