namespace Accounting.Models
{
    public class Setting
    {
        public int Id { get; set; }
        public string Name { get; set; } 
        public string Value { get; set; }
        public string Type { get; set; } 
        public bool? IsActive { get; set; }
        public DateTime? CreatedAt { get; set; }
        public string CreatedBy { get; set; } 
        public DateTime? UpdatedAt { get; set; }
        public string UpdatedBy { get; set; } 
    }
}
