using Microsoft.EntityFrameworkCore;

namespace HRMS.API.Models
{
    [Keyless]
    public class GetMrrs
    {
        public int MrrID { get; set; }
        public int MrrNo { get; set; }
        public DateTime MrrDate { get; set; }
        public string ProjectName { get; set; }
    }
}
