using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace HRMS.API.Models
{
    [Keyless]
    public class IndentDto
    {
       
        public int IndentID { get; set; }
        public int IndentNo { get; set; }
        public string IndentDate { get; set; }
        public int ProjectId { get; set; }
        //public int Total { get; set; }
        //public int TotalDiscount { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; }
        public List<IndentDetailsDto> IndentDetailsDto { get; set; }
    }
}
