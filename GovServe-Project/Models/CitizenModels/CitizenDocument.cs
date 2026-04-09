using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using GovServe_Project.Models.AdminModels;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace GovServe_Project.Models.CitizenModels
{
	public class CitizenDocument
	{
		[Key]
		public int CitizenDocumentID { get; set; }

		public int UserId { get; set; }
		[ForeignKey("UserId")]
		public virtual Users User { get; set; }

		[Required]
		public int ApplicationID { get; set; }
		[ForeignKey("ApplicationID")]
		public virtual Application Application { get; set; }

        public int  DocumentID { get; set; }
		[ForeignKey("DocumentID")]
		public virtual RequiredDocument RequiredDocument{ get; set; }

		[Required]
		public string URI { get; set; }

		[Required]
		public DateTime UploadedDate { get; set; }

		[Required]
		[RegularExpression("Submitted|Under Review|Approved|Rejected", ErrorMessage = "Status must be Submitted, Under Review, Approved or Rejected")]
		public string VerificationStatus { get; set; }

	}
}
