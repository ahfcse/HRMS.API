namespace HRMS.API.Repository
{
    public interface IReportRepository
    {
        byte[] GenerateReportAsync(string reportName, string reportType);
    }
}
