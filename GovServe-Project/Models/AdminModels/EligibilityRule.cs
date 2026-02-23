using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GovServe_Project.Models.AdminModels
{
    [Table("EligibilityRules")]
    public class EligibilityRule
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int RuleID { get; set; }

        // "Service" in requirement means ServiceID (FK)
        [Required(ErrorMessage = "ServiceID is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "ServiceID must be a positive number.")]
        public int ServiceID { get; set; }

        [Required(ErrorMessage = "Rule description is required.")]
        [StringLength(1000, MinimumLength = 5,
            ErrorMessage = "Rule description must be between 5 and 1000 characters.")]
        [RegularExpression(@"^[A-Za-z0-9\s\-\&\(\)]+$",
            ErrorMessage = "Service name allows letters, numbers, spaces, and - & ( ).")]
        public string RuleDescription { get; set; } = default!;



        [Required(ErrorMessage = "Rule expression is required.")]
        [StringLength(4000, ErrorMessage = "Rule expression cannot exceed 4000 characters.")]
        [RegularExpression(@"^[A-Za-z0-9]+\s*(==|!=|>=|<=|>|<)\s*(true|false|\d+)(\s+(AND|OR)\s+[A-Za-z]+\s*(==|!=|>=|<=|>|<)\s*(true|false|\d+))*$",
            ErrorMessage = "Invalid rule expression. Allowed format: FieldName (==, !=, >=, <=, >, <) true/false/number. Multiple conditions must be joined using AND or OR.\r\n ")]
        public string RuleExpression { get; set; } = default!; 

        // Navigation
        [ForeignKey(nameof(ServiceID))]
        public Service? Service { get; set; }
    }
}