using System.ComponentModel.DataAnnotations;

namespace HRMS.API.Models
{
    public class Task
    {
        [Key]
        public int TaskId { get; set; }
        public int EmployeeId { get; set; }
        public string? TaskName { get; set; }
        public DateTime? Date { get; set; }

        public DateTime? DeadlineDate { get; set; }

        //public string Remarks { get; set; }
        //public int UrgencyId { get; set; }



        //public DateTime DeadlineTime { get; set; }
        public string? AssignBy { get; set; }
        //public DateTime Std { get; set; }
        //public DateTime Etd { get; set; }
        //public bool IsFinished { get; set; }
        //public bool IsRecurring { get; set; }
        //public string RecurranceType { get; set; }
        //public string RecurranceValue { get;set; }
        //public bool IsActive { get; set; }
        //public DateTime CreatedAt { get; set; }
        //public string CreatedIP { get; set; }
        //public int UpdatedBy { get; set; }
        //public DateTime UpdatedAt { get; set; }
        //public string UpdatedIP { get; set; }
        //public bool AssignNotify { get; set; }
        //public bool StartNotify { get; set; }
        //public bool FinishedNotify { get; set; }
        //public int Priority { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime CompletedDate { get; set; }
        public string Status { get; set; }


    }
}
