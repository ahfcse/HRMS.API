using HRMS.API.Data;
using HRMS.API.Models;
using Microsoft.EntityFrameworkCore;

namespace HRMS.API.Repository
{
    public class DemoRepository :GenericRepository<Demo>,IDemoRepository
    {
        public DemoRepository(HrmDbContext db) : base(db)
        {
        }

        //private readonly HrmDbContext _db;
        //public EmployeeRepository(HrmDbContext db)
        //{
        //    _db = db;
        //}
        public override async Task<List<Demo>> GetAllAsync()
        {
            return await base.GetAllAsync();
        }

        public override async Task<Demo> GetAsync(int id)
        {

            return await DbSet.FirstOrDefaultAsync(x => x.Id == id);
        }

        public override async Task<bool> AddEntity(Demo entity)
        {
           try
            {
                
               await DbSet.AddAsync(entity);
                return true;
                
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public override async Task<bool> UpdateEntity(Demo entity)
        {
            try
            {
                var existingData= await DbSet.FirstOrDefaultAsync(x => x.Id == entity.Id);
                if (existingData != null) 
                {
                    existingData.DemoId=entity.DemoId;
                    existingData.DemoName=entity.DemoName;
                    return true;
                }
                return false;
           
              

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async override Task<bool> DeleteEntity(int id)
        {
            try
            {
                var existingData = await DbSet.FirstOrDefaultAsync(x => x.Id == id);
                if (existingData != null)
                {
                    DbSet.Remove(existingData);
                    return true;
                }
                return false;



            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Test()
        {

        }
      
    }
}
