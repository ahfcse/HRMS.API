using HRMS.API.Data;
using HRMS.API.Globals;
using HRMS.API.Migrations;
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
    public class StockController : Controller
    {
        private readonly HrmDbContext _db;
        //private readonly IDemoRepository _demoRepository;
        //private readonly IGenericRepository<Demo> _genericRepository;
        private readonly IUnitOfWork _unitOfWork;

        public StockController(IUnitOfWork unitOfWork, HrmDbContext db)
        {
            _unitOfWork = unitOfWork;
            _db = db;
        }



        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            //var rowAffect = await _db.Database.ExecuteSqlAsync($"DELETE FROM Employees");



            //int EmployeeId = Convert.ToInt32(this.User.Claims.First(claim => claim.Type == "EmployeeId").Value);

            var data = await _unitOfWork.Stock.GetAllAsync();
            return Ok(data);
        }


        [HttpGet("GetItem")]
        public async Task<IActionResult> GetItem()
        {
            int ProjectId = 0;

            int EmployeeId = Convert.ToInt32(this.User.Claims.First(claim => claim.Type == "EmployeeId").Value);
            var Data = _db.EmployeeID.FromSqlRaw($"SELECT TOP 1 ProjectId FROM PMSEmpInfo WHERE EmployeeId={EmployeeId}").ToList();

            foreach (var Project in Data)
            {
                ProjectId = Project.EmployeeId;
            }

            Item data=null;
            try
            {
                 data = await _db.Item.FirstOrDefaultAsync(x => x.ProjectId == ProjectId);
            }
            catch (Exception ex) { }
            return Ok(data);
        }

        [HttpGet("Get/{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var data = await _unitOfWork.Stock.GetAsync(id);
            return Ok(data);
        }

        [HttpPost("Create")]
        public async Task<IActionResult> Create(Models.Item entity)
        {
            //task.EmployeeId = GlobalVariables.Instance.UserId;

            //entity.EmployeeId = Convert.ToInt32(this.User.Claims.First(claim => claim.Type == "EmployeeId").Value);
            //task.Date=DateTime.Now;

            var data = await _unitOfWork.Stock.AddEntity(entity);
            await _unitOfWork.CompletAsync();
            return Ok(data);
        }



        [HttpPut("Update")]
        public async Task<IActionResult> Update(Models.Item entity)
        {

            var data = await _unitOfWork.Stock.UpdateEntity(entity);
            await _unitOfWork.CompletAsync();
            return Ok(data);
        }
        [HttpDelete("Delete")]
        public async Task<IActionResult> Delete(int id)
        {
            var data = await _unitOfWork.Stock.DeleteEntity(id);
            await _unitOfWork.CompletAsync();
            return Ok(data);
        }




    }
}
