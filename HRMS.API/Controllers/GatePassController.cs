using HRMS.API.Data;
using HRMS.API.Globals;
using HRMS.API.Models;
using HRMS.API.Repository;
using HRMS.API.UnitOfWork;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Reporting.Map.WebForms.BingMaps;
using System;
using System.Data;

namespace HRMS.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]

    //[Authorize(Roles = "Admin,User")]
    public class GatePassController : Controller
    {
        private readonly HrmDbContext _db;
        //private readonly IDemoRepository _demoRepository;
        //private readonly IGenericRepository<Demo> _genericRepository;
        private readonly IUnitOfWork _unitOfWork;

        public GatePassController(IUnitOfWork unitOfWork,HrmDbContext db)
        {
            _unitOfWork = unitOfWork;
            _db = db;
        }



        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            //var rowAffect = await _db.Database.ExecuteSqlAsync($"DELETE FROM Employees");

            int ProjectId=0;

            int EmployeeId = Convert.ToInt32(this.User.Claims.First(claim => claim.Type == "EmployeeId").Value);
            var Data = _db.EmployeeID.FromSqlRaw($"SELECT TOP 1 ProjectId FROM PMSEmpInfo WHERE EmployeeId={EmployeeId}").ToList();

            foreach(var Project in Data)
            {
                ProjectId = Project.EmployeeId;
            }
            //var data = await _unitOfWork.GatePass.GetAllAsync();

            List<GatePassDto> data=null;
           try
            {
                 data = await _db.GatePassDto.FromSqlRaw(@$"
                            SELECT P.Name,GP.* FROM GatePass GP
                            INNER JOIN ProjectSBHL P ON P.ProjectId=GP.SendBy
                            WHERE Status='Pending' AND SendTo={ProjectId}").ToListAsync();
            }
            catch (Exception ex) { }
            return Ok(data);
        }

        [HttpGet("History")]
        public async Task<IActionResult> History()
        {
            //var rowAffect = await _db.Database.ExecuteSqlAsync($"DELETE FROM Employees");

            int ProjectId = 0;

            int EmployeeId = Convert.ToInt32(this.User.Claims.First(claim => claim.Type == "EmployeeId").Value);
            var Data = _db.EmployeeID.FromSqlRaw($"SELECT TOP 1 ProjectId FROM PMSEmpInfo WHERE EmployeeId={EmployeeId}").ToList();

            foreach (var Project in Data)
            {
                ProjectId = Project.EmployeeId;
            }
            //var data = await _unitOfWork.GatePass.GetAllAsync();

            List<GatePassDto> data = null;

            try
            {
                //data = await _db.GatePass.Where(s => s.SendBy == ProjectId || s.SendTo == ProjectId).ToListAsync();

                data = await _db.GatePassDto.FromSqlRaw(@$"
                            SELECT P.Name,GP.* FROM GatePass GP
                            INNER JOIN ProjectSBHL P ON P.ProjectId=GP.SendBy
                            WHERE SendBy={ProjectId} OR SendTo={ProjectId}").ToListAsync();

            }
            catch(Exception ex) { }
            
            return Ok(data);
        }


        [HttpGet("Get/{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var data = await _unitOfWork.GatePass.GetAsync(id);
            return Ok(data);
        }


        [HttpGet("IsExist")]
        public async Task<bool> IsExist(int GatePassNo)
        {
            var data = await _db.GatePass.Where(s => s.GatePassNo == GatePassNo).ToListAsync();
            if (data.Count != 0)
            {
                return true;
            }
            else
                return false;
        }


        [HttpGet("IsStock")]
        public async Task<bool> IsStock([FromQuery] Models.GatePassDto entity)
        {

            int EmployeeId = Convert.ToInt32(this.User.Claims.First(claim => claim.Type == "EmployeeId").Value);

            //var Data = _db.EmployeeDto.FromSqlRaw($"SELECT EmployeeId,EmpName FROM PMSEmpInfo WHERE DepartmentId={3} AND IsActive=0 AND ProjectId=3").ToList();



            try
            {

                //Use Only FromSql for single data and for multiple data use FromSqlRaw
                var Projects = _db.GetProject.FromSqlRaw($"SELECT ProjectId FROM PMSEmpInfo WHERE EmployeeId= {EmployeeId}").ToList();
                if (Projects != null)
                {
                    foreach (var project in Projects)
                    {
                        entity.SendBy = Convert.ToInt32(project.ProjectId);
                    }
                }


            }
            catch (Exception ex) { }
            var sentByExistitems = await _db.Item.Where(s => s.ProjectId == entity.SendBy).ToListAsync();

            Item sentByItems = new Item();

            foreach (var item in sentByExistitems)
            {
                sentByItems.ShutterPcs = item.ShutterPcs - entity.ShutterPcs;
                sentByItems.ShutterSft = item.ShutterSft - entity.ShutterSft;
                sentByItems.ProopsUpPcs = item.ProopsUpPcs - entity.ProopsUpPcs;
                sentByItems.ProopslowPcs = item.ProopslowPcs - entity.ProopslowPcs;
                sentByItems.ScaffSidePcs = item.ScaffSidePcs - entity.ScaffSidePcs;
                sentByItems.ScaffBumperPcs = item.ScaffBumperPcs - entity.ScaffBumperPcs;
                sentByItems.ScaffKachiPcs = item.ScaffKachiPcs - entity.ScaffKachiPcs;
                sentByItems.MSPipePcs = item.MSPipePcs - entity.MSPipePcs;
                sentByItems.MSPipeFt = item.MSPipeFt - entity.MSPipeFt;
                sentByItems.BigHopper = item.BigHopper - entity.BigHopper;
                sentByItems.Mixer = item.Mixer - entity.Mixer;
                sentByItems.Bending = item.Bending - entity.Bending;
                sentByItems.Welding = item.Welding - entity.Welding;
                sentByItems.Vibrator = item.Vibrator - entity.Vibrator;
                sentByItems.RoofHoist = item.RoofHoist - entity.RoofHoist;
                sentByItems.HollowBox = item.HollowBox - entity.HollowBox;
                sentByItems.IJoist = item.IJoist - entity.IJoist;
                sentByItems.CISHeet = item.CISHeet - entity.CISHeet;
                sentByItems.MSPlainSheet = item.MSPlainSheet - entity.MSPlainSheet;
                sentByItems.SafetyTask = item.SafetyTask - entity.SafetyTask;
                sentByItems.SafetyNet = item.SafetyNet - entity.SafetyNet;
                sentByItems.Wood = item.Wood - entity.Wood;
                sentByItems.StandFan = item.StandFan - entity.StandFan;
                sentByItems.CeilingFan = item.CeilingFan - entity.CeilingFan;
                sentByItems.FloodLight = item.FloodLight - entity.FlooDlight;
                sentByItems.Grill = item.Grill - entity.Grill;
                sentByItems.MsPipeOtherSize = item.MsPipeOtherSize - entity.MsPipeOtherSize;
                sentByItems.MsFencing = item.MsFencing - entity.MsFencing;
                sentByItems.Generator = item.Generator - entity.Generator;
                sentByItems.MudPump = item.MudPump - entity.MudPump;
                sentByItems.Pump = item.Pump - entity.Pump;
                sentByItems.SuableClam = item.SuableClam - entity.SuableClam;
                sentByItems.JoinPin = item.JoinPin - entity.JoinPin;
                sentByItems.WaterTank = item.WaterTank - entity.WaterTank;

            }
            if (sentByItems.ShutterPcs < 0)
            {
                return false;
            }
            if (sentByItems.ShutterSft<0)
            {
                return false;
            }
            if (sentByItems.ProopsUpPcs < 0)
            {
                return false;
            }
            if (sentByItems.ProopslowPcs < 0)
            {
                return false;

            }
            if (sentByItems.ScaffBumperPcs < 0)
            {
                return false;
            }
            if (sentByItems.ScaffSidePcs < 0)
            {
                return false;
            }
            if (sentByItems.ScaffKachiPcs < 0)
            {
                return false;
            }
            if (sentByItems.MSPipePcs < 0)
            {
                return false;
            }
            if (sentByItems.MSPipeFt < 0)
            {
                return false;
            }
            if (sentByItems.BigHopper < 0)
            {
                return false;
            }
            if (sentByItems.Mixer < 0)
            {
                return false;
            }
            if (sentByItems.Bending < 0)
            {
                return false;
            }
            if (sentByItems.Welding < 0)
            {
                return false;
            }
            if (sentByItems.Vibrator < 0)
            {
                return false;
            }
            if (sentByItems.RoofHoist < 0)
            {
                return false;
            }
            if (sentByItems.HollowBox < 0)
            {
                return false;
            }
            if (sentByItems.IJoist < 0)
            {
                return false;
            }
            if (sentByItems.CISHeet < 0)
            {
                return false;
            }
            if (sentByItems.MSPlainSheet < 0)
            {
                return false;

            }
            if (sentByItems.SafetyTask < 0)
            {
                return false;
            }
            if (sentByItems.SafetyNet < 0)
            {
                return false;
            }
            if (sentByItems.Wood < 0)
            {
                return false;
            }
            if (sentByItems.StandFan < 0)
            {
                return false;
            }
            if (sentByItems.CeilingFan < 0)
            {
                return false;
            }
            if (sentByItems.FloodLight < 0)
            {
                return false;
            }
            if (sentByItems.Grill < 0)
            {
                return false;
            }
            if (sentByItems.MsPipeOtherSize < 0)
            {
                return false;
            }
            if (sentByItems.MsFencing < 0)
            {
                return false;
            }
            if (sentByItems.Generator < 0)
            {
                return false;
            }
            if (sentByItems.MudPump < 0)
            {
                return false;
            }
            if (sentByItems.Pump < 0)
            {
                return false;
            }
            if (sentByItems.SuableClam < 0)
            {
                return false;
            }
            if (sentByItems.JoinPin < 0)
            {
                return false;
            }
            if (sentByItems.WaterTank < 0)
            {
                return false;
            }
            else
            {
                return true;
            }



        }


        [HttpPost("Create")]
        public async Task<IActionResult> Create(Models.GatePass entity)
        {
            //task.EmployeeId = GlobalVariables.Instance.UserId;

            int EmployeeId = Convert.ToInt32(this.User.Claims.First(claim => claim.Type == "EmployeeId").Value);
            
            //var Data = _db.EmployeeDto.FromSqlRaw($"SELECT EmployeeId,EmpName FROM PMSEmpInfo WHERE DepartmentId={3} AND IsActive=0 AND ProjectId=3").ToList();

          

            try
            {

                //Use Only FromSql for single data and for multiple data use FromSqlRaw
                var Projects = _db.GetProject.FromSqlRaw($"SELECT ProjectId FROM PMSEmpInfo WHERE EmployeeId= {EmployeeId}").ToList();
                if (Projects != null)
                {
                    foreach (var project in Projects)
                    {
                        entity.SendBy = Convert.ToInt32(project.ProjectId);
                    }
                }


            }
            catch (Exception ex) { }


           

            entity.Status = "Pending";
            entity.SentDate = DateTime.Now;

            var data = await _unitOfWork.GatePass.AddEntity(entity);
            await _unitOfWork.CompletAsync();


            string email = "";
            try
            {
               
                //Use Only FromSql for single data and for multiple data use FromSqlRaw
                var Emails = _db.UserMail.FromSqlRaw($"SELECT TOP 1 Email FROM Users U INNER JOIN  PMSEmpInfo E ON E.EmployeeId=U.EmployeeId WHERE E.ProjectId= {entity.SendTo}").ToList();
                if (Emails != null)
                {
                    foreach (var em in Emails)
                    {
                        email = em.Email;
                    }
                }
            }
            catch (Exception ex) { }

            if (entity.SendTo == 3)
            {
                email = "nayamothullahsamrat@gmail.com";
            }


            Email.Instance.SendMail(
                email,
                $"Upcoming Materials (G.P No-{entity.GatePassNo})",
                @$"Dear Engineer, {Environment.NewLine}
                Your material requisition request has been approved. {Environment.NewLine}
                Please check your materials in app and accept it after physical inspection.{Environment.NewLine}
                Stay Safe! {Environment.NewLine}
                Best regards, {Environment.NewLine}
                SBHL Software.{Environment.NewLine}
                This is an auto-generated e-mail and please do not reply to this.");
            return Ok(data);
        }


        [HttpPut("Accept")]
        public async Task<IActionResult> Accept(Models.GatePass entity)
        {
            var rowAffect = await _db.Database.ExecuteSqlAsync($"UPDATE GatePass SET Status='Accepted' WHERE GatePassNo={entity.GatePassNo}");

            //Need Project Based
            //var Existitems = await _unitOfWork.Stock.GetAllAsync();

            var sentByExistitems = await _db.Item.Where(s=>s.ProjectId==entity.SendBy).ToListAsync();

            Item sentByItems = new Item();

            foreach (var item in sentByExistitems)
            {
                sentByItems.ShutterPcs=item.ShutterPcs-entity.ShutterPcs;
                sentByItems.ShutterSft = item.ShutterSft - entity.ShutterSft;
                sentByItems.ProopsUpPcs = item.ProopsUpPcs - entity.ProopsUpPcs;
                sentByItems.ProopslowPcs = item.ProopslowPcs - entity.ProopslowPcs;
                sentByItems.ScaffSidePcs = item.ScaffSidePcs - entity.ScaffSidePcs;
                sentByItems.ScaffBumperPcs = item.ScaffBumperPcs - entity.ScaffBumperPcs;
                sentByItems.ScaffKachiPcs = item.ScaffKachiPcs - entity.ScaffKachiPcs;
                sentByItems.MSPipePcs = item.MSPipePcs - entity.MSPipePcs;
                sentByItems.MSPipeFt = item.MSPipeFt - entity.MSPipeFt;
                sentByItems.BigHopper = item.BigHopper - entity.BigHopper;
                sentByItems.Mixer = item.Mixer - entity.Mixer;
                sentByItems.Bending = item.Bending - entity.Bending;
                sentByItems.Welding = item.Welding - entity.Welding;
                sentByItems.Vibrator = item.Vibrator - entity.Vibrator;
                sentByItems.RoofHoist = item.RoofHoist - entity.RoofHoist;
                sentByItems.HollowBox = item.HollowBox - entity.HollowBox;
                sentByItems.IJoist = item.IJoist - entity.IJoist;
                sentByItems.CISHeet = item.CISHeet - entity.CISHeet;
                sentByItems.MSPlainSheet = item.MSPlainSheet - entity.MSPlainSheet;
                sentByItems.SafetyTask = item.SafetyTask - entity.SafetyTask;
                sentByItems.SafetyNet = item.SafetyNet - entity.SafetyNet;
                sentByItems.Wood = item.Wood - entity.Wood;
                sentByItems.StandFan = item.StandFan - entity.StandFan;
                sentByItems.CeilingFan = item.CeilingFan - entity.CeilingFan;
                sentByItems.FloodLight = item.FloodLight - entity.FlooDlight;
                sentByItems.Grill = item.Grill - entity.Grill;
                sentByItems.MsPipeOtherSize = item.MsPipeOtherSize - entity.MsPipeOtherSize;
                sentByItems.MsFencing = item.MsFencing - entity.MsFencing;
                sentByItems.Generator = item.Generator - entity.Generator;
                sentByItems.MudPump = item.MudPump - entity.MudPump;
                sentByItems.Pump = item.Pump - entity.Pump;
                sentByItems.SuableClam = item.SuableClam - entity.SuableClam;
                sentByItems.JoinPin = item.JoinPin - entity.JoinPin;
                sentByItems.WaterTank = item.WaterTank - entity.WaterTank;

            }

            var UpdateSentByStock = await _db.Database.ExecuteSqlAsync(@$"
            UPDATE Item SET ShutterPcs={sentByItems.ShutterPcs},ShutterSft={sentByItems.ShutterSft},
            ProopsUpPcs={sentByItems.ProopsUpPcs},ProopslowPcs={sentByItems.ProopslowPcs},
            ScaffSidePcs={sentByItems.ScaffSidePcs},ScaffBumperPcs={sentByItems.ScaffBumperPcs},
            ScaffKachiPcs={sentByItems.ScaffKachiPcs},MSPipePcs={sentByItems.MSPipePcs},
            MSPipeFt={sentByItems.MSPipeFt},BigHopper={sentByItems.BigHopper},
            Mixer={sentByItems.Mixer},Bending={sentByItems.Bending},
            Welding={sentByItems.Welding},Vibrator={sentByItems.Vibrator},
            RoofHoist={sentByItems.RoofHoist},HollowBox={sentByItems.HollowBox},
            IJoist={sentByItems.IJoist},CISHeet={sentByItems.CISHeet},
            MSPlainSheet={sentByItems.MSPlainSheet},SafetyTask={sentByItems.SafetyTask},
            SafetyNet={sentByItems.SafetyNet},Wood={sentByItems.Wood},
            StandFan={sentByItems.StandFan},CeilingFan={sentByItems.CeilingFan},
            FloodLight={sentByItems.FloodLight},Grill={sentByItems.Grill},
            MsPipeOtherSize={sentByItems.MsPipeOtherSize},MsFencing={sentByItems.MsFencing},
            Generator={sentByItems.Generator},MudPump={sentByItems.Generator},
            Pump={sentByItems.Pump},SuableClam={sentByItems.SuableClam},
            WaterTank={sentByItems.WaterTank},JoinPin={sentByItems.JoinPin}
            WHERE ProjectId={entity.SendBy}");




            var sentToExistitems = await _db.Item.Where(s => s.ProjectId == entity.SendTo).ToListAsync();
            Item sentToItems = new Item();


            foreach (var item in sentToExistitems)
            {

                sentToItems.ShutterPcs = item.ShutterPcs + entity.ShutterPcs;
                sentToItems.ShutterSft = item.ShutterSft + entity.ShutterSft;
                sentToItems.ProopsUpPcs = item.ProopsUpPcs + entity.ProopsUpPcs;
                sentToItems.ProopslowPcs = item.ProopslowPcs + entity.ProopslowPcs;
                sentToItems.ScaffSidePcs = item.ScaffSidePcs + entity.ScaffSidePcs;
                sentToItems.ScaffBumperPcs = item.ScaffBumperPcs + entity.ScaffBumperPcs;
                sentToItems.ScaffKachiPcs = item.ScaffKachiPcs + entity.ScaffKachiPcs;
                sentToItems.MSPipePcs = item.MSPipePcs + entity.MSPipePcs;
                sentToItems.MSPipeFt = item.MSPipeFt + entity.MSPipeFt;
                sentToItems.BigHopper = item.BigHopper + entity.BigHopper;
                sentToItems.Mixer = item.Mixer + entity.Mixer;
                sentToItems.Bending = item.Bending + entity.Bending;
                sentToItems.Welding = item.Welding + entity.Welding;
                sentToItems.Vibrator = item.Vibrator + entity.Vibrator;
                sentToItems.RoofHoist = item.RoofHoist + entity.RoofHoist;
                sentToItems.HollowBox = item.HollowBox + entity.HollowBox;
                sentToItems.IJoist = item.IJoist + entity.IJoist;
                sentToItems.CISHeet = item.CISHeet + entity.CISHeet;
                sentToItems.MSPlainSheet = item.MSPlainSheet + entity.MSPlainSheet;
                sentToItems.SafetyTask = item.SafetyTask + entity.SafetyTask;
                sentToItems.SafetyNet = item.SafetyNet + entity.SafetyNet;
                sentToItems.Wood = item.Wood + entity.Wood;
                sentToItems.StandFan = item.StandFan + entity.StandFan;
                sentToItems.CeilingFan = item.CeilingFan + entity.CeilingFan;
                sentToItems.FloodLight = item.FloodLight + entity.FlooDlight;
                sentToItems.Grill = item.Grill + entity.Grill;
                sentToItems.MsPipeOtherSize = item.MsPipeOtherSize + entity.MsPipeOtherSize;
                sentToItems.MsFencing = item.MsFencing + entity.MsFencing;
                sentToItems.Generator = item.Generator + entity.Generator;
                sentToItems.MudPump = item.MudPump + entity.MudPump;
                sentToItems.Pump = item.Pump + entity.Pump;
                sentToItems.SuableClam = item.SuableClam + entity.SuableClam;
                sentToItems.JoinPin = item.JoinPin + entity.JoinPin;
                sentToItems.WaterTank = item.WaterTank + entity.WaterTank;

            }
            var UpdateSentToStock = await _db.Database.ExecuteSqlAsync(@$"
            UPDATE Item SET ShutterPcs={sentToItems.ShutterPcs},ShutterSft={sentToItems.ShutterSft},
            ProopsUpPcs={sentToItems.ProopsUpPcs},ProopslowPcs={sentToItems.ProopslowPcs},
            ScaffSidePcs={sentToItems.ScaffSidePcs},ScaffBumperPcs={sentToItems.ScaffBumperPcs},
            ScaffKachiPcs={sentToItems.ScaffKachiPcs},MSPipePcs={sentToItems.MSPipePcs},
            MSPipeFt={sentToItems.MSPipeFt},BigHopper={sentToItems.BigHopper},
            Mixer={sentToItems.Mixer},Bending={sentToItems.Bending},
            Welding={sentToItems.Welding},Vibrator={sentToItems.Vibrator},
            RoofHoist={sentToItems.RoofHoist},HollowBox={sentToItems.HollowBox},
            IJoist={sentToItems.IJoist},CISHeet={sentToItems.CISHeet},
            MSPlainSheet={sentToItems.MSPlainSheet},SafetyTask={sentToItems.SafetyTask},
            SafetyNet={sentToItems.SafetyNet},Wood={sentToItems.Wood},
            StandFan={sentToItems.StandFan},CeilingFan={sentToItems.CeilingFan},
            FloodLight={sentToItems.FloodLight},Grill={sentToItems.Grill},
            MsPipeOtherSize={sentToItems.MsPipeOtherSize},MsFencing={sentToItems.MsFencing},
            Generator={sentByItems.Generator},MudPump={sentToItems.Generator},
            Pump={sentToItems.Pump},SuableClam={sentToItems.SuableClam},
            WaterTank={sentToItems.WaterTank},JoinPin={sentToItems.JoinPin}
            WHERE ProjectId={entity.SendTo}");



            string email = "";
            try
            {

                //Use Only FromSql for single data and for multiple data use FromSqlRaw
                var Emails = _db.UserMail.FromSqlRaw($"SELECT TOP 1 Email FROM Users U INNER JOIN  PMSEmpInfo E ON E.EmployeeId=U.EmployeeId WHERE E.ProjectId= {entity.SendBy}").ToList();
                if (Emails != null)
                {
                    foreach (var em in Emails)
                    {
                        email = em.Email;
                    }
                }
            }
            catch (Exception ex) { }

            if(entity.SendBy==3)
            {
                email = "nayamothullahsamrat@gmail.com";
            }


            Email.Instance.SendMail(
                email,
                $"Confirmation of materials received (G.P No-{entity.GatePassNo})",
                @$"Dear Engineer, {Environment.NewLine}
                Your Material (send item) has been received.
                Stay Safe! {Environment.NewLine}
                Best regards, {Environment.NewLine}
                SBHL Software.{Environment.NewLine}
                This is an auto-generated e-mail and please do not reply to this.");

            return Ok();
        }

        [HttpPut("Reject")]
        public async Task<IActionResult> Reject(Models.GatePass entity)
        {
            var rowAffect = await _db.Database.ExecuteSqlAsync($"UPDATE GatePass SET Status='Rejected' WHERE GatePassNo={entity.GatePassNo}");


            string email = "";
            try
            {

                //Use Only FromSql for single data and for multiple data use FromSqlRaw
                var Emails = _db.UserMail.FromSqlRaw($"SELECT TOP 1 Email FROM Users U INNER JOIN  PMSEmpInfo E ON E.EmployeeId=U.EmployeeId WHERE E.ProjectId= {entity.SendBy}").ToList();
                if (Emails != null)
                {
                    foreach (var em in Emails)
                    {
                        email = em.Email;
                    }
                }
            }
            catch (Exception ex) { }

            if (entity.SendBy == 3)
            {
                email = "nayamothullahsamrat@gmail.com";
            }


            Email.Instance.SendMail(
                email,
                $" Rejected Materials (G.P No-{entity.GatePassNo})",
                @$"Dear Engineer, {Environment.NewLine}
                Your Material (send item) has been rejected. {Environment.NewLine}
                Please check your returned materials in app and accept it after physical inspection.
                Stay Safe! {Environment.NewLine}
                Best regards, {Environment.NewLine}
                SBHL Software.{Environment.NewLine}
                This is an auto-generated e-mail and please do not reply to this.");
            return Ok();
        }


        [HttpPut("Update")]
        public async Task<IActionResult> Update(Models.GatePass entity)
        {

            var data = await _unitOfWork.GatePass.UpdateEntity(entity);
            await _unitOfWork.CompletAsync();
            return Ok(data);
        }
        [HttpDelete("Delete")]
        public async Task<IActionResult> Delete(int id)
        {
            var data = await _unitOfWork.GatePass.DeleteEntity(id);
            await _unitOfWork.CompletAsync();
            return Ok(data);
        }




    }
}
