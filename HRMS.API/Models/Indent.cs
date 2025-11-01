using Microsoft.AspNetCore.Http.HttpResults;
using System.ComponentModel.DataAnnotations;

namespace HRMS.API.Models
{
    public class Indent
    {
        [Key]
        public int IndentID { get; set; }

        public int IndentNo { get; set; }
        public DateTime IndentDate { get; set; }
        public int ProjectId { get; set; }
        //public int Total { get; set; }
        //public int TotalDiscount { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; }
        public List<IndentDetails> IndentDetails { get; set; }
    }
}
