using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GovServe.Models
{
    public class RequiredDocument
    {
        [Key]
        public int DocumentID { get; set; }

        [Required(ErrorMessage = "Service ID is required.")]
        [ForeignKey(nameof(Service))]
        public int ServiceID { get; set; }
        [ValidateNever]
        public virtual Service Service { get; set; }

        [Required(ErrorMessage = "Document Name is required.")]
        [StringLength(150, ErrorMessage = "Document Name cannot exceed 150 characters.")]
        [Display(Name = "Document Name")]
        public string DocumentName { get; set; }

        [Display(Name = "Is Mandatory?")]
        public bool Mandatory { get; set; }

        [StringLength(100, ErrorMessage = "Document Type cannot exceed 100 characters.")]
        [Display(Name = "Document Type")]
        public string DocumentType { get; set; } // e.g. ID Proof

        // ⭐ NEW: Allowed formats (PDF or Photo)
        [Required]
        [Display(Name = "Allowed File Formats")]
        public string AllowedFormats { get; set; } = "PDF,JPG,PNG";

        // Navigation
       
    }
}