using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace GovServe_Project.Models
{
<<<<<<< HEAD
    public class Applications
=======
    public class Application
>>>>>>> 9a3d2318cc569bfc83007b5f01a7f13e94a715a5
    {
		[Key]
		public int ApplicationID { get; set; }

		public int UserId { get; set; }
		[ForeignKey("UserId")]
		[ValidateNever]

		public virtual User User { get; set; }

		public int ServiceID { get; set; }
<<<<<<< HEAD
		//[ForeignKey("ServiceID")]
		//[ValidateNever]
		//public virtual Service Service { get; set; }

		[Required]
		public int DepartmnetID { get; set; }
		//[ForeignKey("DepartmnetID")]
		//[ValidateNever]
		//public virtual Department Department { get; set; }

		[Required]
		public string DepartmnetName { get; set; }
		//[ForeignKey("DepartmneName")]
		//[ValidateNever]
		//public virtual Department Department { get; set; }

=======
		[ForeignKey("ServiceID")]
		public virtual Service Service { get; set; }

		[Required]
		public int DepartmentID { get; set; }
		[ForeignKey("DepartmnetID")]
		public virtual Department Department { get; set; }

		[Required]
		public string DepartmentName { get; set; }
		[ForeignKey("DepartmneName")]
		public virtual Department Departments { get; set; }
>>>>>>> 9a3d2318cc569bfc83007b5f01a7f13e94a715a5

		[Required]
		public DateTime SubmittedDate { get; set; }

		[Required]
<<<<<<< HEAD
		//	[RegularExpression("Submitted|Under Review|Approved|Rejected",
		//ErrorMessage = "Status must be Submitted and No Submitted")]
		public string Status { get; set; }

		public virtual ICollection<CitizenDocument> CitizenDocuments { get; set; }
=======
		public string ApplicationStatus { get; set; }= "Pending";

		public virtual ICollection<CitizenDocument> CitizenDocuments { get; set; }
		//public virtual ICollection<Grievance> Grievances { get; set; }
		//public virtual ICollection<Appeal> Appeals { get; set; }
>>>>>>> 9a3d2318cc569bfc83007b5f01a7f13e94a715a5
	}
}
