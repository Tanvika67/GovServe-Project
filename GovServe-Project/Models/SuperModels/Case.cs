using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using GovServe_Project.Models.AdminModels;
using GovServe_Project.Models.CitizenModels;

namespace GovServe_Project.Models.SuperModels
{
	public class Case
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int CaseId { get; set; }

		public int ApplicationID { get; set; }
		[ForeignKey("ApplicationID")]
		public virtual Application Application { get; set; }

		public int UserId { get; set; }
		[ForeignKey("UserId")]
		public virtual Users User { get; set; }

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
		public string Status { get; set; } = "Assigned";

		public DateTime? AssignedDate { get; set; }

		public DateTime? CompletedDate { get; set; }
		public bool IsWarningSent { get; set; }

		public bool IsEscalated { get; set; } = false;

		public DateTime LastUpdated { get; set; } = DateTime.Now;

		public string? RejectionReason { get; set; }

	}

}

