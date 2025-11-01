using HRMS.API.Data;
using HRMS.API.Models;
using Microsoft.EntityFrameworkCore;

namespace HRMS.API.Repository
{
    public class EmployeeRepository : GenericRepository<Employee>, IEmployeeRepository
    {
        public EmployeeRepository(HrmDbContext db) : base(db)
        {
        }

        //private readonly HrmDbContext _db;
        //public EmployeeRepository(HrmDbContext db)
        //{
        //    _db = db;
        //}
        public override async Task<List<Employee>> GetAllAsync()
        {
            return await base.GetAllAsync();
        }

        public override async Task<Employee> GetAsync(int id)
        {

            return await DbSet.FirstOrDefaultAsync(x => x.EmployeeId == id);
        }

        public override async Task<bool> AddEntity(Employee entity)
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
        public override async Task<bool> UpdateEntity(Employee entity)
        {
            try
            {
                var existingData = await DbSet.FirstOrDefaultAsync(x => x.EmployeeId == entity.EmployeeId);
                if (existingData != null)
                {
                    existingData.EmployeeId = entity.EmployeeId;
                    existingData.EmployeeName = entity.EmployeeName;
                    existingData.Email = entity.Email;
                    existingData.DOJ=entity.DOJ;
                    existingData.DOB=entity.DOB;    
                    existingData.EmployeeStatus = entity.EmployeeStatus;
                    existingData.PhoneNumber = entity.PhoneNumber;  
                    existingData.DepartmentId = entity.DepartmentId;    
                    existingData.DesignationId= entity.DesignationId;
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
                var existingData = await DbSet.FirstOrDefaultAsync(x => x.EmployeeId == id);
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
