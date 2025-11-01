using HRMS.API.Data;
using HRMS.API.Globals;
using HRMS.API.Migrations;
using HRMS.API.Models;
using HRMS.API.Repository;
using HRMS.API.UnitOfWork;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Syncfusion.DocIO.DLS;
using System;
using System.Data;

namespace HRMS.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]

    [Authorize(Roles = "Admin,User")]
    public class TaskController : Controller
    {
        private readonly HrmDbContext _db;
        //private readonly IDemoRepository _demoRepository;
        //private readonly IGenericRepository<Demo> _genericRepository;
        private readonly IUnitOfWork _unitOfWork;

        public TaskController(IUnitOfWork unitOfWork, HrmDbContext db)
        {
            _unitOfWork = unitOfWork;
            _db = db;
        }



        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            //var rowAffect = await _db.Database.ExecuteSqlAsync($"DELETE FROM Employees");



            int EmployeeId= Convert.ToInt32(this.User.Claims.First(claim => claim.Type == "EmployeeId").Value);

            var data = await _unitOfWork.Tasks.GetAllAsync(EmployeeId);
            return Ok(data);
        }
        [HttpGet("GetTask")]
        public async Task<IActionResult> GetTask()
        {
            //var rowAffect = await _db.Database.ExecuteSqlAsync($"DELETE FROM Employees");



            int EmployeeId = Convert.ToInt32(this.User.Claims.First(claim => claim.Type == "EmployeeId").Value);

            var data = await _unitOfWork.Tasks.GetTaskByDate(EmployeeId);
            return Ok(data);
        }
        [HttpGet("GetAssignTask")]
        public async Task<IActionResult> GetAssignTask()
        {
            //var rowAffect = await _db.Database.ExecuteSqlAsync($"DELETE FROM Employees");



            int EmployeeId = Convert.ToInt32(this.User.Claims.First(claim => claim.Type == "EmployeeId").Value);

            var data = await _unitOfWork.Tasks.GetAssignTask(EmployeeId);
            return Ok(data);
        }
        [HttpGet("Get/{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var data = await _unitOfWork.Tasks.GetAsync(id);
            return Ok(data);
        }
        [HttpPost("Create")]
        public async Task<IActionResult> Create(Models.Task task)
        {
            //task.EmployeeId = GlobalVariables.Instance.UserId;

            task.EmployeeId = Convert.ToInt32(this.User.Claims.First(claim => claim.Type == "EmployeeId").Value);
            //task.Date=DateTime.Now;
            if(task.Status=="Completed")
            {
                task.CompletedDate = DateTime.Now;
            }
            var data = await _unitOfWork.Tasks.AddEntity(task);
            await _unitOfWork.CompletAsync();
            return Ok(data);
        }
        [HttpPost("Assign")]
        public async Task<IActionResult> Assign(Models.Task task)
        {

            string email = "";
            try
            {

                //Use Only FromSql for single data and for multiple data use FromSqlRaw
                var Emails = _db.UserMail.FromSqlRaw($"SELECT TOP 1 Email FROM Users  WHERE EmployeeId= {task.EmployeeId}").ToList();
                if (Emails != null)
                {
                    foreach (var em in Emails)
                    {
                        email = em.Email;
                    }
                }
            }
            catch (Exception ex) { }

            task.Status = "Incomplete";
            task.Date= DateTime.Now;    
            var data = await _unitOfWork.Tasks.AddEntity(task);
            await _unitOfWork.CompletAsync();
            Email.Instance.SendMail(email, $"You have new task from {task.AssignBy} Sir",
                 @$"Dear User, {Environment.NewLine}
                 You have new task from {task.AssignBy} Sir.  {Environment.NewLine}
                
                {task.TaskName} {Environment.NewLine}
                And deadline is {task.DeadlineDate}.  {Environment.NewLine}

                Stay Safe! {Environment.NewLine}
                Best regards, {Environment.NewLine}
                SBHL Software.{Environment.NewLine}
                This is an auto-generated e-mail and please do not reply to this.");
            return Ok(data);
        }


        [HttpGet("Done")]
        public async Task<bool> Done(int taskId)
        {
            string Name = this.User.Claims.First(claim => claim.Type == "Name").Value;

            int rowAffect=0;
            try
            {
                 rowAffect = await _db.Database.ExecuteSqlAsync($"UPDATE Task SET Status='Completed',CompletedDate={DateTime.Now} WHERE TaskId={taskId}");
            }
            catch (Exception ex) { }


            if (rowAffect<0)
            {
                return false;
            }



            string AssignBy = "";
            string TaskName = "";
            string CompletedDate = "";
            string email = "";
            try
            {

                //Use Only FromSql for single data and for multiple data use FromSqlRaw
                var AssignBys = _db.UserAssign.FromSqlRaw($"SELECT AssignBy,TaskName,CONVERT(VARCHAR(30),CompletedDate,23) AS CompletedDate FROM Task WHERE TaskId= {taskId}").ToList();
                if (AssignBys != null)
                {
                    foreach (var eb in AssignBys)
                    {
                        AssignBy = eb.AssignBy;
                        TaskName=eb.TaskName;
                        CompletedDate=eb.CompletedDate;
                    }
                }
            }
            catch (Exception ex) { }


            if(AssignBy=="SED")
            {
                email = "arifrkhan8@gmail.com";
            }

            if (AssignBy == "MD")
            {
                email = "anisrkhan@icloud.com";
            }

            if (AssignBy == "DMD")
            {
                email = "ashiq09@hotmail.com";
            }

            if (AssignBy == "Chairman")
            {
                email = "aminur.khan.sb@gmail.com";
            }


            

            Email.Instance.SendMail(email, $"Your assign task has been completed from {Name}",
                 @$"Dear Sir, {Environment.NewLine}
               Your assign task {TaskName} has been completed at {CompletedDate}.  {Environment.NewLine}
                {Environment.NewLine}

                Stay Safe! {Environment.NewLine}
                Best regards, {Environment.NewLine}
                SBHL Software.{Environment.NewLine}
                This is an auto-generated e-mail and please do not reply to this.");
            return true;
        }
        [HttpPut("Update")]
        public async Task<IActionResult> Update(Models.Task task)
        {
            if (task.Status == "Completed")
            {
                task.CompletedDate = DateTime.Now;
            }
            var data = await _unitOfWork.Tasks.UpdateEntity(task);
            await _unitOfWork.CompletAsync();
            return Ok(data);
        }
        [HttpDelete("Delete")]
        public async Task<IActionResult> Delete(int id)
        {
            var data = await _unitOfWork.Tasks.DeleteEntity(id);
            await _unitOfWork.CompletAsync();
            return Ok(data);
        }




    }
}
