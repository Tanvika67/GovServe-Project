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
=======
        public int NotificationId { get; set; }

        public int  UserId { get; set; }
        public string Message { get; set; }
        public string Category {  get; set; }
		public string Status{ get; set; }
		public DateOnly CreatedDate { get; set; }

		//[Required]
		//[ForeignKey("User")]
		//public int  UserId { get; set; }
		
		//[ForeignKey("Case")]
		//public int CaseId {  get; set; }
		//[Required]
		//[StringLength(1000)]
		//public string Message { get; set; }
		//[Required]
		//[RegularExpression("Info|Warning|Alert")]
  //      public string Category {  get; set; }
		//[Required]
		//[RegularExpression("Read|Unread")]
		//public string Status{ get; set; }         //Unread, Read
		//[Required]
		//public DateTime CreatedDate { get; set; }

>>>>>>> 9a3d2318cc569bfc83007b5f01a7f13e94a715a5

    }
}
