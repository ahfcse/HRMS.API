using Microsoft.EntityFrameworkCore;

namespace HRMS.API.Models
{
    [Keyless]
    public class spRptMrrNotFound
    {
        public int ProductCode { get; set; }
        public string Project { get; set; }
        public string Products { get; set; }
        public int RequiredQuantity { get; set; }
        public int BalanceQuantity { get; set; }
        public int IndentNo { get; set; }
        public DateTime IndentDate { get; set; }
    }
}
