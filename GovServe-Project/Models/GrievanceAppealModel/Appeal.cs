using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using GovServe_Project.Enum;
using GovServe_Project.Models;

namespace GovServe.Models
{
	// Entity representing Appeal table
	public class Appeal
	{
		[Key]
		public int AppealID { get; set; }

		

		// Citizen reference
		[Required]
		[ForeignKey("Users")]
		public int UserId { get; set; }

		// Reason for appeal
		[MaxLength(500)]
		public string Reason { get; set; }

		// Detailed explanation
		[MaxLength(1000)]
		public string Description { get; set; }

		// Remarks by authority (acts as notification)
		public string Remarks { get; set; }

		// Workflow status
		public AppealStatus Status { get; set; }

		// Date filed
		public DateTime FiledDate { get; set; }
	}
}





