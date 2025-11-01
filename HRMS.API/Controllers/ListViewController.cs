using HRMS.API.Data;
using HRMS.API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data.SqlClient;

namespace HRMS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ListViewController : ControllerBase
    {
        private readonly HrmDbContext _db;
        public ListViewController(HrmDbContext db)
        {
            _db = db;
        }
        [HttpGet("GetApprovedLeaves")]
        public async Task<List<lvApprovedLeave>> GetApprovedLeaves([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {

            var data = _db.lvApprovedLeave.FromSqlRaw("EXEC lvApprovedLeave").AsEnumerable().Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToList();
            return data;
        }
        [HttpGet("GetIndents")]
        public async Task<List<GetIndents>> GetIndents([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {

            var data = _db.GetIndents.FromSqlRaw("EXEC GetIndents").AsEnumerable().Skip((page - 1) * pageSize)
            .Take(pageSize).OrderByDescending(t=>t.IndentID)
            .ToList();
            return data;
        }
        [HttpGet("GetMrrs")]
        public async Task<List<GetMrrs>> GetMrrs([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {

            var data = _db.GetMrrs.FromSqlRaw("EXEC GetMrrs").AsEnumerable().Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToList();
            return data;
        }
    }
}
