using HRMS.API.Globals;
using HRMS.API.Migrations;
using HRMS.API.Models;
using HRMS.API.Repository;
using HRMS.API.RequestModels;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using User = HRMS.API.Models.User;

namespace HRMS.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : Controller
    {

      
        private readonly IAuthRepository _authRepository;
        private readonly IHttpContextAccessor _contextAccessor;
        public AuthController(IAuthRepository authRepository, IHttpContextAccessor contextAccessor)
        {
            _authRepository = authRepository;
            _contextAccessor = contextAccessor;
        }
        [HttpPost("Login")]
        public string Login(LoginRequest loginRequest)
        {
            var token=_authRepository.Login(loginRequest);

            //string UserId = JsonConvert.SerializeObject(178);
            //_contextAccessor.HttpContext.Session.SetString("UserId", UserId);

            return JsonConvert.SerializeObject(token);
        }
        [HttpPost("AddUser")]
        public User AddUser(Models.User user)
        {
            if(_authRepository.IsUserExist(user.EmployeeId, user.Email))
            {
                GlobalVariables.Instance.Error = "User Already Exist";
                throw new Exception("User Already Exist");


            }
            else
            {
                Random random = new Random();
                string RandomNumber = (random.Next(100000, 999999)).ToString();

                string Body = $"Your OTP is {RandomNumber}";

                Email.Instance.SendMail(user.Email, "OTP Verification", Body);
                GlobalVariables.Instance.EmployeeId = user.EmployeeId;
                GlobalVariables.Instance.OTP = RandomNumber;
                GlobalVariables.Instance.Email = user.Email;
                var data = _authRepository.AddUser(user);

                return data;
            }
          
        }

        [HttpPost("VeryfyOtp")]
        public bool VeryfyOtp(string OTP)
        {


            if (GlobalVariables.Instance.OTP == OTP)
            {
              _authRepository.UserVerify(GlobalVariables.Instance.EmployeeId);
                string Body = "Your Email Verified Successfully. Please Contact Administrator to Active your Account Or wait for the activation mail";
                Email.Instance.SendMail(GlobalVariables.Instance.Email, "Email Verified Successfully", Body);
                return true;
            }

            else return false;
        }


        [HttpPost("AddRole")]
        public Role AddRole(Role role)
        {
            var data = _authRepository.AddRole(role);
          
            return data;
        }
        [HttpPost("AssignRole")]
        public bool AssignRoleToUser(AddUserRole role)
        {
            var AddedUserRole=_authRepository.AssignRoleToUser(role);
            return AddedUserRole;
        }
    }
}
