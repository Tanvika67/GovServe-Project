namespace GovServe_Project.DTOs.SupervisorDTO
{
	public class NotificationDto
	{
		public int UserId { get; set; }
		public int CaseId { get; set; }
		public string Message { get; set; }
		public string Category { get; set; }
	}
}
