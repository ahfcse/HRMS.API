namespace HRMS.API.Models
{
    public class Attendance
    {
        //public int Id { get; set; }
        public int? AttendanceId { get; set; }
        public int? EmployeeId { get; set; }
        public DateTime? WorkDate { get; set; }
        public TimeSpan? InTime { get; set; }
        public TimeSpan? OutTime { get; set; }
        public string? DayStatus { get; set; }
        public string? Remarks { get; set; }
        public string? PersonalSMS { get; set; }
    }
}
