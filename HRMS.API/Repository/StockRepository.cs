using HRMS.API.Data;
using HRMS.API.Globals;
using HRMS.API.Models;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Linq;

namespace HRMS.API.Repository
{
    public class StockRepository : GenericRepository<Models.Item>, IStockRepository
    {

        public StockRepository(HrmDbContext db) : base(db)
        {

        }

        public async Task<List<Models.Item>> GetAllAsync()
        {


            return await base.GetAllAsync();

        }


        public override async Task<Models.Item> GetAsync(int id)
        {

            return await DbSet.FirstOrDefaultAsync(x => x.ItemID == id);
        }

        public override async Task<bool> AddEntity(Models.Item entity)
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
        public override async Task<bool> UpdateEntity(Models.Item entity)
        {
            try
            {
                var existingData = await DbSet.FirstOrDefaultAsync(x => x.ItemID == entity.ItemID);
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
                var existingData = await DbSet.FirstOrDefaultAsync(x => x.ItemID == id);
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
