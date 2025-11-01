using Microsoft.EntityFrameworkCore;

namespace HRMS.API.Models
{
    [Keyless]
    public class spRptAttendanceDetails
    {
        public string? EmpName { get; set; }
        public string? DOJ { get; set; }
        public DateTime? WorkDate { get; set; }
        public string? InTime { get; set; }
        public string? OutTime { get; set; }
        public string? DayStatus { get; set; }
        public string? Remarks { get; set; }
        public string? FromDate { get; set; }
        public string? ToDate { get; set; }
        public string? PersonalSMS { get; set; }
    }
}
