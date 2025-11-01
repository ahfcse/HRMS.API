using Microsoft.EntityFrameworkCore;

namespace HRMS.API.Models
{
    [Keyless]
    public class DashBoard
    {
        public bool? IsGatePassDashBoard { get; set; }
    }
}
