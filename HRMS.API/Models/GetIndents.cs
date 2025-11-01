using Microsoft.EntityFrameworkCore;

namespace HRMS.API.Models
{
    [Keyless]
    public class GetIndents
    {
        public long SL { get; set; }
        public int IndentID { get; set; }
        public int IndentNo { get; set; }
        public DateTime IndentDate { get; set; }
        public string ProjectName { get; set; }
    }
}
