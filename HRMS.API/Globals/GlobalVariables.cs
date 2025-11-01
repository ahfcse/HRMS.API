namespace HRMS.API.Globals
{
    public class GlobalVariables
    {
        #region Sigleton Pattarn
        private static GlobalVariables _Instance { get; set; }
        public static GlobalVariables Instance
        {
            get
            {
                if (_Instance == null)
                    _Instance = new GlobalVariables();
                return _Instance;
            }
        }
        private GlobalVariables()
        {

        }

        #endregion

        public int EmployeeId { get; set; }

        public string OTP { get; set; }
        public string Email { get; set; }
        public string Error { get; set; }


    }
}
