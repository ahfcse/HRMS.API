using Microsoft.EntityFrameworkCore;

namespace HRMS.API.Models
{
    [Keyless]
    public class spRptPartialMrrReceived
    {
        public int ProductCode { get; set; }
        public string Projects { get; set; }
        public string Products { get; set; }
        public int RequiredQuantity { get; set; }
        public int ChalanQuantity { get; set; }
        public int Due_Quantity { get; set; }
        public int IndentNo { get; set; }
        public DateTime IndentDate { get; set; }
    }
}
