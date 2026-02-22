using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using GovServe_Project.Enum;

namespace GovServe_Project.Models.AdminModels
{
    [Table("Services")]
    public class Service
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ServiceID { get; set; }

        [Required(ErrorMessage = "DepartmentID is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "DepartmentID must be a positive number.")]
        public int DepartmentID { get; set; }

        [Required(ErrorMessage = "Service name is required.")]
        [StringLength(120, MinimumLength = 2,
            ErrorMessage = "Service name must be between 2 and 120 characters.")]
        [RegularExpression(@"^[A-Za-z0-9\s\-\&\(\)]+$",
            ErrorMessage = "Service name allows letters, numbers, spaces, and - & ( ).")]
        public string ServiceName { get; set; } = default!;

        [StringLength(1000, ErrorMessage = "Description cannot exceed 1000 characters.")]
        public string? Description { get; set; }

        [Required(ErrorMessage = "SLA days are required.")]
        [Range(1, 365, ErrorMessage = "SLA days must be between 1 and 365.")]
        public int SLA_Days { get; set; }

        [Required(ErrorMessage = "Status is required.")]
        public ServiceStatus Status { get; set; } = ServiceStatus.Active;


        // Navigation
        [ForeignKey(nameof(DepartmentID))]
        public Department? Department { get; set; }

        public ICollection<EligibilityRule> EligibilityRules { get; set; } = new List<EligibilityRule>();
        public ICollection<RequiredDocument> RequiredDocuments { get; set; } = new List<RequiredDocument>();
        public ICollection<WorkflowStage> WorkflowStages { get; set; } = new List<WorkflowStage>();
        public ICollection<SLARecord> SLARecords { get; set; } = new List<SLARecord>();
    }
}