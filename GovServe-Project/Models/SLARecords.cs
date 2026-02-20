using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using GovServe_Project.Enum;

namespace GovServe_Project.Models
{
    [Table("SLARecords")]
    public class SLARecord : IValidatableObject
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int SLARecordID { get; set; }

        [Required(ErrorMessage = "CaseID is required.")]
        [StringLength(100, MinimumLength = 1, ErrorMessage = "CaseID must be between 1 and 100 characters.")]
        public string CaseID { get; set; } = default!;

        [Required(ErrorMessage = "StageID is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "StageID must be a positive number.")]
        public int StageID { get; set; }

        [Required(ErrorMessage = "Start date is required.")]
        public DateTime StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        [Required(ErrorMessage = "Status is required.")]
        public SLAStatus Status { get; set; } = SLAStatus.OnTime;

        // Navigation
        [ForeignKey(nameof(StageID))]
        public WorkflowStage? Stage { get; set; }

        // Cross-field validations
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (EndDate.HasValue && EndDate.Value < StartDate)
            {
                yield return new ValidationResult(
                    "EndDate cannot be earlier than StartDate.",
                    new[] { nameof(EndDate), nameof(StartDate) });
            }
        }
    }
}