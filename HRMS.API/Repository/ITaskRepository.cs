using HRMS.API.Models;
using HRMS.API.UnitOfWork;

namespace HRMS.API.Repository
{
    public interface ITaskRepository : IGenericRepository<Models.Task>
    {
        Task<List<Models.Task>> GetAllAsync(int EmployeeId);
        Task<List<Models.Task>> GetTaskByDate(int EmployeeId);
        Task<List<Models.Task>> GetAssignTask(int EmployeeId);
    }
}
