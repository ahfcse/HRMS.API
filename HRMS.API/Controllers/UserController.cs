using HRMS.API.Data;
using HRMS.API.Migrations;
using HRMS.API.Models;
using HRMS.API.Repository;
using HRMS.API.UnitOfWork;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Data;
using User = HRMS.API.Models.User;
using System.Web;
using HRMS.API.Globals;
using static System.Net.Mime.MediaTypeNames;
using System.Collections.Generic;
using Syncfusion.DocIO.DLS;
using Syncfusion.Presentation;

namespace HRMS.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "Admin,User")]
    public class UserController : Controller
    {
        private readonly HrmDbContext _db;
        //private readonly IDemoRepository _demoRepository;
        //private readonly IGenericRepository<Demo> _genericRepository;
        private readonly IUnitOfWork _unitOfWork;
        //private readonly IHttpContextAccessor _contextAccessor;
        private readonly IWebHostEnvironment environment;

        public UserController(IUnitOfWork unitOfWork, IWebHostEnvironment environment,HrmDbContext db)
        {
            _unitOfWork = unitOfWork;
            this.environment = environment;
            _db = db;
        }

        [HttpGet("GetAllUsers")]
        public List<VwUser> GetAllAsync()
        {

            var users = _db.VwUser.FromSqlRaw("EXEC PhoneBook").ToList();

            return users;
        }


        [HttpGet("IsGatePassDashBoard")]
        public async Task<bool> IsGatePassDashBoard()
        {
            int EmployeeId = Convert.ToInt32(this.User.Claims.First(claim => claim.Type == "EmployeeId").Value);


            try
            {

                //Use Only FromSql for single data and for multiple data use FromSqlRaw
                var DashBoards =await _db.DashBoard.FromSqlRaw($"SELECT IsGatePassDashBoard FROM Users WHERE EmployeeId= {EmployeeId}").ToListAsync();
                if (DashBoards != null)
                {
                    foreach (var DashBoard in DashBoards)
                    {
                       if(DashBoard.IsGatePassDashBoard==true)
                        {
                            return true;
                        }
                       else
                        {
                           return false;
                        }
                    }
                }
                else
                {
                    return false;
                }


            }
            catch (Exception ex) { }

            return false;
        }


        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var data = await _unitOfWork.Users.GetAllAsync();
            return Ok(data);
        }

        [HttpGet("Get")]
        public async Task<IActionResult> Get()
        {
            int EmployeeId = Convert.ToInt32(this.User.Claims.First(claim => claim.Type == "EmployeeId").Value);
            var data = await _unitOfWork.Users.GetAsync(EmployeeId);
            return Ok(data);
        }
        [HttpGet("GetUserName")]
        public async Task<IActionResult> GetUserName()
        {
            string UserName = this.User.Claims.First(claim => claim.Type == "Name").Value;
            return Ok(JsonConvert.SerializeObject(UserName));
        }
        [HttpPost("Create")]
        public async Task<IActionResult> Create(User entity)
        {
            var data = await _unitOfWork.Users.AddEntity(entity);
            await _unitOfWork.CompletAsync();
            return Ok(data);
        }
        [HttpPut("Update")]
        public async Task<IActionResult> Update(User entity)
        {

            var data = await _unitOfWork.Users.UpdateEntity(entity);
            await _unitOfWork.CompletAsync();
            return Ok(data);
        }
        [HttpDelete("Delete")]
        public async Task<IActionResult> Delete(int id)
        {
            var data = await _unitOfWork.Users.DeleteEntity(id);
            await _unitOfWork.CompletAsync();
            return Ok(data);
        }

        [HttpPost("AddEmployeeImage")]
        public async Task<IActionResult> DBMultiUploadImage(IFormFile file)
        {
            int EmployeeId = Convert.ToInt32(this.User.Claims.First(claim => claim.Type == "EmployeeId").Value);
            var rowAffect = await _db.Database.ExecuteSqlAsync($"DELETE FROM EmployeeImages WHERE EmployeeId={EmployeeId}");

            APIResponse response = new APIResponse();
            int passcount = 0; int errorcount = 0;
            try
            {
                using (MemoryStream stream = new MemoryStream())
                {
                    await file.CopyToAsync(stream);
                    this._db.EmployeeImages.Add(new EmployeeImage()
                    {
                        EmployeeId = EmployeeId,
                        Image = stream.ToArray(),

                    });
                  
                    await this._db.SaveChangesAsync();
                    passcount++;
                }



            }
            catch (Exception ex)
            {
                errorcount++;
                response.Errormessage = ex.Message;
            }
            response.ResponseCode = 200;
            response.Result = passcount + " Files uploaded &" + errorcount + " files failed";
            return Ok(response);
        }


        [HttpGet("GetEmployeeImage")]
        public async Task<IActionResult> GetDBMultiImage()
        {
            int EmployeeId = Convert.ToInt32(this.User.Claims.First(claim => claim.Type == "EmployeeId").Value);
            List<string> Imageurl = new List<string>();
            string hosturl = $"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}";
            try
            {
                var _image = this._db.EmployeeImages.Where(item => item.EmployeeId == EmployeeId).ToList().OrderByDescending(item=>item.EmployeeImageId);
                if (_image != null)
                {
                    //_image.ForEach(item =>
                    //{
                    //    Imageurl.Add(Convert.ToBase64String(item.Image));
                        
                    //});

                    foreach(var item in _image)
                    {
                        Imageurl.Add(Convert.ToBase64String(item.Image));
                        break;
                    }
                }
                else
                {
                    return NotFound();
                }


            }
            catch (Exception ex)
            {
            }
            return Ok(Imageurl);

        }
        //[HttpGet("GetUserImage")]
        //public async Task<IActionResult> GetUserImage()
        //{

        //    string image= this.User.Claims.First(claim => claim.Type == "Image").Value;
        //    return Ok(image);

        //}


        //[HttpPut("UploadImage")]
        //public async Task<IActionResult> UploadImage(IFormFile formFile, string productcode)
        //{
        //    APIResponse response = new APIResponse();
        //    try
        //    {
        //        string Filepath = GetFilepath(productcode);
        //        if (!System.IO.Directory.Exists(Filepath))
        //        {
        //            System.IO.Directory.CreateDirectory(Filepath);
        //        }

        //        string imagepath = Filepath + "\\" + productcode + ".png";
        //        if (System.IO.File.Exists(imagepath))
        //        {
        //            System.IO.File.Delete(imagepath);
        //        }
        //        using (FileStream stream = System.IO.File.Create(imagepath))
        //        {
        //            await formFile.CopyToAsync(stream);
        //            response.ResponseCode = 200;
        //            response.Result = "pass";
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        response.Errormessage = ex.Message;
        //    }
        //    return Ok(response);
        //}

        //[HttpPut("MultiUploadImage")]
        //public async Task<IActionResult> MultiUploadImage(IFormFileCollection filecollection, string productcode)
        //{

        //    APIResponse response = new APIResponse();
        //    int passcount = 0; int errorcount = 0;
        //    try
        //    {
        //        string Filepath = GetFilepath(productcode);
        //        if (!System.IO.Directory.Exists(Filepath))
        //        {
        //            System.IO.Directory.CreateDirectory(Filepath);
        //        }
        //        foreach (var file in filecollection)
        //        {
        //            string imagepath = Filepath + "\\" + file.FileName;
        //            if (System.IO.File.Exists(imagepath))
        //            {
        //                System.IO.File.Delete(imagepath);
        //            }
        //            using (FileStream stream = System.IO.File.Create(imagepath))
        //            {
        //                await file.CopyToAsync(stream);
        //                passcount++;

        //            }
        //        }


        //    }
        //    catch (Exception ex)
        //    {
        //        errorcount++;
        //        response.Errormessage = ex.Message;
        //    }
        //    response.ResponseCode = 200;
        //    response.Result = passcount + " Files uploaded &" + errorcount + " files failed";
        //    return Ok(response);
        //}

        //[HttpGet("GetImage")]
        //public async Task<IActionResult> GetImage(string productcode)
        //{
        //    string Imageurl = string.Empty;
        //    string hosturl = $"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}";
        //    try
        //    {
        //        string Filepath = GetFilepath(productcode);
        //        string imagepath = Filepath + "\\" + productcode + ".png";
        //        if (System.IO.File.Exists(imagepath))
        //        {
        //            Imageurl = hosturl + "/Upload/product/" + productcode + "/" + productcode + ".png";
        //        }
        //        else
        //        {
        //            return NotFound();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //    }
        //    return Ok(Imageurl);

        //}

        //[HttpGet("GetMultiImage")]
        //public async Task<IActionResult> GetMultiImage(string productcode)
        //{
        //    List<string> Imageurl = new List<string>();
        //    string hosturl = $"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}";
        //    try
        //    {
        //        string Filepath = GetFilepath(productcode);

        //        if (System.IO.Directory.Exists(Filepath))
        //        {
        //            DirectoryInfo directoryInfo = new DirectoryInfo(Filepath);
        //            FileInfo[] fileInfos = directoryInfo.GetFiles();
        //            foreach (FileInfo fileInfo in fileInfos)
        //            {
        //                string filename = fileInfo.Name;
        //                string imagepath = Filepath + "\\" + filename;
        //                if (System.IO.File.Exists(imagepath))
        //                {
        //                    string _Imageurl = hosturl + "/Upload/product/" + productcode + "/" + filename;
        //                    Imageurl.Add(_Imageurl);
        //                }
        //            }
        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //    }
        //    return Ok(Imageurl);

        //}

        //[HttpGet("download")]
        //public async Task<IActionResult> download(string productcode)
        //{
        //    // string Imageurl = string.Empty;
        //    //string hosturl = $"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}";
        //    try
        //    {
        //        string Filepath = GetFilepath(productcode);
        //        string imagepath = Filepath + "\\" + productcode + ".png";
        //        if (System.IO.File.Exists(imagepath))
        //        {
        //            MemoryStream stream = new MemoryStream();
        //            using (FileStream fileStream = new FileStream(imagepath, FileMode.Open))
        //            {
        //                await fileStream.CopyToAsync(stream);
        //            }
        //            stream.Position = 0;
        //            return File(stream, "image/png", productcode + ".png");
        //            //Imageurl = hosturl + "/Upload/product/" + productcode + "/" + productcode + ".png";
        //        }
        //        else
        //        {
        //            return NotFound();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        return NotFound();
        //    }


        //}

        //[HttpGet("remove")]
        //public async Task<IActionResult> remove(string productcode)
        //{
        //    // string Imageurl = string.Empty;
        //    //string hosturl = $"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}";
        //    try
        //    {
        //        string Filepath = GetFilepath(productcode);
        //        string imagepath = Filepath + "\\" + productcode + ".png";
        //        if (System.IO.File.Exists(imagepath))
        //        {
        //            System.IO.File.Delete(imagepath);
        //            return Ok("pass");
        //        }
        //        else
        //        {
        //            return NotFound();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        return NotFound();
        //    }


        //}

        //[HttpGet("multiremove")]
        //public async Task<IActionResult> multiremove(string productcode)
        //{
        //    // string Imageurl = string.Empty;
        //    //string hosturl = $"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}";
        //    try
        //    {
        //        string Filepath = GetFilepath(productcode);
        //        if (System.IO.Directory.Exists(Filepath))
        //        {
        //            DirectoryInfo directoryInfo = new DirectoryInfo(Filepath);
        //            FileInfo[] fileInfos = directoryInfo.GetFiles();
        //            foreach (FileInfo fileInfo in fileInfos)
        //            {
        //                fileInfo.Delete();
        //            }
        //            return Ok("pass");
        //        }
        //        else
        //        {
        //            return NotFound();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        return NotFound();
        //    }


        //}


        //[HttpGet("dbdownload")]
        //public async Task<IActionResult> dbdownload(string productcode)
        //{

        //    try
        //    {

        //        var _productimage = await this.context.TblProductimages.FirstOrDefaultAsync(item => item.Productcode == productcode);
        //        if (_productimage != null)
        //        {
        //            return File(_productimage.Productimage, "image/png", productcode + ".png");
        //        }


        //        //string Filepath = GetFilepath(productcode);
        //        //string imagepath = Filepath + "\\" + productcode + ".png";
        //        //if (System.IO.File.Exists(imagepath))
        //        //{
        //        //    MemoryStream stream = new MemoryStream();
        //        //    using (FileStream fileStream = new FileStream(imagepath, FileMode.Open))
        //        //    {
        //        //        await fileStream.CopyToAsync(stream);
        //        //    }
        //        //    stream.Position = 0;
        //        //    return File(stream, "image/png", productcode + ".png");
        //        //    //Imageurl = hosturl + "/Upload/product/" + productcode + "/" + productcode + ".png";
        //        //}
        //        else
        //        {
        //            return NotFound();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        return NotFound();
        //    }


        //}

        [NonAction]
        private string GetFilepath(string productcode)
        {
            return this.environment.WebRootPath + "\\Upload\\product\\" + productcode;
        }



    }
}
