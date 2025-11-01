using HRMS.API.Models;

namespace HRMS.API.Repository
{
    public interface IGenericRepository<T>where T : class
    {
        Task<T> GetAsync(int id);
        Task<List<T>> GetAllAsync();
        Task<bool> AddEntity(T entity);
        Task<bool> UpdateEntity(T entity);
        Task<bool> DeleteEntity(int id);
      

    }
}
