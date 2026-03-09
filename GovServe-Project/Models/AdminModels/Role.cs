using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace GovServe_Project.Models.AdminModels
{
    
    [Table("Roles")]
    public class Role
    {
        [Key]
        public int RoleID { get; set; }

        [Required]
        [MaxLength(100)]
        public string RoleName { get; set; } = string.Empty;

        // Navigation
        public ICollection<WorkflowStage>? WorkflowStages { get; set; }
    }

}
