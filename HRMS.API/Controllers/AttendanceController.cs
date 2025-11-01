using Azure.Core;
using HRMS.API.Data;
using HRMS.API.Globals;
using HRMS.API.UnitOfWork;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace HRMS.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "Admin,User")]
    public class AttendanceController : Controller
    {
       private readonly IUnitOfWork _unitOfWork;
        private readonly HrmDbContext _db;
        private readonly IHttpContextAccessor _contextAccessor;
        public AttendanceController(IUnitOfWork unitOfWork,HrmDbContext db,IHttpContextAccessor httpContextAccessor)
        {
            _unitOfWork = unitOfWork;
            _db = db;
            _contextAccessor = httpContextAccessor;
        }


        
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            //var header = HttpContext.Request.Headers["Authorization"];


            int EmployeeId = Convert.ToInt32(this.User.Claims.First(claim => claim.Type == "EmployeeId").Value);


            //int EmployeeId;

            //string UserId = _contextAccessor.HttpContext.Session.GetString("UserId");

            //EmployeeId=JsonConvert.DeserializeObject<int>(UserId);

            //int EmployeeId = GlobalVariables.Instance.UserId;
            try
            {
                var rowAffect = await _db.Database.ExecuteSqlAsync($"EXEC spAttendaceProcess {EmployeeId},{DateTime.Today.AddMonths(-1)},{DateTime.Now}");
            }
            catch (Exception ex) { }
             //rowAffect = await _db.Database.ExecuteSqlAsync($"EXEC spAttendaceProcess {EmployeeId},{DateTime.Today.AddMonths(-1)},{DateTime.Now}");
            var data= await _unitOfWork.Attendances.GetAllAsync(EmployeeId);
            return Ok(data);
        }

    }
}
