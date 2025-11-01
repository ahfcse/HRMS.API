namespace HRMS.API.Models
{
    public class Menu
    {
        public int MenuId { get; set; }
        public string? MenuName { get; set; }
        public int? ParentId { get; set; }
        public string? RouterLink { get; set; }
        public string? Icon { get; set; }
        public string? Title { get; set; }
        public bool? IsVisible { get; set; }
    }
}
