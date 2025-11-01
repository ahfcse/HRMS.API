namespace HRMS.API.Models
{
    public class Prayer
    {
        public int PrayerId { get; set; }
        public string Namaz { get; set; }
        public DateTime Azan { get; set; }
        public DateTime Jamat { get; set; }

    }
}
