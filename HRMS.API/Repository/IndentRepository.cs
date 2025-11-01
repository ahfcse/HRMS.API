using HRMS.API.Data;
using HRMS.API.Globals;
using HRMS.API.Models;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Linq;

namespace HRMS.API.Repository
{
    public class IndentRepository : GenericRepository<Models.Indent>, IIndentRepository
    {

        public IndentRepository(HrmDbContext db) : base(db)
        {

        }

        public async Task<List<Models.Indent>> GetAllAsync()
        {


            return await base.GetAllAsync();

        }


        public override async Task<Models.Indent> GetAsync(int id)
        {

            return await DbSet.FirstOrDefaultAsync(x => x.IndentID == id);
        }

        public override async Task<bool> AddEntity(Models.Indent entity)
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
        public override async Task<bool> UpdateEntity(Models.Indent entity)
        {
            try
            {
                var existingData = await DbSet.Include(o=>o.IndentDetails).FirstOrDefaultAsync(x => x.IndentNo == entity.IndentNo);
                if (existingData != null)
                {
                    existingData.IndentID = entity.IndentID;
                    existingData.IndentNo = entity.IndentNo;
                    existingData.IndentDate = entity.IndentDate;
                    existingData.ProjectId = entity.ProjectId;
                    existingData.IndentDetails = entity.IndentDetails;
                   

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
                var existingData = await DbSet.Include(o => o.IndentDetails).FirstOrDefaultAsync(x => x.IndentNo == id);
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
