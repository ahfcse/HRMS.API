using HRMS.API.Data;
using HRMS.API.Models;
using Microsoft.EntityFrameworkCore;

namespace HRMS.API.Repository
{
    public class PrayerRepository : GenericRepository<Prayer>, IPrayerRepository
    {
        public PrayerRepository(HrmDbContext db) : base(db)
        {
        }

        //private readonly HrmDbContext _db;
        //public EmployeeRepository(HrmDbContext db)
        //{
        //    _db = db;
        //}
        public override async Task<List<Prayer>> GetAllAsync()
        {
            return await base.GetAllAsync();
        }

        public override async Task<Prayer> GetAsync(int id)
        {

            return await DbSet.FirstOrDefaultAsync(x => x.PrayerId == id);
        }

        public override async Task<bool> AddEntity(Prayer entity)
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
        public override async Task<bool> UpdateEntity(Prayer entity)
        {
            try
            {
                var existingData = await DbSet.FirstOrDefaultAsync(x => x.PrayerId == entity.PrayerId);
                if (existingData != null)
                {
                    existingData.Namaz = entity.Namaz;
                    existingData.Azan = entity.Azan;
                    existingData.Jamat = entity.Jamat;
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
                var existingData = await DbSet.FirstOrDefaultAsync(x => x.PrayerId == id);
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
