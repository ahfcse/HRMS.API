using Microsoft.EntityFrameworkCore;

namespace HRMS.API.Models
{
    [Keyless]
    public class ProductDto
    {
        public int ProductCode { get; set; }
        public string ProductName { get; set; }
    }
}