namespace GovServe_Project.DTOs
{
	public class ApplicationResponseDTO
	{
		public int UserId { get; set; }

		public string ServiceName { get; set; }

		public string ApplicationStatus { get; set; }

		public DateTime SubmittedDate { get; set; }
	}
}
