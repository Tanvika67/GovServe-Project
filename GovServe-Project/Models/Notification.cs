using System.ComponentModel.DataAnnotations;

namespace GovServe.Models
{
    public class Notification
    {
        [Key]
        public int NotificationId { get; set; }

        public int  UserId { get; set; }
        public string Message { get; set; }
        public string Category {  get; set; }
		public string Status{ get; set; }
		public DateOnly CreatedDate { get; set; }

    }
}
