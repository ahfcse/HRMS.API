using HRMS.API.Data;
using HRMS.API.Models;
using HRMS.API.Repository;
using HRMS.API.UnitOfWork;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace HRMS.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]

    //[Authorize(Roles = "Admin,User")]
    public class DemoController : Controller
    {
        private readonly HrmDbContext _db;
        //private readonly IDemoRepository _demoRepository;
        //private readonly IGenericRepository<Demo> _genericRepository;
        private readonly IUnitOfWork _unitOfWork;

        public DemoController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }


        //public DemoController(HrmDbContext db,IDemoRepository demoRepository, IGenericRepository<Demo> genericRepository,IUnitOfWork unitOfWork)
        //{
        //    _db = db;
        //    _demoRepository = demoRepository;
        //    _genericRepository = genericRepository;
        //    _unitOfWork = unitOfWork;   
        //}




        //[HttpGet]
        //public async Task<IActionResult> GetAll()
        //{

        //    var data = await _genericRepository.GetAll();

        //    return Ok(data);
        //}

        //[HttpGet]
        //public async Task<IActionResult> GetAll()
        //{

        //    var data = await _demoRepository.GetAll();

        //    return Ok(data);
        //}


        //[HttpGet]
        //public async Task<IActionResult> GetAll()
        //{

        //    var data = await _db.Employees.ToListAsync();

        //    return Ok(data);
        //}


        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            //var rowAffect = await _db.Database.ExecuteSqlAsync($"DELETE FROM Employees");

            var data = await _unitOfWork.Demos.GetAllAsync();
            return Ok(data);
        }

        [HttpGet("Get/{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var data=await _unitOfWork.Demos.GetAsync(id);
            return Ok(data);
        }
        [HttpPost("Create")]
        public async Task<IActionResult>Create(Demo entity)
        {
            var data=await _unitOfWork.Demos.AddEntity(entity);
                       await _unitOfWork.CompletAsync();
            return Ok(data);
        }
        [HttpPut("Update")]
        public async Task<IActionResult> Update(Demo entity)
        {
            var data = await _unitOfWork.Demos.UpdateEntity(entity);
            await _unitOfWork.CompletAsync();
            return Ok(data);
        }
        [HttpDelete("Delete")]
        public async Task<IActionResult> Delete(int id)
        {
            var data = await _unitOfWork.Demos.DeleteEntity(id);
            await _unitOfWork.CompletAsync();
            return Ok(data);
        }




    }
}
