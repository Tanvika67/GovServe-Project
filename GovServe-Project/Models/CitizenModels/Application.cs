using GovServe_Project.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using GovServe_Project.Enum;
using GovServe_Project.Models.AdminModels;
using GovServe_Project.Models.AdminModels;

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

		[Required]
		public string Description { get; set; }

		[Required]
		public int DepartmentID { get; set; }
		[ForeignKey("DepartmentID")]
		public virtual Department Department { get; set; }


		[Required]
		public DateTime SubmittedDate { get; set; }

		[Required]
		public DateTime? CompletedDate { get; set; }


		[Required]
		[MaxLength(50)]
		public string ApplicationStatus { get; set; } = "Submitted";
		public virtual ICollection<CitizenDocument> CitizenDocuments { get; set; }
		//public virtual ICollection<Grievance> Grievances { get; set; }
		//public virtual ICollection<Appeal> Appeals { get; set; }
	}

}
