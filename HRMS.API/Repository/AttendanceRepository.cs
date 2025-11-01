using HRMS.API.Data;
using HRMS.API.Globals;
using HRMS.API.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace HRMS.API.Repository
{
    public class AttendanceRepository : GenericRepository<VwAttendace>, IAttendanceRepository
    {
        private readonly IHttpContextAccessor _contextAccessor;
        public AttendanceRepository(HrmDbContext db, IHttpContextAccessor contextAccessor) : base(db)
        {
            _contextAccessor = contextAccessor;

        }
        public async Task<List<VwAttendace>> GetAllAsync(int EmployeeId)
        {
            //int EmployeeId;

            //string UserId = _contextAccessor.HttpContext.Session.GetString("UserId");

            //EmployeeId = JsonConvert.DeserializeObject<int>(UserId);

            //int EmployeeId = 11;
            //var attendaceProccess = DbSet.FromSqlRaw("EXEC spAttendaceProcess @EmployeeId,@FromDate,@ToDate", 
            //    new SqlParameter("@EmployeeId", EmployeeId),
            //    new SqlParameter("@FromDate", DateTime.Today.AddMonths(-1)),
            //    new SqlParameter("@ToDate", DateTime.Now));

            List<VwAttendace> data=null;

            try
            {
                
             data = DbSet.FromSqlRaw("EXEC spLastThirtyAttendanceDetails @param1",
                     new SqlParameter("@param1", EmployeeId)).ToList();
            }
            catch (Exception ex) { }
            

        

            return data;
        }

    }
}
