using HRMS.API.Data;
using HRMS.API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HRMS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DropDownController : ControllerBase
    {
        private readonly HrmDbContext _db;
        public DropDownController(HrmDbContext db)
        {
            _db = db;
        }
        [HttpGet("GetProjectById")]
        public async Task<ProjectDto> GetProjectById(int ProjectId)
        {
            var Data =await _db.ProjectDto.FromSqlRaw($"SELECT ProjectId,Name FROM ProjectSBHL WHERE ProjectId={ProjectId}").FirstOrDefaultAsync();
            return Data;

        }
        [HttpGet("GetProjects")]
        public async Task<List<ProjectDto>> GetProjects()
        {
            var Data = _db.ProjectDto.FromSqlRaw("SELECT ProjectId,Name FROM ProjectSBHL WHERE ProjectId<>1").ToList();
            return Data;

        }

        [HttpGet("GetDepartments")]
        public async Task<List<DepartmentDto>> GetDepartments()
        {
            var Data = _db.DepartmentDto.FromSqlRaw("SELECT PMSDepartmentId AS DepartmentId, DepartmentName FROM PMSDepartment WHERE PMSDepartmentId NOT IN(12,13,14,15,19,21)").ToList();
            return Data;

        }
        [HttpGet("GetEmployeesByDepartment")]
        public async Task<List<EmployeeDto>> GetEmployeesByDepartment(int DepartmentId)
        {
            var Data = _db.EmployeeDto.FromSqlRaw($"SELECT EmployeeId,EmpName FROM PMSEmpInfo WHERE DepartmentId={DepartmentId} AND IsActive=0 AND ProjectId=3").ToList();
          
            return Data;

        }
        [HttpGet("GetProducts")]
        public async Task<List<ProductDto>> GetProducts(int ProductCode)
        {
            var Data = _db.ProductDto.FromSqlRaw(@$"SELECT ProductCode,ProductName+' - '+u.Name+' - Code-'+CONVERT(VARCHAR(30),p.ProductCode) AS ProductName FROM Products P LEFT JOIN Unit U ON U.UnitId=P.UnitId").ToList();

            return Data;

        }
        [HttpGet("GetProductByCode")]
        public async Task<ProductDto> GetProductByCode(int ProductCode)
        {
            var Data =await _db.ProductDto.FromSqlRaw(@$"SELECT ProductCode,ProductName+' - '+u.Name+' - Code-'+CONVERT(VARCHAR(30),p.ProductCode) AS ProductName 
                                                        FROM Products P 
                                                        LEFT JOIN Unit U ON U.UnitId=P.UnitId 
                                                        WHERE P.ProductCode={ProductCode}").FirstOrDefaultAsync();



            return Data;

        }

        [HttpGet("GetSuppliers")]
        public async Task<List<SupplierDto>> GetSuppliers()
        {
            var Data = _db.SupplierDto.FromSqlRaw(@$"SELECT Supplierid,Name AS SupplierName FROM Supplier").ToList();

            return Data;

        }
    }
}
