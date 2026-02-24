using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using GovServe_Project.Enum;
using GovServe_Project.Models;

namespace GovServe.Models
{
	// Grievance entity representing a complaint raised by a citizen
	public class Grievance
	{
		[Key] // Primary key
		public int GrievanceID { get; set; }

		[Required(ErrorMessage = "CitizenID is required.")]
		public int CitizenID { get; set; } // Who raised this grievance

		[MaxLength(500, ErrorMessage = "Remarks can be maximum 500 characters.")]
		public string Remarks { get; set; } // Officer/Supervisor remarks

		[Required]
		public GrievanceStatus Status { get; set; } // Current status of grievance

		[Required]
		public DateTime FiledDate { get; set; } // When grievance was filed

		public DateTime? ForwardedDate { get; set; } // When forwarded to supervisor

		// Navigation property to related appeals
		public List<Appeal> Appeals { get; set; }
	}
}
