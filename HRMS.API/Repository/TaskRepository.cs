using HRMS.API.Data;
using HRMS.API.Globals;
using HRMS.API.Models;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Linq;

namespace HRMS.API.Repository
{
    public class TaskRepository : GenericRepository<Models.Task>, ITaskRepository
    {
        private readonly IHttpContextAccessor _contextAccessor;
        public TaskRepository(HrmDbContext db, IHttpContextAccessor httpContextAccessor) : base(db)
        {
            _contextAccessor = httpContextAccessor;
        }
    
        public async Task<List<Models.Task>> GetAllAsync(int EmployeeId)
        {

            //int EmployeeId;

            //string UserId = _contextAccessor.HttpContext.Session.GetString("UserId");

            //EmployeeId = JsonConvert.DeserializeObject<int>(UserId);

            var data = DbSet.Where(x=>x.Status=="Incomplete" && x.EmployeeId==EmployeeId).ToList();
            
            return data;
        }
        public async Task<List<Models.Task>> GetTaskByDate(int EmployeeId)
        {

            //int EmployeeId;

            //string UserId = _contextAccessor.HttpContext.Session.GetString("UserId");

            //EmployeeId = JsonConvert.DeserializeObject<int>(UserId);
            List<Models.Task> data = null;

            try
            {
                data = DbSet.Where(x => x.Status != "Incomplete" && x.EmployeeId == EmployeeId && x.Date >= DateTime.Now.AddMonths(-1)).OrderByDescending(x => x.Date).ToList();
            }
            catch (Exception ex) { }

            return data;
        }
        public async Task<List<Models.Task>> GetAssignTask(int EmployeeId)
        {

            //int EmployeeId;

            //string UserId = _contextAccessor.HttpContext.Session.GetString("UserId");

            //EmployeeId = JsonConvert.DeserializeObject<int>(UserId);


            var data = DbSet.Where(x => x.Status != "Incomplete" && x.Status!= "Completed" && x.Status != "Ongoing" && x.EmployeeId == EmployeeId && x.AssignBy!="" && x.AssignBy != null).OrderByDescending(x => x.Date).ToList();

            return data;
        }

        public override async Task<Models.Task> GetAsync(int id)
        {

            return await DbSet.FirstOrDefaultAsync(x => x.TaskId == id);
        }

        public override async Task<bool> AddEntity(Models.Task entity)
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
        public override async Task<bool> UpdateEntity(Models.Task entity)
        {
            try
            {
                var existingData = await DbSet.FirstOrDefaultAsync(x => x.TaskId == entity.TaskId);
                if (existingData != null)
                {
                    existingData.TaskId = entity.TaskId;
                    existingData.TaskName = entity.TaskName;
                    existingData.Date = entity.Date;
                    //existingData.StartDate = entity.StartDate;
                    //existingData.EndDate = entity.EndDate;
                    existingData.CompletedDate = entity.CompletedDate;
                    existingData.Status = entity.Status;
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
                var existingData = await DbSet.FirstOrDefaultAsync(x => x.TaskId == id);
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
