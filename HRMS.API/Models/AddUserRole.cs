namespace HRMS.API.Models
{
    public class AddUserRole
    {
        public int EmployeeId { get; set; }
        public List<int> RoleIds { get; set; }
    }
}
