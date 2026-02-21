using System.ComponentModel.DataAnnotations;

namespace GovServe.Models
{
    public class Notification
    {
        [Key]
        public int NotificationId { get; set; }


  

		//[Required]
		//[ForeignKey("User")]
		//public int  UserId { get; set; }
		//[ForeignKey("Case")]
		//public int CaseId {  get; set; }
		//[Required]
		[StringLength(1000)]
		public string Message { get; set; }
		[Required]
		[RegularExpression("Info|Warning|Alert")]
        public string Category {  get; set; }
		[Required]
		[RegularExpression("Read|Unread")]
		public string Status{ get; set; }         //Unread, Read
		[Required]
		public DateTime CreatedDate { get; set; }

    }
}
