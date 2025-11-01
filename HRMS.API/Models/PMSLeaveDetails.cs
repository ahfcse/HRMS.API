using System.ComponentModel.DataAnnotations;

namespace HRMS.API.Models
{
    public class PMSLeaveDetails
    {
        [Key]
        public int LeaveId { get; set; }
        public int? EmployeeId { get; set; }
        public DateTime? LeaveDate { get; set; }
        public DateTime? StDate { get; set; }
        public DateTime? EDate { get; set; }
        public string? Reason { get; set; }
        public string? ApprovedBy { get; set; }
        //public string? Remarks { get; set; }
        public bool? IsSpecial { get; set; }
     
    }
}
