using HRMS.API.Data;
using HRMS.API.Models;
using Microsoft.EntityFrameworkCore;

namespace HRMS.API.Repository
{
    public class MenuRepository : GenericRepository<Menu>, IMenuRepository
    {
        public MenuRepository(HrmDbContext db) : base(db)
        {
        }

        //private readonly HrmDbContext _db;
        //public EmployeeRepository(HrmDbContext db)
        //{
        //    _db = db;
        //}
        public override async Task<List<Menu>> GetAllAsync()
        {
            return await base.GetAllAsync();
        }

        public override async Task<Menu> GetAsync(int id)
        {

            return await DbSet.FirstOrDefaultAsync(x => x.MenuId == id);
        }

        public override async Task<bool> AddEntity(Menu entity)
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
        public override async Task<bool> UpdateEntity(Menu entity)
        {
            try
            {
                var existingData = await DbSet.FirstOrDefaultAsync(x => x.MenuId == entity.MenuId);
                if (existingData != null)
                {
                    existingData.MenuName = entity.MenuName;
                    existingData.ParentId = entity.ParentId;
                    existingData.RouterLink = entity.RouterLink;
                    existingData.IsVisible = entity.IsVisible;
                    existingData.Title = entity.Title;
                    existingData.Icon = entity.Icon;
              

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
                var existingData = await DbSet.FirstOrDefaultAsync(x => x.MenuId == id);
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
