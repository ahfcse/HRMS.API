using HRMS.API.Data;
using HRMS.API.Globals;
using HRMS.API.Models;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Linq;

namespace HRMS.API.Repository
{
    public class MrrRepository : GenericRepository<Models.Mrr>, IMrrRepository
    {

        public MrrRepository(HrmDbContext db) : base(db)
        {

        }

        public async Task<List<Models.Mrr>> GetAllAsync()
        {


            return await base.GetAllAsync();

        }


        public override async Task<Models.Mrr> GetAsync(int id)
        {

            return await DbSet.FirstOrDefaultAsync(x => x.MRRId == id);
        }

        public override async Task<bool> AddEntity(Models.Mrr entity)
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
        public override async Task<bool> UpdateEntity(Models.Mrr entity)
        {
            try
            {
                var existingData = await DbSet.Include(o => o.MrrDetails).FirstOrDefaultAsync(x => x.MRRNo == entity.MRRNo);
                if (existingData != null)
                {
                    
                    existingData.MRRNo = entity.MRRNo;
                    existingData.MrrDate = entity.MrrDate;
                    existingData.ProjectId = entity.ProjectId;
                    existingData.MrrDetails = entity.MrrDetails;


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
                var existingData = await DbSet.Include(o => o.MrrDetails).FirstOrDefaultAsync(x => x.MRRNo == id);
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
