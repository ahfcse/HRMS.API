using HRMS.API.Data;
using HRMS.API.Migrations;
using HRMS.API.Models;
using HRMS.API.Repository;
using HRMS.API.UnitOfWork;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HRMS.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
   
    public class EmployeeController : Controller
    {
        private readonly HrmDbContext _db;
        //private readonly IDemoRepository _demoRepository;
        //private readonly IGenericRepository<Demo> _genericRepository;
        private readonly IUnitOfWork _unitOfWork;

        public EmployeeController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }


        //[Authorize]
        //[Authorize(Roles = "Admin,User")]
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            //var rowAffect = await _db.Database.ExecuteSqlAsync($"DELETE FROM Employees");

            var data = await _unitOfWork.Employees.GetAllAsync();
            return Ok(data);
        }

        [HttpGet("Get/{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var data = await _unitOfWork.Employees.GetAsync(id);
            return Ok(data);
        }
        [Authorize(Roles ="Admin")]
        [HttpPost("Create")]
        public async Task<IActionResult> Create(Employee entity)
        {
            var data = await _unitOfWork.Employees.AddEntity(entity);
            await _unitOfWork.CompletAsync();
            return Ok(data);
        }
        [HttpPut("Update")]
        public async Task<IActionResult> Update(Employee entity)
        {
            var data = await _unitOfWork.Employees.UpdateEntity(entity);
            await _unitOfWork.CompletAsync();
            return Ok(data);
        }
        [HttpDelete("Delete")]
        public async Task<IActionResult> Delete(int id)
        {
            var data = await _unitOfWork.Employees.DeleteEntity(id);
            await _unitOfWork.CompletAsync();
            return Ok(data);
        }




    }
}
