using HRMS.API.Repository;

namespace HRMS.API.UnitOfWork
{
    public interface IUnitOfWork:IDisposable
    {
         IDemoRepository Demos { get; }
        IAttendanceRepository Attendances { get; }
        IEmployeeRepository Employees { get; }
        IUserRepository Users { get; }
        ITaskRepository Tasks { get; }
        IPrayerRepository Prayers { get; }
        INoticeRepository Notices { get; }
        IMenuRepository Menus { get; }
        IMenuPrivilegeRepository MenuPrivileges { get; }
        IApprovedLeaveRepository ApprovedLeaves { get; }
        IGatePassRepository GatePass { get; }
        IStockRepository Stock { get; }
        IIndentRepository Indent { get; }
        IMrrRepository Mrr { get; }
        Task CompletAsync();
    }
}
