using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GovServe_Project.Models.AdminModels
{
    [Table("WorkflowStages")]
    public class WorkflowStage
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int StageID { get; set; }

        // "Service" in requirement means ServiceID (FK)
        [Required(ErrorMessage = "ServiceID is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "ServiceID must be a positive number.")]
        public int ServiceID { get; set; }

        [Required(ErrorMessage = "Responsible role is required.")]
        [StringLength(100, MinimumLength = 2,
            ErrorMessage = "Responsible role must be between 2 and 100 characters.")]
        public string ResponsibleRole { get; set; } = default!;

        [Required(ErrorMessage = "Sequence number is required.")]
        [Range(1, 1000, ErrorMessage = "Sequence number must be between 1 and 1000.")]
        public int SequenceNumber { get; set; }

        [Required(ErrorMessage = "SLA days are required.")]
        [Range(0, 3650, ErrorMessage = "SLA days must be between 0 and 3650.")]
        public int SLA_Days { get; set; }

        // Navigation
        [ForeignKey(nameof(ServiceID))]
        public Service? Service { get; set; }
    }
}
