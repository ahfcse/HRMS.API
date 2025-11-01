using Microsoft.EntityFrameworkCore;

namespace HRMS.API.Models
{
    [Keyless]
    public class spRptManagementTaskStatus
    {
        public string? Employee { get; set; }
        public string? TaskAssignDate { get; set; }
        public string? AssignBy { get; set; }
        public string? TaskName { get; set; }
        public string? DeadlineDate { get; set; }
        public string? Status { get; set; }
        public string? FromDate { get; set; }
        public string? ToDate { get; set; }

    }
}
