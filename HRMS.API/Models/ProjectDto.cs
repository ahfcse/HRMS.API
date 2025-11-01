using Microsoft.EntityFrameworkCore;

namespace HRMS.API.Models
{
    [Keyless]
    public class ProjectDto
    {
        public int ProjectId { get; set; }
        public string Name { get; set; }
    }
}
