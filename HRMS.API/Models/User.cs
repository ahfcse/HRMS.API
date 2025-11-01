namespace HRMS.API.Models
{
    public class User
    {
        public int Id { get; set; }
        public int EmployeeId { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? ContactNo { get; set; }
        public string? IntercomNo { get; set; }
        public string? BloodGroup { get; set; }
        public string? NID { get; set; }
        public string? Password { get; set; }
        public bool? IsVerified { get; set; }
        public bool? IsActive { get; set; }
        public DateTime? ExpiryDate { get; set; }

    }
}
