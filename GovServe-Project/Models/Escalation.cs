using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace GovServe.Models
{
	public class Escalation
	{
		[Key]
		public int EscalationId { get; set; }
		[Required]
		[ForeignKey("Case")]
		public int CaseId {  get; set; }
		[Required]
		public int EscalatedByUserId { get; set; }             //Citizen or GrievanceOfficer or Supervisor
		public int PreviousOfficerId {  get; set; }
		public int NewOfficerId { get; set; }
		
		[Required]
		[StringLength(500,MinimumLength =5)]
		public string Reason { get; set; }
		[Required]
		[RegularExpression("Pending|Resolved|",ErrorMessage ="Status must be Pending or Resolved")]
		public string Status { get; set; }
		[Required]
		public DateTime EscalationDate {  get; set; }
		[Required]
		public DateTime ResolvedDate {  get; set; }
		[Required]
		public int EscalationLevel { get; set; } = 1;

	}
}
