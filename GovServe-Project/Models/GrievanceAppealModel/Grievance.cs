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
		// Primary key of the table
		[Key]
		public int GrievanceId { get; set; }

		// Reference to Application table
		[Required]
		[ForeignKey("Application")]
		public int ApplicationId { get; set; }

		// Reference to Citizen/User table
		[Required]
		public int UserId { get; set; }

		// Reason for raising grievance
		[MaxLength(500)]
		public string Reason { get; set; }

		// Detailed explanation of grievance
		[MaxLength(1000)]
		public string Description { get; set; }

		// Remarks given by officer/supervisor (acts as notification for citizen)
		public string Remarks { get; set; }

		// Workflow status
		public GrievanceStatus Status { get; set; }

		// Date when grievance was created
		public DateTime FiledDate { get; set; }

	}
}
