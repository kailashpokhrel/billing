using System.ComponentModel.DataAnnotations;

namespace Accounting.Models
{
    public class ChartOfAccount
    {
        [Key]
        public int AccountID { get; set; }
        [Required]
        [StringLength(10)]
        public string? AccountCode { get; set; }
        [Required]
        [StringLength(100)]
        public string? AccountName { get; set; }
        [Required]
        [StringLength(255)]
        public string? Description { get; set; }
        [Required]
        [StringLength(50)]
        public string? AccountType { get; set; }
        [StringLength(10)]
        public string? ParentAccountCode { get; set; }

        public bool IsActive { get; set; }
        public DateTime DateCreated { get; set;}
        public DateTime DateModified { get; set;}

    }
}
