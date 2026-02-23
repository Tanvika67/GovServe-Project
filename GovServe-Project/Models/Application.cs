using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace GovServe_Project.Models
{
	public class Application

	{
		[Key]
		public int ApplicationID { get; set; }

		public int UserId { get; set; }
		[ForeignKey("UserId")]
		[ValidateNever]

		public virtual User User { get; set; }

		public int ServiceID { get; set; }

		[ForeignKey("ServiceID")]
		public virtual Service Service { get; set; }

		[Required]
		public int DepartmentID { get; set; }
		[ForeignKey("DepartmentID")]
		public virtual Department Department { get; set; }

		[Required]
		public string DepartmentName { get; set; }
		[ForeignKey("DepartmentName")]
		public virtual Department Departments { get; set; }
		[Required]
		public DateTime SubmittedDate { get; set; }

		[Required]
		//	[RegularExpression("Submitted|Under Review|Approved|Rejected",
		//ErrorMessage = "Status must be Submitted and No Submitted")]
		public string Status { get; set; }

		public virtual ICollection<CitizenDocument> CitizenDocuments { get; set; }
		public string ApplicationStatus { get; set; }= "Pending";

		//public virtual ICollection<Grievance> Grievances { get; set; }
		//public virtual ICollection<Appeal> Appeals { get; set; } 
	}
}
