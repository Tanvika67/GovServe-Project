using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GovServe.Models
{
    public class Service
    {
        [Key]
        public int ServiceID { get; set; }

        [Required]
        [ForeignKey(nameof(Department))]
        public int DepartmentID { get; set; }

        [ValidateNever]
        public virtual Department Department { get; set; }

        [Required(ErrorMessage = "Service Name is required.")]
        [StringLength(150, MinimumLength = 5,
            ErrorMessage = "Service Name must be between 5 and 150 characters.")]
        public string ServiceName { get; set; }

        [Required(ErrorMessage = "Description is required.")]
        [StringLength(500, ErrorMessage = "Description cannot exceed 500 characters.")]
        public string Description { get; set; }

        [Range(0, 365, ErrorMessage = "SLA days must be between 0 and 365.")]
        [Display(Name = "SLA (Days)")]
        public int SLA_Days { get; set; }

        [Display(Name = "Active Status")]
        public bool Status { get; set; }

        [Required]
        [Display(Name = "Created On")]
        public DateTime CreatedOn { get; set; } = DateTime.Now;

        // Navigation Collections (not validated on model binding)
        [ValidateNever]
        public virtual ICollection<EligibilityRule> EligibilityRules { get; set; }
            = new List<EligibilityRule>();

        [ValidateNever]
        public virtual ICollection<RequiredDocument> RequiredDocuments { get; set; }
            = new List<RequiredDocument>();

        [ValidateNever]
        public virtual ICollection<WorkflowStage> WorkflowStages { get; set; }
            = new List<WorkflowStage>();

        // Future Expansion
        // public ICollection<ServiceReport> ServiceReports { get; set; } 
        //    = new List<ServiceReport>();
    }
}