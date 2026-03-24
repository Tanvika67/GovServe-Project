namespace GovServe_Project.DTOs.CitizenDTO
{
	public class CreateApplicationDTO
	{
		public int UserId { get; set; }
		public int ServiceID { get; set; }
		public string ServiceName { get; set; }

		public int DepartmentID { get; set; }

		public DateTime CompletedDate { get; set; }
	}
}
