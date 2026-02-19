using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static System.Net.Mime.MediaTypeNames;
namespace GovServe.Models
{
	public class Case
	{
		[Key]
		public int CaseId { get; set; }   //Primary key
		[Required]
		[ForeignKey("Application")]
		public int ApplicationId { get; set; } //FK from Application Table

		public virtual Applications Application { get; set; }

		public int AssignedOfficerId { get; set; }
		[RegularExpression("Pending|Approved|Rejected", ErrorMessage = "Status must be Pending,Approved or Rejected")]
		public string Status { get; set; }
		[Required]
		public DateTime AssignedDate { get; set; }
		[Required]
		public DateTime CreatedDate { get; set; }
		[Required]
		public DateTime SLADeadline { get; set; }

	}
}
