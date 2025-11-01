using Microsoft.EntityFrameworkCore;

namespace HRMS.API.Models
{
    [Keyless]
    public class spRptAttendanceSummery
    {
        public string? EmpName { get; set; }
        public string? DOJ { get; set; }
        public string? DayStatus { get; set; }
        public int? Total { get; set; }
        public string? FromDate { get; set; }
        public string? ToDate { get; set; }
    }
}
