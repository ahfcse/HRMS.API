using HRMS.API.Data;
using HRMS.API.Globals;
using HRMS.API.Models;
using HRMS.API.Repository;
using HRMS.API.UnitOfWork;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Data;

namespace HRMS.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]

    //[Authorize(Roles = "Admin,User")]
    public class MrrController : Controller
    {
        private readonly HrmDbContext _db;
        //private readonly IDemoRepository _demoRepository;
        //private readonly IGenericRepository<Demo> _genericRepository;
        private readonly IUnitOfWork _unitOfWork;

        public MrrController(IUnitOfWork unitOfWork, HrmDbContext db)
        {
            _unitOfWork = unitOfWork;
            _db = db;
        }



        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            //var rowAffect = await _db.Database.ExecuteSqlAsync($"DELETE FROM Employees");



            //int EmployeeId = Convert.ToInt32(this.User.Claims.First(claim => claim.Type == "EmployeeId").Value);

            var data = await _unitOfWork.Mrr.GetAllAsync();
            return Ok(data);
        }
        [HttpGet("GetMrrbyCode")]
        public async Task<MrrDto> GetMrrbyCode(int mrrNo)
        {
            MrrDto data = null;
            try
            {
                data = await _db.MrrDto.FromSqlRaw(@$"SELECT MRRID,MRRNo,CONVERT(VARCHAR(30),MrrDate,23) AS MrrDate,ProjectId,CreatedBy,CreatedAt FROM MRR WHERE MRRNo={mrrNo}").FirstOrDefaultAsync();
            }
            catch (Exception ex) { }
            return data;

        }

        [HttpGet("GetMrrDetailsbyCode")]
        public async Task<List<MrrDetailsDto>> GetMrrDetailsbyCode(int mrrNo)
        {
            List<MrrDetailsDto> data = null;
            try
            {
                data = await _db.MrrDetailsDto.FromSqlRaw(@$"SELECT MrrDetailsId,MrrNo,MD.ProductCode,ProductName+' - '+u.Name+' - Code-'+CONVERT(VARCHAR(30),p.ProductCode) AS ProductName ,ChalanNumber,  
													   CONVERT(VARCHAR(30),ChalanDate,23) AS ChalanDate
                                ,ChalanQuantity,IndentNo,BillNo,MrrRemarks,MRRId FROM MrrDetails MD
                                INNER JOIN Products P ON P.ProductCode=MD.ProductCode
                                   LEFT JOIN Unit U ON U.UnitId=P.UnitId  WHERE MD.MrrNo={mrrNo}").ToListAsync();
                
            }
            catch (Exception ex) { }
            return data;

        }

        [HttpGet("Get/{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var data = await _unitOfWork.Mrr.GetAsync(id);
            return Ok(data);
        }
        [HttpPost("Create")]
        public async Task<IActionResult> Create(Models.Mrr entity)
        {
            //task.EmployeeId = GlobalVariables.Instance.UserId;

            //entity.EmployeeId = Convert.ToInt32(this.User.Claims.First(claim => claim.Type == "EmployeeId").Value);
            //task.Date=DateTime.Now;
            entity.CreatedAt = DateTime.Now;
            entity.CreatedBy = Convert.ToInt32(this.User.Claims.First(claim => claim.Type == "EmployeeId").Value);
            entity.MrrDate = Convert.ToDateTime(entity.MrrDate);

            var data = await _unitOfWork.Mrr.AddEntity(entity);
            await _unitOfWork.CompletAsync();
            return Ok(data);
        }
        [HttpPut("Update")]
        public async Task<IActionResult> Update(Models.Mrr entity)
        {

            var data = await _unitOfWork.Mrr.UpdateEntity(entity);
            await _unitOfWork.CompletAsync();
            return Ok(data);
        }
        [HttpDelete("Delete")]
        public async Task<IActionResult> Delete(int id)
        {
            var data = await _unitOfWork.Mrr.DeleteEntity(id);
            await _unitOfWork.CompletAsync();
            return Ok(data);
        }

        [HttpGet("Search")]
        public async Task<List<GetIndents>> Search(string? indentNo, string? projectId, string? fromDate, string? toDate)
        {

            List<GetIndents> data = null;
            try
            {
                data = await _db.GetIndents.FromSqlRaw(@$"EXEC spSearchIndents {indentNo},{projectId},'{fromDate}','{toDate}'").ToListAsync();
            }
            catch (Exception ex) { }
            return data;

        }
        [HttpGet("SearchMrr")]
        public async Task<List<GetMrrs>> SearchMrr(string? mrrNo, string? projectId, string? fromDate, string? toDate)
        {

            List<GetMrrs> data = null;
            try
            {
                data = await _db.GetMrrs.FromSqlRaw(@$"EXEC spSearchMrrs {mrrNo},{projectId},'{fromDate}','{toDate}'").ToListAsync();
            }
            catch (Exception ex) { }
            return data;

        }


    }
}
