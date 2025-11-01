using Microsoft.EntityFrameworkCore;

namespace HRMS.API.Models
{

    [Keyless]
    public class UserAssign
    {
        public string AssignBy { get; set; }
        public string TaskName { get; set; }
        public string CompletedDate { get; set; }
    }
}
