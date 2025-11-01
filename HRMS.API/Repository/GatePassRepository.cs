using HRMS.API.Data;
using HRMS.API.Globals;
using HRMS.API.Models;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Linq;

namespace HRMS.API.Repository
{
    public class GatePassRepository : GenericRepository<Models.GatePass>, IGatePassRepository
    {

        public GatePassRepository(HrmDbContext db) : base(db)
        {

        }

        public async Task<List<Models.GatePass>> GetAllAsync()
        {


            return await base.GetAllAsync();

        }


        public override async Task<Models.GatePass> GetAsync(int id)
        {

            return await DbSet.FirstOrDefaultAsync(x => x.GatePassNo == id);
        }

        public override async Task<bool> AddEntity(Models.GatePass entity)
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
        public override async Task<bool> UpdateEntity(Models.GatePass entity)
        {
            try
            {
                var existingData = await DbSet.FirstOrDefaultAsync(x => x.GatePassNo == entity.GatePassNo);
                if (existingData != null)
                {
                    //existingData.TaskId = entity.TaskId;
                    //existingData.TaskName = entity.TaskName;
                    //existingData.Date = entity.Date;
                    ////existingData.StartDate = entity.StartDate;
                    ////existingData.EndDate = entity.EndDate;
                    //existingData.CompletedDate = entity.CompletedDate;
                    //existingData.Status = entity.Status;
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
                var existingData = await DbSet.FirstOrDefaultAsync(x => x.GatePassNo == id);
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
