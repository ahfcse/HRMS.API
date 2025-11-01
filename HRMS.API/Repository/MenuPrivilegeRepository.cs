using HRMS.API.Data;
using HRMS.API.Models;
using Microsoft.EntityFrameworkCore;

namespace HRMS.API.Repository
{
    public class MenuPrivilegeRepository : GenericRepository<MenuPrivilege>, IMenuPrivilegeRepository
    {
        public MenuPrivilegeRepository(HrmDbContext db) : base(db)
        {
        }

        //private readonly HrmDbContext _db;
        //public EmployeeRepository(HrmDbContext db)
        //{
        //    _db = db;
        //}
        public override async Task<List<MenuPrivilege>> GetAllAsync()
        {
            return await base.GetAllAsync();
        }

        public override async Task<MenuPrivilege> GetAsync(int EmployeeId)
        {

            return await DbSet.FirstOrDefaultAsync(x => x.EmployeeId == EmployeeId);
        }

        public override async Task<bool> AddEntity(MenuPrivilege entity)
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
        public override async Task<bool> UpdateEntity(MenuPrivilege entity)
        {
            try
            {
                var existingData = await DbSet.FirstOrDefaultAsync(x => x.MenuPrivilegeId == entity.MenuPrivilegeId);
                if (existingData != null)
                {
                    existingData.MenuId = entity.MenuId;
                    existingData.EmployeeId = entity.EmployeeId;
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
                var existingData = await DbSet.FirstOrDefaultAsync(x => x.MenuPrivilegeId == id);
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

       

    }
}
