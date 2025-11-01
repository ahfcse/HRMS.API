using HRMS.API.Models;
using HRMS.API.UnitOfWork;

namespace HRMS.API.Repository
{
    public interface IMrrRepository : IGenericRepository<Models.Mrr>
    {
        Task<List<Models.Mrr>> GetAllAsync();


    }
}
