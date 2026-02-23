using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GovServe_Project.Models
{
    public class Notification
    {
        [Key]

		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int NotificationId { get; set; }
		[Required]
		[ForeignKey("User")]
		public int  UserId { get; set; }
		public virtual User User { get; set; }
		[ForeignKey("Case")]
		public int CaseId {  get; set; }
		public virtual Case Case { get; set; }
		[Required]
		[StringLength(1000)]
		public string Message { get; set; }
		[Required]
		[RegularExpression("^(Info|Warning|Alert)$")]
		public string Category { get; set; } = "Info";
		[Required]
		[RegularExpression("^(Read|Unread)$")]
		public string Status { get; set; } = "Unread";        //Unread, Read
		[Required]
		public DateTime CreatedDate { get; set; }

    }
}
