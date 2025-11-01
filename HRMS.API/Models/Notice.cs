namespace HRMS.API.Models
{
    public class Notice
    {
        public int NoticeId { get; set; }
        public string? RedNotice { get; set; }
        public string? YellowNotice { get; set; }
        public string? GreenNotice { get; set; }
        public string? HeaderNotice { get; set; }
    }
}
