using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GovServe.Models
{
    public class WorkflowStage
    {
        [Key]
        public int StageID { get; set; }

        [Required(ErrorMessage = "Service is required.")]
        [ForeignKey(nameof(Service))]
        [Display(Name = "Service")]
        public int ServiceID { get; set; }

        [ValidateNever]
        public virtual Service Service { get; set; }

        [Required(ErrorMessage = "Responsible role is required.")]
        [StringLength(50, ErrorMessage = "Responsible role cannot exceed 50 characters.")]
        [Display(Name = "Responsible Role")]
        public string ResponsibleRole { get; set; }  

        [Required(ErrorMessage = "Sequence number is required.")]
        [Range(1, 20, ErrorMessage = "Sequence number must be between 1 and 20.")]
        [Display(Name = "Sequence Number")]
        public int SequenceNumber { get; set; }

        [Required(ErrorMessage = "SLA (Days) is required.")]
        [Range(1, 365, ErrorMessage = "SLA (Days) must be between 1 and 365.")]
        [Display(Name = "SLA (Days)")]
        public int SLA_Days { get; set; }

        

        // Optional: track whether this stage is terminal/auto-approval, etc.
        // [Display(Name = "Final Stage")]
        // public bool IsFinalStage { get; set; } = false;
    }
}
