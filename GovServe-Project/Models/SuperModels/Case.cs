using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using GovServe_Project.Models.AdminModels;
using GovServe_Project.Models.CitizenModels;


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
		[ForeignKey("SupervisorId")]
		public int AssignedOfficerId { get; set; }
		[ForeignKey("AssignedOfficerId")]
		public virtual Users AssignedOfficer { get; set; }
		[Required]
		[ForeignKey("Department")]               //We should only keep how many admin will add that only 
		public int DepartmentID { get; set; }
		public virtual Department Department { get; set; }

		[Required]
		[RegularExpression("Pending|Assigned|Escalated|Completed", ErrorMessage = "Invalid status value")]
		public string Status { get; set; } = "Pending";

		public DateTime? AssignedDate { get; set; }

		public DateTime? CompletedDate { get; set; }
		public bool IsWarningSent { get; set; }

		public bool IsEscalated { get; set; } = false;

		public DateTime LastUpdated { get; set; } = DateTime.Now;

		// SLA → 1 day example
		public int Sladays { get; set; } = 1;
	}

}

