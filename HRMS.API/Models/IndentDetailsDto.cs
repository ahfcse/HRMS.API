using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace HRMS.API.Models
{
    [Keyless]
    public class IndentDetailsDto
    {
        
        public int IndentDetailsID { get; set; }
        public int IndentNo { get; set; }
        public int IndentID { get; set; }
        public int ProductCode { get; set; }
        public string ProductName { get; set; }
        public int BalanceQuantity { get; set; }
        public decimal RequiredQuantity { get; set; }
        public decimal EstimateRate { get; set; }
        public decimal Discount { get; set; }
        //public decimal DiscountTk { get; set; }
        public decimal TotalAmount { get; set; }
        public int Supplierid { get; set; }
        public string? SupplierName { get; set; }
     
        public string? IndentRemarks { get; set; }
    }
}
