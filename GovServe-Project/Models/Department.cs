using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GovServe.Models
{
    public class Department
    {
        [Key]
        public int DepartmentID { get; set; }

        [Required(ErrorMessage = "Department Name is required.")]
        [StringLength(100, ErrorMessage = "Department Name cannot exceed 100 characters.")]
        public string DepartmentName { get; set; }

        [StringLength(250, ErrorMessage = "Description cannot exceed 250 characters.")]
        public string Description { get; set; }

        [Display(Name = "Is Active")]
        public bool IsActive { get; set; } = true;

        // Navigation Property
        public virtual ICollection<Service> Services { get; set; } = new List<Service>();
    }
}
