using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace HRMS.API.Models
{
    [Keyless]
    public class MrrDto
    {

       
        public int MRRId { get; set; }
        public int MRRNo { get; set; }
        public string MrrDate { get; set; }
        public int ProjectId { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; }
        public List<MrrDetailsDto> MrrDetailsDto { get; set;}
    }
}
