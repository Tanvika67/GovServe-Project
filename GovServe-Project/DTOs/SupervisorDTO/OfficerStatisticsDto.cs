namespace GovServe_Project.DTOs.SupervisorDTO
{
	public class OfficerStatisticsDto
	{
		public int OfficerId { get; set; }

		public string OfficerName { get; set; }

		public string Department { get; set; }

		public int TotalCases { get; set; }

		public int ActiveCases { get; set; }

		public int PendingCases { get; set; }

		public int CompletedCases { get; set; }

		public int EscalatedCases { get; set; }
	}
}
