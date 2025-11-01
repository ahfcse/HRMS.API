using Microsoft.EntityFrameworkCore;

namespace HRMS.API.Models
{
    [Keyless]
    public class spLateSummery
    {
        public string? DepartmentName { get; set; }
        public string? EmpName { get; set; }
        public string? DesignationName { get; set; }
        public string? Remarks { get; set; }
        public string? DayStatus { get; set; }
        public int? EmployeeId { get; set; }
        public int? Total { get; set; }
        public int? SD { get; set; }
        public string? FromDate { get; set; }
        public string? ToDate { get; set; }
        
    }
}
