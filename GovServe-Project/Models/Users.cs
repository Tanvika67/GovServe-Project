using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using GovServe_Project.Enum;
using GovServe_Project.Models.CitizenModels;
using GovServe_Project.Models.AdminModels;
using GovServe_Project.Models.GrievanceAppealModel;
using GovServe_Project.Models.SuperModels;


namespace GovServe_Project.Models
{
	public class Users
	{
		[Key]
		public int UserId { get; set; }

		[Required]
		[MaxLength(100)]
		public string FullName { get; set; }

		[Required]
		[EmailAddress]
		public string Email { get; set; }

		[Required]
		[MaxLength(15)]
		public string Phone { get; set; }

		[Required]
		[RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^A-Za-z0-9]).{8,}$",
		ErrorMessage = "Password must contain at least 1 uppercase, 1 lowercase, 1 number, and 1 special character.")]
		public string Password { get; set; }

        
        public int? DepartmentID { get; set; }
		[ForeignKey("DepartmentID")]
		public virtual Department Department { get; set; }

		public string? DepartmentName { get; set; }

		[Required]
        public int RoleID { get; set; }
		[ForeignKey("RoleID")]
		public virtual Role Role { get; set; }

		public string RoleName { get; set; }

		public string Status { get; set; } = "Approved";

		public virtual ICollection<Application> Applications { get; set; }
		public virtual ICollection<Grievance> Grievances { get; set; }
		public virtual ICollection<Appeal> Appeals { get; set; }
		public virtual ICollection<CitizenDocument>CitizenDocuments { get; set; }
		public virtual ICollection<Case> Cases { get; set; }

	}
}
