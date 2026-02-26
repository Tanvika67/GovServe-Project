using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GovServe_Project.Models.AdminModels
{
    [Table("SLADays")]
    public class SLADays
    {
        [Key]
        public int SLADayID { get; set; }

        [Required]
        public string RoleName { get; set; } = string.Empty;

        [Required]
        public int Days { get; set; }
    }
}
