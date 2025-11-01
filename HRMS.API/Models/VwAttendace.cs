using Microsoft.EntityFrameworkCore;

namespace HRMS.API.Models
{
    [Keyless]
    public class VwAttendace
    {
     
        public string? EmpName { get; set; }
        public string? DOJ { get; set; }
        public DateTime? WorkDate { get; set; }
        public string? InTime { get; set; }
        public string? OutTime { get; set; }
        public string? DayStatus { get; set; }
        public string? Remarks { get; set; }

    }
}
