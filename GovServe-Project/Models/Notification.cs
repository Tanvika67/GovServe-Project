using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GovServe.Models
{
    public class Notification
    {
        [Key]
<<<<<<< HEAD
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int NotificationId { get; set; }
		[Required]
		[ForeignKey("User")]
		public int  UserId { get; set; }
		[ForeignKey("Case")]
		public int CaseId {  get; set; }
		[Required]
=======
        public int NotificationId { get; set; }


  

		//[Required]
		//[ForeignKey("User")]
		//public int  UserId { get; set; }
		//[ForeignKey("Case")]
		//public int CaseId {  get; set; }
		//[Required]
>>>>>>> 7ccf39df470d162ce7c4a7178afe304594bd83c4
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
