using GovServe_Project.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using GovServe_Project.Models.AdminModels;
using GovServe_Project.Models.GrievanceAppealModel;
using GovServe_Project.Models.SuperModels;


namespace GovServe_Project.Models.CitizenModels
{

	public class Application
	{

		[Key] 
		public int ApplicationID { get; set; }


		[Required]
		public int UserId { get; set; }
		[ForeignKey("UserId")]
		public virtual Users User { get; set; }


		[Required]
		public int ServiceID { get; set; }
		[ForeignKey("ServiceID")]
		public virtual Service Service { get; set; }


		[Required]
		public string ServiceName { get; set; }

		public int DepartmentID { get; set; }
		[ForeignKey("DepartmentID")]
		public virtual Department Department { get; set; }
		
		public DateTime SubmittedDate { get; set; }

		public DateTime? CompletedDate { get; set; }

		public string ApplicationStatus { get; set; } = "Submitted";
		public virtual ICollection<CitizenDocument> CitizenDocuments { get; set; }
		public virtual ICollection<Grievance> Grievances { get; set; }
		public virtual ICollection<Appeal> Appeals { get; set; }
		public virtual Case Case { get; set; }
	}

}
