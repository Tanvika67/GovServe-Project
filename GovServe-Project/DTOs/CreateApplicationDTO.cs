namespace GovServe_Project.DTOs
{
	public class CreateApplicationDTO
	{
		public string ServiceId { get; set; }

		public int UserId { get; set; }

        public string ServiceName { get; set; }

		public string? Description { get; set; }
	}
}
