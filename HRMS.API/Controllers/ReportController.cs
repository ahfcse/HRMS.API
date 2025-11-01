using HRMS.API.Data;
using HRMS.API.Migrations;
using HRMS.API.Models;
using HRMS.API.Repository;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Reporting.NETCore;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Net.Mime;

namespace HRMS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("CorsPolicy")]
    //[EnableCors(origins: "*", headers: "", methods: "*")]
    public class ReportController : ControllerBase
    {
        private IReportRepository _report;
        private readonly HrmDbContext _dbContext;
        private readonly IWebHostEnvironment _hostingEnvironment;

        public ReportController(IReportRepository report, HrmDbContext dbContext, IWebHostEnvironment hostingEnvironment)
        {
            _report = report;
            _dbContext = dbContext;
            _hostingEnvironment = hostingEnvironment;
        }

        //Multiple Datas source with perameter

        
        [HttpGet("AttendanceDetailsReport")]
        public async Task<ActionResult<byte[]>> AttendanceDetailsReport(int EmployeeId,string FromDate,string ToDate)
        {

            try
            {
                var rowAffect = await _dbContext.Database.ExecuteSqlAsync($"EXEC spAttendaceProcess {EmployeeId},{FromDate},{ToDate}");

            }
            catch(Exception ex) { }

            //For Local
            //string reportPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory + "Reports", "rptAttendanceDetails.rdlc");

            //FOR IIS

            // Get the content root path of the application
            string contentRootPath = _hostingEnvironment.ContentRootPath;

            // Combine the content root path with the relative path to your RDLC report file
            string reportFileName = "rptAttendanceDetails.rdlc";
            string reportPath = Path.Combine(contentRootPath, "Reports", reportFileName);


            var reportData = _dbContext.spRptAttendanceDetails.FromSqlRaw("EXEC spRptAttendanceDetails @p0, @p1,@p2", EmployeeId, FromDate, ToDate).ToList();
            var reportData2 = _dbContext.spRPTCompanyInformation.FromSqlRaw("EXEC spRPTCompanyInformation").ToList();
            // Create a ReportDataSource for the dataset in the RDLC report
            var reportDataSource = new ReportDataSource("DataSet1", reportData);
            var reportDataSource2 = new ReportDataSource("DataSet2", reportData2);
            var localReport = new LocalReport { ReportPath = reportPath };
            // Add the ReportDataSource to the LocalReport
            localReport.DataSources.Add(reportDataSource);
            localReport.DataSources.Add(reportDataSource2);
            // Set report parameters
            var parameters = new List<ReportParameter>
        {
                  new ReportParameter("EmployeeId", EmployeeId.ToString()),
                  new ReportParameter("FromDate", FromDate),
                  new ReportParameter("ToDate", ToDate)
            // Add more parameters as needed
        };
            localReport.SetParameters(parameters);

            byte[] result = localReport.Render("PDF", null);

            if (result == null || result.Length == 0)
            {
                // Handle the case where the result is null or empty
                return BadRequest("Invalid report content");
            }
            // Return the report as a file with content type 'application/pdf'
            return File(result, "application/pdf", "Report.pdf");
        }


        [HttpGet("IndividualAttendanceSummary")]
        public async Task<ActionResult<byte[]>> IndividualAttendanceSummary(int EmployeeId, string FromDate, string ToDate)
        {

            try
            {
                var rowAffect = await _dbContext.Database.ExecuteSqlAsync($"EXEC spAttendaceProcess {EmployeeId},{FromDate},{ToDate}");

            }
            catch (Exception ex) { }


            //For Local
            //string reportPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory + "Reports", "rptAttendanceDetails.rdlc");

            //FOR IIS

            // Get the content root path of the application
            string contentRootPath = _hostingEnvironment.ContentRootPath;

            // Combine the content root path with the relative path to your RDLC report file
            string reportFileName = "rptAttendanceSummery.rdlc";
            string reportPath = Path.Combine(contentRootPath, "Reports", reportFileName);


            var reportData = _dbContext.spRptAttendanceSummery.FromSqlRaw("EXEC spRptAttendanceSummery @p0, @p1,@p2", EmployeeId, FromDate, ToDate).ToList();
            var reportData2 = _dbContext.spRPTCompanyInformation.FromSqlRaw("EXEC spRPTCompanyInformation").ToList();
            // Create a ReportDataSource for the dataset in the RDLC report
            var reportDataSource = new ReportDataSource("DataSet1", reportData);
            var reportDataSource2 = new ReportDataSource("DataSet2", reportData2);
            var localReport = new LocalReport { ReportPath = reportPath };
            // Add the ReportDataSource to the LocalReport
            localReport.DataSources.Add(reportDataSource);
            localReport.DataSources.Add(reportDataSource2);
            // Set report parameters
            var parameters = new List<ReportParameter>
        {
                  new ReportParameter("EmployeeId", EmployeeId.ToString()),
                  new ReportParameter("FromDate", FromDate),
                  new ReportParameter("ToDate", ToDate)
            // Add more parameters as needed
        };
            localReport.SetParameters(parameters);

            byte[] result = localReport.Render("PDF", null);

            if (result == null || result.Length == 0)
            {
                // Handle the case where the result is null or empty
                return BadRequest("Invalid report content");
            }
            // Return the report as a file with content type 'application/pdf'
            return File(result, "application/pdf", "Report.pdf");
        }


        [HttpGet("LateSummary")]
        public async Task<ActionResult<byte[]>> LateSummary(string FromDate, string ToDate)
        {
            var Employees = _dbContext.EmployeesRS.FromSqlRaw("SELECT EmployeeId FROM PMSEmpInfo WHERE ProjectId=3 AND IsActive=0").ToList();




            foreach (var employee in Employees)
            {

                try
                {
                    var rowAffect = await _dbContext.Database.ExecuteSqlAsync($"EXEC spAttendaceProcess {employee.EmployeeId},{FromDate},{ToDate}");

                }
                catch (Exception ex) { }
            }







            //var rowAffect = await _dbContext.Database.ExecuteSqlAsync($"EXEC spAttendaceProcessAll {FromDate},{ToDate}");


            //For Local
            //string reportPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory + "Reports", "rptAttendanceDetails.rdlc");

            //FOR IIS

            // Get the content root path of the application
            string contentRootPath = _hostingEnvironment.ContentRootPath;

            // Combine the content root path with the relative path to your RDLC report file
            string reportFileName = "rptLateSummery.rdlc";
            string reportPath = Path.Combine(contentRootPath, "Reports", reportFileName);


            var reportData = _dbContext.spLateSummery.FromSqlRaw("EXEC spLateSummery @p0,@p1", FromDate, ToDate).ToList();
            var reportData2 = _dbContext.spRPTCompanyInformation.FromSqlRaw("EXEC spRPTCompanyInformation").ToList();
            // Create a ReportDataSource for the dataset in the RDLC report
            var reportDataSource = new ReportDataSource("DataSet1", reportData);
            var reportDataSource2 = new ReportDataSource("DataSet2", reportData2);
            var localReport = new LocalReport { ReportPath = reportPath };
            // Add the ReportDataSource to the LocalReport
            localReport.DataSources.Add(reportDataSource);
            localReport.DataSources.Add(reportDataSource2);
            // Set report parameters
            var parameters = new List<ReportParameter>
        {
                  new ReportParameter("FromDate", FromDate),
                  new ReportParameter("ToDate", ToDate)
            // Add more parameters as needed
        };
            localReport.SetParameters(parameters);

            byte[] result = localReport.Render("PDF", null);

            if (result == null || result.Length == 0)
            {
                // Handle the case where the result is null or empty
                return BadRequest("Invalid report content");
            }
            // Return the report as a file with content type 'application/pdf'
            return File(result, "application/pdf", "Report.pdf");
        }

        [HttpGet("AttendanceSummary")]
        public async Task<ActionResult<byte[]>> AttendanceSummary(string FromDate, string ToDate)
        {

            var Employees = _dbContext.EmployeesRS.FromSqlRaw("SELECT EmployeeId FROM PMSEmpInfo WHERE ProjectId=3 AND IsActive=0").ToList();




            foreach (var employee in Employees)
            {

                try
                {
                    var rowAffect = await _dbContext.Database.ExecuteSqlAsync($"EXEC spAttendaceProcess {employee.EmployeeId},{FromDate},{ToDate}");

                }
                catch (Exception ex) { }
            }

          
            string contentRootPath = _hostingEnvironment.ContentRootPath;

            // Combine the content root path with the relative path to your RDLC report file
            string reportFileName = "rptAttendenceSummeryAll.rdlc";
            string reportPath = Path.Combine(contentRootPath, "Reports", reportFileName);


            var reportData = _dbContext.spAttendenceSummeryAll.FromSqlRaw("EXEC spAttendenceSummeryAll @p0,@p1", FromDate, ToDate).ToList();
            var reportData2 = _dbContext.spRPTCompanyInformation.FromSqlRaw("EXEC spRPTCompanyInformation").ToList();
            // Create a ReportDataSource for the dataset in the RDLC report
            var reportDataSource = new ReportDataSource("DataSet1", reportData);
            var reportDataSource2 = new ReportDataSource("DataSet2", reportData2);
            var localReport = new LocalReport { ReportPath = reportPath };
            // Add the ReportDataSource to the LocalReport
            localReport.DataSources.Add(reportDataSource);
            localReport.DataSources.Add(reportDataSource2);
            // Set report parameters
            var parameters = new List<ReportParameter>
        {
                  new ReportParameter("FromDate", FromDate),
                  new ReportParameter("ToDate", ToDate)
            // Add more parameters as needed
        };
            localReport.SetParameters(parameters);

            byte[] result = localReport.Render("PDF", null);

            if (result == null || result.Length == 0)
            {
                // Handle the case where the result is null or empty
                return BadRequest("Invalid report content");
            }
            // Return the report as a file with content type 'application/pdf'
            return File(result, "application/pdf", "Report.pdf");
        }



        [HttpGet("PartialMrr")]
        public async Task<ActionResult<byte[]>> PartialMrr()
        {

            


            string contentRootPath = _hostingEnvironment.ContentRootPath;

            // Combine the content root path with the relative path to your RDLC report file
            string reportFileName = "rptPartialMrrReport.rdlc";
            string reportPath = Path.Combine(contentRootPath, "Reports", reportFileName);


            var reportData = _dbContext.spRptPartialMrrReceived.FromSqlRaw("EXEC spRptPartialMrrReceived").ToList();
            var reportData2 = _dbContext.spRPTCompanyInformation.FromSqlRaw("EXEC spRPTCompanyInformation").ToList();
            // Create a ReportDataSource for the dataset in the RDLC report
            var reportDataSource = new ReportDataSource("DataSet1", reportData);
            var reportDataSource2 = new ReportDataSource("DataSet2", reportData2);
            var localReport = new LocalReport { ReportPath = reportPath };
            // Add the ReportDataSource to the LocalReport
            localReport.DataSources.Add(reportDataSource);
            localReport.DataSources.Add(reportDataSource2);
            // Set report parameters
      

            byte[] result = localReport.Render("PDF", null);

            if (result == null || result.Length == 0)
            {
                // Handle the case where the result is null or empty
                return BadRequest("Invalid report content");
            }
            // Return the report as a file with content type 'application/pdf'
            return File(result, "application/pdf", "Report.pdf");
        }
        [HttpGet("MrrNotFound")]
        public async Task<ActionResult<byte[]>> MrrNotFound()
        {




            string contentRootPath = _hostingEnvironment.ContentRootPath;

            // Combine the content root path with the relative path to your RDLC report file
            string reportFileName = "RptMrrNotFound.rdlc";
            string reportPath = Path.Combine(contentRootPath, "Reports", reportFileName);


            var reportData = _dbContext.spRptMrrNotFound.FromSqlRaw("EXEC spRptMrrNotFound").ToList();
            var reportData2 = _dbContext.spRPTCompanyInformation.FromSqlRaw("EXEC spRPTCompanyInformation").ToList();
            // Create a ReportDataSource for the dataset in the RDLC report
            var reportDataSource = new ReportDataSource("DataSet1", reportData);
            var reportDataSource2 = new ReportDataSource("DataSet2", reportData2);
            var localReport = new LocalReport { ReportPath = reportPath };
            // Add the ReportDataSource to the LocalReport
            localReport.DataSources.Add(reportDataSource);
            localReport.DataSources.Add(reportDataSource2);
            // Set report parameters


            byte[] result = localReport.Render("PDF", null);

            if (result == null || result.Length == 0)
            {
                // Handle the case where the result is null or empty
                return BadRequest("Invalid report content");
            }
            // Return the report as a file with content type 'application/pdf'
            return File(result, "application/pdf", "Report.pdf");
        }


        [HttpGet("TaskDetailsReport")]
        public async Task<IActionResult> TaskDetailsReport(int EmployeeId, string FromDate, string ToDate)
        {
            //string reportPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory + "Reports", "rptTaskDetails.rdlc");

            // Get the content root path of the application
            string contentRootPath = _hostingEnvironment.ContentRootPath;

            // Combine the content root path with the relative path to your RDLC report file
            string reportFileName = "rptTaskDetails.rdlc";
            string reportPath = Path.Combine(contentRootPath, "Reports", reportFileName);

            var reportData = _dbContext.spRptTaskDetails.FromSqlRaw("EXEC spRptTaskDetails @p0, @p1,@p2", EmployeeId, FromDate, ToDate).ToList();
            var reportData2 = _dbContext.spRPTCompanyInformation.FromSqlRaw("EXEC spRPTCompanyInformation").ToList();
            // Create a ReportDataSource for the dataset in the RDLC report
            var reportDataSource = new ReportDataSource("DataSet1", reportData);
            var reportDataSource2 = new ReportDataSource("DataSet2", reportData2);
            var localReport = new LocalReport { ReportPath = reportPath };
            // Add the ReportDataSource to the LocalReport
            localReport.DataSources.Add(reportDataSource);
            localReport.DataSources.Add(reportDataSource2);
            // Set report parameters
            var parameters = new List<ReportParameter>
        {
                  new ReportParameter("EmployeeId", EmployeeId.ToString()),
                  new ReportParameter("FromDate", FromDate),
                  new ReportParameter("ToDate", ToDate)
           
            // Add more parameters as needed
        };
            localReport.SetParameters(parameters);

            byte[] result = localReport.Render("PDF", null);

            if (result == null || result.Length == 0)
            {
                // Handle the case where the result is null or empty
                return BadRequest("Invalid report content");
            }

            // Return the report as a file with content type 'application/pdf'
            return File(result, "application/pdf", "Report.pdf");
        }




        [HttpGet("TaskSummaryReport")]
        public async Task<IActionResult> TaskSummaryReport(string FromDate, string ToDate,string Status)
        {
            
            string contentRootPath = _hostingEnvironment.ContentRootPath;
         
            string reportFileName = "rptManagementTaskStatus.rdlc";
            string reportPath = Path.Combine(contentRootPath, "Reports", reportFileName);
            List<spRptManagementTaskStatus> reportData = null;
            List<spRPTCompanyInformation> reportData2 = null;

            try
            {

                 reportData = _dbContext.spRptManagementTaskStatus.FromSqlRaw("EXEC spRptManagementTaskStatus @p0, @p1,@p2", FromDate, ToDate, Status).ToList();
                 reportData2 = _dbContext.spRPTCompanyInformation.FromSqlRaw("EXEC spRPTCompanyInformation").ToList();
            }
            catch (Exception ex) { }
            
            var reportDataSource = new ReportDataSource("DataSet1", reportData);
            var reportDataSource2 = new ReportDataSource("DataSet2", reportData2);
            var localReport = new LocalReport { ReportPath = reportPath };
           
            localReport.DataSources.Add(reportDataSource);
            localReport.DataSources.Add(reportDataSource2);
            
            var parameters = new List<ReportParameter>
        {
                 
                  new ReportParameter("FromDate", FromDate),
                  new ReportParameter("ToDate", ToDate),
                   new ReportParameter("Status", Status)


        };
            localReport.SetParameters(parameters);

            byte[] result = localReport.Render("PDF", null);

            if (result == null || result.Length == 0)
            {
              
                return BadRequest("Invalid report content");
            }

            
            return File(result, "application/pdf", "Report.pdf");
        }













        [HttpGet("generate-report")]
        public IActionResult GenerateReport()
        {
            // Replace 'YourReportFileName.rdlc' with your actual report file name
            string reportPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory+ "Reports", "UserDetails.rdlc");

            // Replace 'YourDataSet' with the name of your dataset in the RDLC report
            var reportDataSource = new ReportDataSource("dsUsers", GetReportData());

            var localReport = new LocalReport { ReportPath = reportPath };
            localReport.DataSources.Add(reportDataSource);

            byte[] result = localReport.Render("PDF", null);

            if (result == null || result.Length == 0)
            {
                // Handle the case where the result is null or empty
                return BadRequest("Invalid report content");
            }

            return File(result, "application/pdf", "Report.pdf");
        }

        private IEnumerable<UserDto> GetReportData()
        {
            // Replace this with the actual method to fetch or generate report data
            // For example, you might query a database to get the data
            // Ensure the model matches the structure expected by the RDLC report
            return new List<UserDto>
        {
            // Sample data
            new UserDto { FirstName = "jp5", LastName = "jan", Email = "jp5@gm.com", Phone = "+976666661111" },
            // Add more data as needed
        };
        }

        [HttpGet("GetReport")]
        public ActionResult Get(string reportName, string reportType)
        {
            //var reportNameWithLang = reportName + "_" + lang;
            var reportFileByteString = _report.GenerateReportAsync(reportName, reportType);
            return File(reportFileByteString, MediaTypeNames.Application.Octet, getReportName(reportName, reportType));
        }


        private string getReportName(string reportName, string reportType)
        {
            var outputFileName = reportName + ".pdf";

            switch (reportType.ToUpper())
            {
                default:
                case "PDF":
                    outputFileName = reportName + ".pdf";
                    break;
                case "XLS":
                    outputFileName = reportName + ".xls";
                    break;
                case "WORD":
                    outputFileName = reportName + ".doc";
                    break;
            }

            return outputFileName;
        }

    }
}
