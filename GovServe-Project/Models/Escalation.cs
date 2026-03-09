using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace GovServe_Project.Models
{
	using System.ComponentModel.DataAnnotations;
	using System.ComponentModel.DataAnnotations.Schema;

	public class Escalation
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int EscalationId { get; set; }

		[Required]
		[ForeignKey("Case")]
		public int CaseId { get; set; }
		public virtual Case Case { get; set; }

		public int EscalatedByUserId { get; set; }

		public int PreviousOfficerId { get; set; }

		public int NewOfficerId { get; set; }

		[Required]
		public string Reason { get; set; }

		[RegularExpression("Open|Resolved|Closed", ErrorMessage = "Invalid escalation status")]
		public string Status { get; set; } = "Open";

		public DateTime EscalationDate { get; set; }

		public DateTime? ResolvedDate { get; set; }

		public int EscalationLevel { get; set; } = 1;
	}
}

