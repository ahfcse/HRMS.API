using HRMS.API.Models;
using HRMS.API.UnitOfWork;

namespace HRMS.API.Repository
{
    public interface IGatePassRepository : IGenericRepository<Models.GatePass>
    {
        Task<List<Models.GatePass>> GetAllAsync();

    }
}
