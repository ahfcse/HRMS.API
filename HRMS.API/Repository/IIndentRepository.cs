using HRMS.API.Models;
using HRMS.API.UnitOfWork;

namespace HRMS.API.Repository
{
    public interface IIndentRepository : IGenericRepository<Models.Indent>
    {
        Task<List<Models.Indent>> GetAllAsync();


    }
}
