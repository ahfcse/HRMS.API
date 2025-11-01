using HRMS.API.Models;
using HRMS.API.RequestModels;

namespace HRMS.API.Repository
{
    public interface IAuthRepository
    {
        User AddUser(User user);
        string Login(LoginRequest loginRequest);

        Role AddRole(Role role);
        bool AssignRoleToUser(AddUserRole obj);
        bool UserVerify(int EmployeeId);
        bool IsUserExist(int EmployeeId, string Email);
       


       
    }
}
