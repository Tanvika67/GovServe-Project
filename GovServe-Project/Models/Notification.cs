using System.ComponentModel.DataAnnotations;

namespace GovServe.Models
{
    public class Notification
    {
        [Key]
        public int NotificationId { get; set; }
<<<<<<< HEAD

        public int  UserId { get; set; }
        public string Message { get; set; }
        public string Category {  get; set; }
		public string Status{ get; set; }
		public DateOnly CreatedDate { get; set; }
=======
		[Required]
		[ForeignKey("User")]
		public int  UserId { get; set; }
		[ForeignKey("Case")]
		public int CaseId {  get; set; }
		[Required]
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
>>>>>>> 2ecc413b84a8deeb62aee7d2d3912243272203df

    }
}
