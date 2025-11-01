using System.ComponentModel.DataAnnotations;

namespace HRMS.API.Models
{
    public class Item
    {
        [Key]
        public int ItemID { get; set; }
        public int? ProjectId { get; set; }
        public int? ShutterPcs { get; set; }
        public decimal? ShutterSft { get; set; }
        public int? ProopsUpPcs { get; set; }
        public int? ProopslowPcs { get; set; }
        public int? ScaffSidePcs { get; set; }
        public int? ScaffBumperPcs { get; set; }
        public int? ScaffKachiPcs { get; set; }
        public int? MSPipePcs { get; set; }
        public decimal? MSPipeFt { get; set; }
        public int? BigHopper { get; set; }
        public int? Mixer { get; set; }
        public int? Bending { get; set; }
        public int? Welding { get; set; }
        public int? Vibrator { get; set; }
        public int? RoofHoist { get; set; }
        public decimal? HollowBox { get; set; }
        public decimal? IJoist { get; set; }
        public decimal? CISHeet { get; set; }
        public decimal? MSPlainSheet { get; set; }
        public decimal? SafetyTask { get; set; }
        public decimal? SafetyNet { get; set; }
        public decimal? Wood { get; set; }
        public int? StandFan { get; set; }
        public int? CeilingFan { get; set; }
        public int? FloodLight { get; set; }
        public decimal? Grill { get; set; }
        public decimal? MsPipeOtherSize { get; set; }
        public decimal? MsFencing { get; set; }
        public int? Generator { get; set; }
        public int? MudPump { get; set; }
        public int? Pump { get; set; }
        public int? SuableClam { get; set; }
        public int? JoinPin { get; set; }
        public int? WaterTank { get; set; }
    }
}
