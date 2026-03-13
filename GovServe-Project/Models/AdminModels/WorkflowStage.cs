using GovServe_Project.Models.AdminModels;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GovServe_Project.Models
{
    [Table("WorkflowStages")]
    public class WorkflowStage
    {
        [Key]
        public int StageID { get; set; } // Primary Key

        [Required]
        public int ServiceID { get; set; } // FK to Service

        [Required]
        public string ResponsibleRole { get; set; } = string.Empty; // Role for this stage

        [Required]
        public int SequenceNumber { get; set; } // Order of stage

        public int SLA_Days { get; set; } // Auto-fetched from SLADays table

        [ForeignKey(nameof(ServiceID))]
        public Service? Service { get; set; } // Navigation property
    }
}
