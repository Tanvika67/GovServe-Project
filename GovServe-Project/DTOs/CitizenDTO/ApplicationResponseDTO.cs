namespace GovServe_Project.DTOs.CitizenDTO
{
	public class ApplicationResponseDTO
	{
		public int UserId { get; set; }

		public int ServiceID { get; set; }
		public string ServiceName { get; set; }
		public string ApplicationStatus { get; set; }
		public DateTime SubmittedDate { get; set; }
	}
}
