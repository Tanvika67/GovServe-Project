using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GovServe_Project.Models.SuperModels
{ 
	public class Notification
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int NotificationId { get; set; }
		[Required]
		[ForeignKey("User")]
		public int UserId { get; set; }
		public virtual Users User { get; set; }
		[Required]
		[ForeignKey("Case")]
		public int CaseId { get; set; }
		public virtual Case Case { get; set; }
		[Required]
		public string Message { get; set; }
		[RegularExpression("Assignment|Escalation|Update|In-Progress|Rejected|Completed", ErrorMessage = "Invalid category")]
		public string Category { get; set; }
		[RegularExpression("Unread|Read", ErrorMessage = "Invalid notification status")]
        public bool IsRead { get; set; } = false;

		//[NotMapped]
		public string Status => IsRead ? "Read" : "Unread";

		public DateTime CreatedDate { get; set; } = DateTime.Now;
	}

}
