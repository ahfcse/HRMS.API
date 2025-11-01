using HRMS.API.Models;

namespace HRMS.API.Repository
{
    public interface IAttendanceRepository:IGenericRepository<VwAttendace>
    {
        Task<List<VwAttendace>> GetAllAsync(int EmployeeId);
    }
}
