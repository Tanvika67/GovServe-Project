using GovServe_Project.Enum;

namespace GovServe_Project.DTOs
{
	public class RaiseGrievanceDTO
	{
		public int UserId { get; set; }

		public int ApplicationID { get; set; }

		public string? Reason { get; set; }

		public string? Description { get; set; }

		public DateTime FileDate { get; set; } = DateTime.Now;

		public GrievanceStatus Status { get; set; }

	
	}
}
