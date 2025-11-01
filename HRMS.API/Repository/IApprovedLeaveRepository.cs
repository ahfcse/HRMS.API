using HRMS.API.Models;
using HRMS.API.UnitOfWork;

namespace HRMS.API.Repository
{
    public interface IApprovedLeaveRepository : IGenericRepository<Models.PMSLeaveDetails>
    {
        Task<List<Models.PMSLeaveDetails>> GetAllAsync();

    }
}
