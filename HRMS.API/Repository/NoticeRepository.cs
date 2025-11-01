using HRMS.API.Data;
using HRMS.API.Models;
using Microsoft.EntityFrameworkCore;

namespace HRMS.API.Repository
{
    public class NoticeRepository : GenericRepository<Notice>, INoticeRepository
    {
        public NoticeRepository(HrmDbContext db) : base(db)
        {
        }

        //private readonly HrmDbContext _db;
        //public EmployeeRepository(HrmDbContext db)
        //{
        //    _db = db;
        //}
        public override async Task<List<Notice>> GetAllAsync()
        {
            return await base.GetAllAsync();
        }

        public override async Task<Notice> GetAsync(int id)
        {

            return await DbSet.FirstOrDefaultAsync(x => x.NoticeId == id);
        }

        public override async Task<bool> AddEntity(Notice entity)
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
        public override async Task<bool> UpdateEntity(Notice entity)
        {
            try
            {
                var existingData = await DbSet.FirstOrDefaultAsync(x => x.NoticeId == entity.NoticeId);
                if (existingData != null)
                {
                    existingData.RedNotice = entity.RedNotice;
                    existingData.YellowNotice = entity.YellowNotice;
                    existingData.GreenNotice = entity.GreenNotice;
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
                var existingData = await DbSet.FirstOrDefaultAsync(x => x.NoticeId == id);
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
