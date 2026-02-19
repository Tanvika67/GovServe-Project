using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GovServe.Models
{
    public class EligibilityRule
    {
        [Key]
        public int RuleID { get; set; }

        [Required (ErrorMessage="Service ID is Required")]
        [ForeignKey(nameof(Service))]
        public int ServiceID { get; set; }

        [ValidateNever]
        public virtual Service Service { get; set; }

        [Required(ErrorMessage ="Rule Description is Required.")]
        [StringLength(300,ErrorMessage="Rule Description cannot exceed 300 character.")]
        public string RuleDescription {  get; set; }

        [Display(Name ="Rule Expression (Optional)")]
        public string RuleExpression {  get; set; }

        

    }
}
