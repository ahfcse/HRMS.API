
using Microsoft.EntityFrameworkCore;

namespace HRMS.API.Models
{
    [Keyless]
    public class SupplierDto
    {
        public int Supplierid { get; set; }
        public string? SupplierName { get; set; }
    }
}
