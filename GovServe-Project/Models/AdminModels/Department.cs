using GovServe_Project.Enum;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GovServe_Project.Models.AdminModels
{
    [Table("Departments")]
    public class Department
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int DepartmentID { get; set; }

        [Required(ErrorMessage = "Department name is required.")]
        [StringLength(100, MinimumLength = 2,
            ErrorMessage = "Department name must be between 2 and 100 characters.")]
        [RegularExpression(@"^[A-Za-z0-9\s\-\&\(\)]+$",
            ErrorMessage = "Department name allows letters, numbers, spaces, and - & ( ).")]
        public string DepartmentName { get; set; } = default!;

        [StringLength(500, ErrorMessage = "Description cannot exceed 500 characters.")]
        public string? Description { get; set; }


        [Required]
        public DepartmentStatus Status { get; set; } = DepartmentStatus.Active;


        // Navigation
        //public ICollection<Service> Services { get; set; } = new List<Service>();
    }
}
