using HRMS.API.Models;
using HRMS.API.UnitOfWork;

namespace HRMS.API.Repository
{
    public interface IStockRepository : IGenericRepository<Models.Item>
    {
        Task<List<Models.Item>> GetAllAsync();

    }
}
