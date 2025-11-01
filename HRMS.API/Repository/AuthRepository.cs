using HRMS.API.Data;
using HRMS.API.Globals;
using HRMS.API.Models;
using HRMS.API.RequestModels;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using User = HRMS.API.Models.User;

namespace HRMS.API.Repository
{
    public class AuthRepository : IAuthRepository
    {
        private readonly HrmDbContext _db;
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _contextAccessor;
        public AuthRepository(HrmDbContext db, IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
        {
            _db = db;
            _configuration = configuration; 
            _contextAccessor = httpContextAccessor;

        }
      
        public User AddUser(User user)
        {
            

            var data=_db.Users.Add(user);
            _db.SaveChanges();

    
            return data.Entity;
        }

        public bool UserVerify(int EmployeeId)
        {
           int row=_db.Database.ExecuteSqlRaw($"UPDATE Users SET IsVerified=1 WHERE UserId={EmployeeId}");
          if(row > 0) { return true; }
          else { return false; }
        }


        public string Login(LoginRequest loginRequest)
        {
            if (loginRequest.Email != null && loginRequest.Password != null)
            {
                var user = _db.Users.SingleOrDefault(s => s.Email == loginRequest.Email && s.Password == loginRequest.Password && s.IsActive==true && s.IsVerified==true);
                if (user != null)
                {
                    var claims = new List<Claim> {
                        new Claim(JwtRegisteredClaimNames.Sub, _configuration["Jwt:Subject"]),
                        new Claim("Id", user.Id.ToString()),
                        new Claim("Email", user.Email),
                        new Claim("Name", user.Name),
                        new Claim("EmployeeId",user.EmployeeId.ToString()),
                        //new Claim("Image",user.Image.ToString()),
                    };
                    var userRoles = _db.UserRoles.Where(u => u.EmployeeId == user.EmployeeId).ToList();
                    var roleIds = userRoles.Select(s => s.RoleId).ToList();
                    var roles = _db.Roles.Where(r => roleIds.Contains(r.Id)).ToList();
                    foreach (var role in roles)
                    {
                        claims.Add(new Claim(ClaimTypes.Role, role.Name));
                    }
                    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
                    var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                    var token = new JwtSecurityToken(
                        _configuration["Jwt:Issuer"],
                        _configuration["Jwt:Audience"],
                        claims,
                        expires: DateTime.UtcNow.AddHours(10),
                        signingCredentials: signIn);

                    var jwtToken = new JwtSecurityTokenHandler().WriteToken(token);



                    string EmployeeId = JsonConvert.SerializeObject(user.EmployeeId);
                    _contextAccessor.HttpContext.Session.SetString("EmployeeId", EmployeeId);




                    return jwtToken;
                }
                else
                {
                    GlobalVariables.Instance.Error = "user is not valid";
                    throw new Exception("user is not valid");
                }
            }
            else
            {
                GlobalVariables.Instance.Error = "credentials are not valid";
                throw new Exception("credentials are not valid");
            }
        }
        public bool AssignRoleToUser(AddUserRole obj)
        {
            try
            {

                var UserRoles = new List<UserRole>();
                var users = _db.Users.FirstOrDefault(x => x.EmployeeId == obj.EmployeeId);
                if (users == null)
                {
                    GlobalVariables.Instance.Error = "Invalid User";
                    throw new Exception("Invalid User");
                }
                   

                foreach (int roleid in obj.RoleIds)
                {
                    var userRole = new UserRole();
                    userRole.RoleId = roleid;
                    userRole.EmployeeId = obj.EmployeeId;
                    UserRoles.Add(userRole);
                }
                _db.UserRoles.AddRange(UserRoles);
                _db.SaveChanges();

               


                return true;
            }
            catch
            {
                return false;
            }
        }
        public Role AddRole(Role role)
        {
          var data= _db.Roles.Add(role);
            _db.SaveChanges();
            return role;
        }
        public bool IsUserExist(int EmployeeId, string Email)
        {
            
                var IsUserIdExist=_db.Users.Any(x => x.EmployeeId == EmployeeId);
                var EmailIsExist=_db.Users.Any(_x => _x.Email == Email);    
                if(IsUserIdExist || EmailIsExist)
                {
                    return true;
                }

                return false;
            
           
        }




    }
}
