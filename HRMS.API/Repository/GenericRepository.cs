using HRMS.API.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Runtime.InteropServices;

namespace HRMS.API.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly HrmDbContext _db;
        internal DbSet<T> DbSet;
        public GenericRepository(HrmDbContext db)
        {
            _db = db;
            DbSet = _db.Set<T>();
        }
        public virtual async Task<T> GetAsync(int id)
        {
            
            throw new NotImplementedException();
        }
        public virtual async Task<List<T>> GetAllAsync()
        {
            var data =await DbSet.ToListAsync();
            return data;
        }

        public virtual async Task<bool> AddEntity(T entity)
        {
         
            throw new NotImplementedException();
        }

        public virtual async Task<bool> UpdateEntity(T entity)
        {
            throw new NotImplementedException();
        }

        public virtual async Task<bool> DeleteEntity(int id)
        {
            throw new NotImplementedException();
        }
       


      
    }
}
