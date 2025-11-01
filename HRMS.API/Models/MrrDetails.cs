using System.ComponentModel.DataAnnotations;

namespace HRMS.API.Models
{
    public class MrrDetails
    {
        [Key]
        public int MrrDetailsId { get; set; }
        public int MrrNo { get; set; }
        public int ProductCode { get; set; }
        //public string ProductName { get; set; }
        public int ChalanNumber { get; set; }
        public DateTime ChalanDate { get; set; }
        public int ChalanQuantity { get; set; }
        public int IndentNo { get; set; }
        public int BillNo { get; set; }
        public string MrrRemarks { get; set; }
        public int MRRId { get; set; }
    }
}
