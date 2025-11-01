using Microsoft.EntityFrameworkCore;

namespace HRMS.API.Models
{
    [Keyless]
    public class GetProject
    {
        public int ProjectId { get; set; }
    }
}
