using Microsoft.EntityFrameworkCore;

namespace HRMS.API.Models
{
    [Keyless]
    public class spRptTaskDetails
    {
        public string? EmpName { get; set; }
        public DateTime? Dateofjoin { get; set; }
        public DateTime? date { get; set; }
        public string? TaskName { get; set; }
        public string? Status { get; set; }
      
    }
}
