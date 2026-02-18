using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GovServe.Models
{
    public class Notification
    {
        [Key]
        public int NotificationId { get; set; }
		[Required]
		[ForeignKey("User")]
		public int  UserId { get; set; }
	//	public virtual Users UserId { get; set; }    // ==>why error
		[Required]
		[StringLength(150)]
		public string Message { get; set; }
		[Required]
		[RegularExpression("Info|Warning|Alert")]
        public string Category {  get; set; }
		[Required]
		[RegularExpression("Read|Unread")]
		public string Status{ get; set; }
		[Required]
		public DateTime CreatedDate { get; set; }

    }
}
