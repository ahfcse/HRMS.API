using Microsoft.EntityFrameworkCore;

namespace HRMS.API.Models
{
    [Keyless]
    public class spRPTCompanyInformation
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public string ShortName { get; set; }
    }
}
