using HRMS.API.Data;
using HRMS.API.Models;
using Microsoft.EntityFrameworkCore;

namespace HRMS.API.Repository
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        public UserRepository(HrmDbContext db) : base(db)
        {
        }

        //private readonly HrmDbContext _db;
        //public EmployeeRepository(HrmDbContext db)
        //{
        //    _db = db;
        //}
        public override async Task<List<User>> GetAllAsync()
        {
            return await base.GetAllAsync();
        }

        public override async Task<User> GetAsync(int EmployeeId)
        {

            return await DbSet.FirstOrDefaultAsync(x => x.EmployeeId == EmployeeId);
        }

        public override async Task<bool> AddEntity(User entity)
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
        public override async Task<bool> UpdateEntity(User entity)
        {
            try
            {
                var existingData = await DbSet.FirstOrDefaultAsync(x => x.Id == entity.Id);
                if (existingData != null)
                {
                    existingData.EmployeeId = entity.EmployeeId;
                    existingData.Password = entity.Password;
                    existingData.Email = entity.Email;
                    existingData.Name = entity.Name;
                    existingData.ContactNo = entity.ContactNo;
                    existingData.IntercomNo = entity.IntercomNo;
                    existingData.BloodGroup = entity.BloodGroup;
                    existingData.NID = entity.NID;
                 


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



    }
}
