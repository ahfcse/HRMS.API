using HRMS.API.Data;
using HRMS.API.Models;
using HRMS.API.Repository;

namespace HRMS.API.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly HrmDbContext _db;
        public IDemoRepository Demos { get; private set; }

        public IAttendanceRepository Attendances { get; private set; }
        public IEmployeeRepository Employees { get; private set; }
        public IUserRepository Users { get; private set; }
        public ITaskRepository Tasks { get; private set; }
        public IHttpContextAccessor httpContextAccessor { get; private set; }
        public IPrayerRepository Prayers { get; private set; }
        public INoticeRepository Notices { get; private set; }
        public IMenuRepository Menus { get; private set; }
        public IMenuPrivilegeRepository MenuPrivileges { get; private set; }
        public IApprovedLeaveRepository ApprovedLeaves { get; private set; }
        public IGatePassRepository GatePass { get; private set; }
        public IStockRepository Stock { get; private set; }
        public IIndentRepository Indent { get; private set; }
        public IMrrRepository Mrr { get; private set; }


        public UnitOfWork(HrmDbContext db)
        { 
            _db = db;
            Demos = new DemoRepository(_db);
            Attendances=new AttendanceRepository(_db, httpContextAccessor);
            Employees=new EmployeeRepository(_db);
            Users=new UserRepository(_db);
            Tasks=new TaskRepository(_db, httpContextAccessor);
            Prayers=new PrayerRepository(_db);
            Notices=new NoticeRepository(_db);
            Menus=new MenuRepository(_db);
            MenuPrivileges=new MenuPrivilegeRepository(_db);
            ApprovedLeaves=new ApprovedLeaveRepository(_db);
            GatePass = new GatePassRepository(_db);
            Stock = new StockRepository(_db);
            Indent = new IndentRepository(_db);
            Mrr = new MrrRepository(_db);
        }
      

       public void Dispose()
        {
           _db.Dispose();
        }

       public async System.Threading.Tasks.Task CompletAsync()
        {
            try
            {
                await _db.SaveChangesAsync();
            }
            catch (Exception ex) { }
        }
    }
}
