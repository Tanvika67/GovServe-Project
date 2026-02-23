namespace GovServe_Project.DTOs
{
	public class CreateApplicationDTO
	{
		public string ServiceId { get; set; }

		public int UserId { get; set; }

		public DateTime SubmittedDate { get; set; }

		public string ApplicationStatus { get; set; } = "Submitted";
	}
}
