namespace HRMS.API.Globals
{
    public class SessionVariables
    {
        public const string SessionKeyUserName = "SessionKeyUserName";
        public const string SessionKeyId = "SessionKeyId";

        public const string EmployeeId = "EmployeeId";
        public const string OTP = "OTP";

    }
    public enum SessionKeyEnum
    {
        SessionKeyUserName=0,
        SessionKeyId=1,
        UserId=2,
        OTP=3
    }
}
