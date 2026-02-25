using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace GovServe_Project.Models.CitizenModels
{
	public class CitizenDocument
	{
		[Key]
		public int CitizenDocumentID { get; set; }

		[Required]
		public int ApplicationID { get; set; }
		[ForeignKey("ApplicationID")]
		public virtual Application Application { get; set; }

		[Required]
		public string DocumentName { get; set; } = default!;

		[Required]
		public string URI { get; set; }

		[Required]
		public DateTime UploadedDate { get; set; }

		[Required]
		[RegularExpression("Submitted|Under Review|Approved|Rejected", ErrorMessage = "Status must be Submitted, Under Review, Approved or Rejected")]
		public string VerificationStatus { get; set; }

	}
}
