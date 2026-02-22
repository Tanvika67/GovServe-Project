using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GovServe_Project.Models.AdminModels
{
    [Table("RequiredDocuments")]
    public class RequiredDocument
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int DocumentID { get; set; }

        // "Service" in requirement means ServiceID (FK)
        [Required(ErrorMessage = "ServiceID is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "ServiceID must be a positive number.")]
        public int ServiceID { get; set; }

        [Required(ErrorMessage = "Document name is required.")]
        [StringLength(150, MinimumLength = 2,
            ErrorMessage = "Document name must be between 2 and 150 characters.")]
        [RegularExpression(@"^[A-Za-z0-9\s\-\&\(\)]+$",
            ErrorMessage = "Document name allows letters, numbers, spaces, and - & ( ).")]
        public string DocumentName { get; set; } = default!;

        [Required(ErrorMessage = "Mandatory flag is required.")]
        public bool Mandatory { get; set; }

        // Navigation
        [ForeignKey(nameof(ServiceID))]
        public Service? Service { get; set; }
    }
}
