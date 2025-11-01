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
    public class IndentController : Controller
    {
        private readonly HrmDbContext _db;
        //private readonly IDemoRepository _demoRepository;
        //private readonly IGenericRepository<Demo> _genericRepository;
        private readonly IUnitOfWork _unitOfWork;

        public IndentController(IUnitOfWork unitOfWork,HrmDbContext db)
        {
            _unitOfWork = unitOfWork;
            _db = db;
        }



        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            //var rowAffect = await _db.Database.ExecuteSqlAsync($"DELETE FROM Employees");



            //int EmployeeId = Convert.ToInt32(this.User.Claims.First(claim => claim.Type == "EmployeeId").Value);

            var data = await _unitOfWork.Indent.GetAllAsync();
            return Ok(data);
        }
        [HttpGet("GetIndentbyCode")]
        public async Task<IndentDto> GetIndentbyCode(int indentNo)
        {
            IndentDto data = null;
            try
            {
                data = await _db.IndentDto.FromSqlRaw(@$"SELECT IndentID,IndentNo,CONVERT(VARCHAR(30),IndentDate,23) AS IndentDate,ProjectId,CreatedBy,CreatedAt FROM Indent WHERE IndentNo={indentNo}").FirstOrDefaultAsync();
            }
            catch (Exception ex) { }
            return data;

        }

        [HttpGet("GetIndentDetailsbyCode")]
        public async Task<List<IndentDetailsDto>> GetIndentDetailsbyCode(int indentNo)
        {
            List<IndentDetailsDto> data = null;
            try
            {
                data = await _db.IndentDetailsDto.FromSqlRaw(@$"SELECT IndentDetailsID,IndentNo,ID.ProductCode,ProductName+' - '+u.Name+' - Code-'+CONVERT(VARCHAR(30),p.ProductCode) AS ProductName,
                                                       BalanceQuantity,RequiredQuantity,EstimateRate,Discount,TotalAmount,S.Supplierid,S.Name AS SupplierName,IndentRemarks,IndentID FROM IndentDetails ID
                                                       INNER JOIN Products P ON P.ProductCode=ID.ProductCode
													   LEFT JOIN Supplier S ON S.Supplierid=ID.SupplierId
                                                       LEFT JOIN Unit U ON U.UnitId=P.UnitId WHERE ID.IndentNo={indentNo}").ToListAsync();
            }
            catch (Exception ex) { }
            return data;

        }

        [HttpGet("Get/{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var data = await _unitOfWork.Indent.GetAsync(id);
            return Ok(data);
        }
        [HttpGet("IsExixt")]
        public async Task<bool> IsExixt(int IndentNo)
        {
            var IsExixt = await _db.Indent.Where(x=>x.IndentNo==IndentNo).ToListAsync();
            if(IsExixt.Count == 0) {  return false; }
            return true;
        }
        [HttpPost("Create")]
        public async Task<IActionResult> Create(Models.Indent entity)
        {
            //task.EmployeeId = GlobalVariables.Instance.UserId;

            //entity.EmployeeId = Convert.ToInt32(this.User.Claims.First(claim => claim.Type == "EmployeeId").Value);
            //task.Date=DateTime.Now;
            entity.CreatedAt = DateTime.Now;
            entity.CreatedBy = Convert.ToInt32(this.User.Claims.First(claim => claim.Type == "EmployeeId").Value);
            entity.IndentDate = Convert.ToDateTime(entity.IndentDate);

            var data = await _unitOfWork.Indent.AddEntity(entity);
            await _unitOfWork.CompletAsync();
            return Ok(data);
        }
        [HttpPut("Update")]
        public async Task<IActionResult> Update(Models.Indent entity)
        {

            var data = await _unitOfWork.Indent.UpdateEntity(entity);
            await _unitOfWork.CompletAsync();
            return Ok(data);
        }
        [HttpDelete("Delete")]
        public async Task<IActionResult> Delete(int id)
        {
            var data = await _unitOfWork.Indent.DeleteEntity(id);
            await _unitOfWork.CompletAsync();
            return Ok(data);
        }

        [HttpGet("Search")]
        public async Task<List<GetIndents>> Search(string? indentNo, string? projectId,string? fromDate, string? toDate)
        {

            List<GetIndents> data = null;
            try
            {
                data = await _db.GetIndents.FromSqlRaw(@$"EXEC spSearchIndents {indentNo},{projectId},'{fromDate}','{toDate}'").ToListAsync();
            }
            catch (Exception ex) { }
            return data;

        }


    }
}
