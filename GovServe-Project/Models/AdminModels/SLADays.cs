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
        public int ServiceID { get; set; }

        [Required]
        public int RoleID { get; set; }

        [Required]
        [Range(1, 365)]
        public int Days { get; set; }

        // Navigation
        [ForeignKey(nameof(ServiceID))]
        public Service Service { get; set; }

        [ForeignKey(nameof(RoleID))]
        public Role Role { get; set; }
    }
}
