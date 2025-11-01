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
    public class ApprovedLeaveController : Controller
    {
        private readonly HrmDbContext _db;
        //private readonly IDemoRepository _demoRepository;
        //private readonly IGenericRepository<Demo> _genericRepository;
        private readonly IUnitOfWork _unitOfWork;

        public ApprovedLeaveController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }



        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            //var rowAffect = await _db.Database.ExecuteSqlAsync($"DELETE FROM Employees");



            //int EmployeeId = Convert.ToInt32(this.User.Claims.First(claim => claim.Type == "EmployeeId").Value);

            var data = await _unitOfWork.ApprovedLeaves.GetAllAsync();
            return Ok(data);
        }
       

        [HttpGet("Get/{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var data = await _unitOfWork.ApprovedLeaves.GetAsync(id);
            return Ok(data);
        }
        [HttpPost("Create")]
        public async Task<IActionResult> Create(Models.PMSLeaveDetails entity)
        {
            //task.EmployeeId = GlobalVariables.Instance.UserId;

            //entity.EmployeeId = Convert.ToInt32(this.User.Claims.First(claim => claim.Type == "EmployeeId").Value);
            //task.Date=DateTime.Now;
            
            var data = await _unitOfWork.ApprovedLeaves.AddEntity(entity);
            await _unitOfWork.CompletAsync();
            return Ok(data);
        }
        [HttpPut("Update")]
        public async Task<IActionResult> Update(Models.PMSLeaveDetails entity)
        {
           
            var data = await _unitOfWork.ApprovedLeaves.UpdateEntity(entity);
            await _unitOfWork.CompletAsync();
            return Ok(data);
        }
        [HttpDelete("Delete")]
        public async Task<IActionResult> Delete(int id)
        {
            var data = await _unitOfWork.ApprovedLeaves.DeleteEntity(id);
            await _unitOfWork.CompletAsync();
            return Ok(data);
        }




    }
}
