using System.ComponentModel.DataAnnotations;

namespace HRMS.API.Models
{
    public class Mrr
    {

        [Key]
        public int MRRId { get; set; }
        public int MRRNo { get; set; }
        public DateTime MrrDate { get; set; }
        public int ProjectId { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; }
        public List<MrrDetails> MrrDetails { get; set;}
    }
}
