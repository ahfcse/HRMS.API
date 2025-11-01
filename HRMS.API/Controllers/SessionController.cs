using HRMS.API.Globals;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HRMS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SessionController : ControllerBase
    {
        [HttpGet("GetSessionInfo")]
        public IEnumerable<string>GetSessionInfo()
        {
            List<string> sessionInfo = new List<string>();

            if(string.IsNullOrWhiteSpace(HttpContext.Session.GetString(SessionVariables.SessionKeyUserName)))
            {
                HttpContext.Session.SetString(SessionVariables.SessionKeyUserName, "Foysal");
                HttpContext.Session.SetString(SessionVariables.SessionKeyId, Guid.NewGuid().ToString());
            }

            string UserName = HttpContext.Session.GetString(SessionVariables.SessionKeyUserName);
            string UserId=HttpContext.Session.GetString(SessionVariables.SessionKeyId); 

            sessionInfo.Add(UserName);
            sessionInfo.Add(UserId);


            return sessionInfo;
        }

    }
}
