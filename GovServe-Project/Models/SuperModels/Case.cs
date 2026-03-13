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
		 
		// Citizen linked automatically from Application
		public int UserId { get; set; }
		[ForeignKey("UserId")]
		public virtual Users User { get; set; }

		// Officer assigned automatically
		public int AssignedOfficerId { get; set; }

		[Required]
		[ForeignKey("Department")]
		public int DepartmentID { get; set; }
		public virtual Department Department { get; set; }

		[Required]
		[RegularExpression("Pending|Assigned|Escalated|Completed", ErrorMessage = "Invalid status value")]
		public string Status { get; set; } = "Assigned";
		public DateTime? AssignedDate { get; set; }
		public bool IsWarningSent { get; set; }
		public bool IsEscalated { get; set; } = false;
		public DateTime? CompletedDate { get; set; }
		public DateTime LastUpdated { get; set; } = DateTime.Now;
		public string? RejectionReason { get; set; }
	}

}

