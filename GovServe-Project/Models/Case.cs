using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static System.Net.Mime.MediaTypeNames;
namespace GovServe.Models
{
	public class Case
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int CaseId { get; set; }   //Primary key
		[Required]
		[ForeignKey("Application")]
		public int ApplicationId { get; set; } //FK from Application Table
		public virtual Application Application { get; set; }
		[Required]
		public int SupervisorId {  get; set; }
		[Required]
		public int AssignedOfficerId {  get; set; }
		[Required]
		public bool IsAssigned {  get; set; }=false;
		[Required]
		[RegularExpression("^(Pending|Approved|Rejected)$")]   
		public string Status { get; set; }

		// Make these nullable in DB if not always known at creation time
		[Required]
		public DateTime? AssignedDate { get; set; }
		[Required]
		public DateTime? CompletedDate { get; set; }
		[Required]
		public bool IsEscalated { get; set; } = false;
		[Required]
		public DateTime LastUpdated { get; set; }

	}
}
