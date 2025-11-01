using HRMS.API.Models;
using Microsoft.EntityFrameworkCore;

namespace HRMS.API.Data
{
    public class HrmDbContext : DbContext
    {
        public HrmDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Demo> Demos { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Attendance> Attendance { get; set; }
        public DbSet<Employee> PMSEmpInfo { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<Models.Task> Task { get; set; }
        public DbSet<Models.VwAttendace> vwAttendaces { get; set; }
        public DbSet<Models.VwUser> VwUser { get; set; }
        public DbSet<EmployeeImage> EmployeeImages { get; set; }
        public DbSet<Notice> Notices { get; set; }
        public DbSet<Prayer> Prayers { get; set; }
        public DbSet<Menu> Menus { get; set; }
        public DbSet<MenuPrivilege> MenuPrivileges { get; set; }
        public DbSet<VwMenuPrivilege> VwMenuPrivileges { get; set; }
        public DbSet<spRptAttendanceDetails> spRptAttendanceDetails { get; set; }
        public DbSet<spRPTCompanyInformation> spRPTCompanyInformation { get; set; }
        public DbSet<DepartmentDto> DepartmentDto { get; set; }
        public DbSet<EmployeeDto> EmployeeDto { get; set; }
        public DbSet<spRptTaskDetails> spRptTaskDetails { get; set; }
        public DbSet<spRptAttendanceSummery> spRptAttendanceSummery { get; set; }
        public DbSet<spLateSummery> spLateSummery { get; set; }
        public DbSet<spAttendenceSummeryAll> spAttendenceSummeryAll { get; set; }
        public DbSet<EmployeesRS> EmployeesRS { get; set; }
        public DbSet<PMSLeaveDetails> PMSLeaveDetails { get; set; }
        public DbSet<lvApprovedLeave> lvApprovedLeave { get; set; }
        public DbSet<GatePass> GatePass { get; set; }
        public DbSet<GatePassDto> GatePassDto { get; set; }
        public DbSet<ProjectDto> ProjectDto { get; set; }
        public DbSet<GetProject> GetProject { get; set; }
        public DbSet<GetMenuPrevilege> GetMenuPrevilege { get; set; }
        public DbSet<spGetSubMenus> spGetSubMenus { get; set; }
        public DbSet<Item> Item { get; set; }
        public DbSet<EmployeeID> EmployeeID { get; set; }
        public DbSet<UserMail> UserMail { get; set; }
        public DbSet<DashBoard> DashBoard { get; set; }
        public DbSet<UserAssign> UserAssign { get; set; }
        public DbSet<spRptManagementTaskStatus> spRptManagementTaskStatus { get; set; }
        public DbSet<Indent> Indent { get; set; }
        public DbSet<IndentDetails> IndentDetails { get; set; }
        public DbSet<IndentDto> IndentDto { get; set; }
        public DbSet<IndentDetailsDto> IndentDetailsDto { get; set; }
        public DbSet<ProductDto> ProductDto { get; set; }
        public DbSet<GetIndents> GetIndents { get; set; }
        public DbSet<Mrr> Mrr { get; set; }
        public DbSet<MrrDetails> MrrDetails { get; set; }
        public DbSet<MrrDto> MrrDto { get; set; }
        public DbSet<MrrDetailsDto> MrrDetailsDto { get; set; }
        public DbSet<GetMrrs> GetMrrs { get; set; }
        public DbSet<spRptPartialMrrReceived> spRptPartialMrrReceived { get; set; }
        public DbSet<spRptMrrNotFound> spRptMrrNotFound { get; set; }
        public DbSet<SupplierDto> SupplierDto { get; set; }



    }
}
 