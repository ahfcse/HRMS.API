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

    [Authorize(Roles = "Admin,User")]
    public class MenuPrivilegeController : Controller
    {
        private readonly HrmDbContext _db;
        //private readonly IDemoRepository _demoRepository;
        //private readonly IGenericRepository<Demo> _genericRepository;
        private readonly IUnitOfWork _unitOfWork;

        public MenuPrivilegeController(IUnitOfWork unitOfWork,HrmDbContext db)
        {
            _unitOfWork = unitOfWork;
            _db = db;
        }

        [HttpGet("GetMenuPrevilege")]
        public async Task<IActionResult> GetMenuPrevilege()
        {
            int EmployeeId = Convert.ToInt32(this.User.Claims.First(claim => claim.Type == "EmployeeId").Value);
            var Data = _db.GetMenuPrevilege.FromSqlRaw($"EXEC GetMenuPrevilege {EmployeeId}").ToList();
            
            return Ok(Data);

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
        [HttpGet("GetMenu")]
        public List<VwMenuPrivilege> GetMenu()
        {
            int EmployeeId = Convert.ToInt32(this.User.Claims.First(claim => claim.Type == "EmployeeId").Value);
            var Menus = _db.VwMenuPrivileges.FromSqlRaw($"EXEC spGetMenus {EmployeeId}").ToList();

            return Menus;
        }

        [HttpGet("GetSubMenu")]
        public List<spGetSubMenus> GetSubMenu()
        {
            int EmployeeId = Convert.ToInt32(this.User.Claims.First(claim => claim.Type == "EmployeeId").Value);
            var Menus = _db.spGetSubMenus.FromSqlRaw($"EXEC spGetSubMenus {EmployeeId}").ToList();

            return Menus;
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            //var rowAffect = await _db.Database.ExecuteSqlAsync($"DELETE FROM Employees");

            var data = await _unitOfWork.MenuPrivileges.GetAllAsync();
            return Ok(data);
        }

        [HttpGet("Get")]
        public async Task<IActionResult> Get(int EmployeeId)
        {
            var data = await _unitOfWork.MenuPrivileges.GetAsync(EmployeeId);
            return Ok(data);
        }
        [HttpPost("Create")]
        public async Task<IActionResult> Create(MenuPrivilege entity)
        {
            var data = await _unitOfWork.MenuPrivileges.AddEntity(entity);
            await _unitOfWork.CompletAsync();
            return Ok(data);
        }
        [HttpPut("Update")]
        public async Task<IActionResult> Update(MenuPrivilege entity)
        {
            var data = await _unitOfWork.MenuPrivileges.UpdateEntity(entity);
            await _unitOfWork.CompletAsync();
            return Ok(data);
        }
        [HttpDelete("Delete")]
        public async Task<IActionResult> Delete(int id)
        {
            var data = await _unitOfWork.MenuPrivileges.DeleteEntity(id);
            await _unitOfWork.CompletAsync();
            return Ok(data);
        }




    }
}
