using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using GovServe_Project.Enum;
using GovServe_Project.Models;

namespace GovServe_Project.Models.GrievanceAppealModel
{ 
	public class Grievance
	{
		// Primary key of the table
		[Key]
		public int GrievanceId { get; set; }

		
		[Required]
		[ForeignKey("Application")]
		public int ApplicationId { get; set; }

	
		[Required]
		public int UserId { get; set; }

		
		[MaxLength(500)]
		public string Reason { get; set; }

		// Detailed explanation of grievance
		[MaxLength(1000)]
		public string Description { get; set; }

		// Remarks given by officer/supervisor (acts as notification for citizen)
		public string Remarks { get; set; }

		// Workflow status
		public string Status { get; set; } = "Submitted";

		// Date when grievance was created
		public DateTime FiledDate { get; set; }

	}
}
