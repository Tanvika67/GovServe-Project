using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using GovServe_Project.Enum;
using GovServe_Project.Models;
using GovServe_Project.Models.CitizenModels;


namespace GovServe_Project.Models.GrievanceAppealModel
{ 
	public class Grievance
	{
		// Primary key of the table
		[Key]
		public int GrievanceId { get; set; }

		
		[Required]
		[ForeignKey("ApplicationID")]
		public int ApplicationID { get; set; }
		public virtual Application Application { get; set; }	
	
		[Required]
		public int UserId { get; set; }
		[ForeignKey("UserId")]
		public virtual Users User { get; set; }	

		
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
