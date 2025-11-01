namespace HRMS.API.Models
{
    public class Employee
    {
        //public int Id { get; set; }
        public int EmployeeId { get; set; }
        public string EmployeeName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string DOJ { get; set; }
        public string DOB { get; set; }
        public string NID { get; set; }
        public string EmployeeStatus { get; set; }
        public int DepartmentId { get; set; }
        public int DesignationId { get; set;}

    }
}
