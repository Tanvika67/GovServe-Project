using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace GovServe_Project.Models
{
    public class CitizenDocument
    {
		[Key]
		public int CitizenDocumentID { get; set; }


		public int ApplicationID { get; set; }
		[ForeignKey("ApplicationID")]
		[ValidateNever]
		public virtual Application Application { get; set; }
		public int DocumentID { get; set; }
		//[ForeignKey("DocumentID")]
		//[ValidateNever]
		//public virtual RequiredDocument Required { get; set; }

		[Required, MaxLength(100)]
		public string DocumentType { get; set; }

		[Required]
		public string FilePath { get; set; }

		[Required]
		public DateTime UploadedDate { get; set; }

		[Required]
		[RegularExpression("Submitted|Under Review|Approved|Rejected",
	ErrorMessage = "Status must be Submitted, Under Review, Approved or Rejected")]
		public string VerificationStatus { get; set; }
	}
}
