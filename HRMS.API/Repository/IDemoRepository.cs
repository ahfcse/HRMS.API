using HRMS.API.Models;
using HRMS.API.UnitOfWork;

namespace HRMS.API.Repository
{
    public interface IDemoRepository:IGenericRepository<Demo>
    {
        void Test();
    }
}
