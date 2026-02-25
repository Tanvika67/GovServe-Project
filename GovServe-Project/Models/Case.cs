using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using GovServe_Project.Models.AdminModels;


namespace GovServe_Project.Models
{
	public class Case
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int CaseId { get; set; }

		[Required]
		[ForeignKey("Application")]
		public int ApplicationId { get; set; }
		public virtual Application Application { get; set; }

		[Required]
		public int SupervisorId { get; set; }
		[ForeignKey("User")]
		public int AssignedOfficerId { get; set; }

		[Required]
		[ForeignKey("Department")]               //We should only keep how many admin will add that only 
		public int DepartmentID { get; set; }
		public virtual Department Department { get; set; }

		[Required]
		[RegularExpression("Pending|Assigned|Escalated|Completed", ErrorMessage = "Invalid status value")]
		public string Status { get; set; } = "Pending";

		public DateTime? AssignedDate { get; set; }

		public DateTime? CompletedDate { get; set; }

		public bool IsEscalated { get; set; } = false;

		public DateTime LastUpdated { get; set; } = DateTime.Now;

		// SLA → 48 hours example
		public int SlaHours { get; set; } = 48;
	}

}

