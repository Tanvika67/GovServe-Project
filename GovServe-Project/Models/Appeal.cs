using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using GovServe_Project.Enum;
using GovServe_Project.Models;

namespace GovServe.Models
{
	// Appeal entity for rejected grievances
	public class Appeal
	{
		[Key] // Primary key
		public int AppealID { get; set; }

		[Required(ErrorMessage = "GrievanceID is required.")]
		public int GrievanceID { get; set; } // Links to the rejected grievance

		[Required(ErrorMessage = "CitizenID is required.")]
		public int CitizenID { get; set; } // Who filed the appeal

		[MaxLength(500, ErrorMessage = "Remarks can be maximum 500 characters.")]
		public string Remarks { get; set; } // Supervisor/Admin remarks

		[Required]
		public AppealStatus AppealStatus { get; set; } // Current status of appeal

		[Required]
		public DateTime AppealDate { get; set; } // When appeal was filed

		// Navigation property back to the grievance
		public Grievance Grievance { get; set; }
	}
}