namespace HRMS.API.Models
{
    public class EmployeeImage
    {
        public int EmployeeImageId { get; set; }
        public int EmployeeId { get; set; }
        public byte[] Image { get; set; }
    }
}
