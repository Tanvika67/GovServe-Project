using GovServe_Project.Models.AdminModels;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GovServe_Project.Models
{
    [Table("WorkflowStages")]
    public class WorkflowStage
    {
        [Key]
        public int StageID { get; set; }

        [Required]
        public int ServiceID { get; set; }

        [Required]
        public int ResponsibleRoleID { get; set; }  // ✅ FIXED

        [Required]
        [Range(1, int.MaxValue)]
        public int SequenceNumber { get; set; }

        public int SLA_Days { get; set; } // ✅ Auto fetched

        [ForeignKey(nameof(ServiceID))]
        public Service? Service { get; set; }

        [ForeignKey(nameof(ResponsibleRoleID))]
        public Role? Role { get; set; }
    }
}
